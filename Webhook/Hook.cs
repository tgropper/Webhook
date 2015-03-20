using System;
using System.ComponentModel;
using Webhook.Helpers;

namespace Webhook
{
    public class Hook : Component, IHook
    {
        private Client _client;
        private static Action<Exception> OnError;

        public Hook(Action<Exception> onError = null)
        {
            _client = new Client();
            OnError = onError;
        }

        private delegate void NotifyMethodCaller(string key, object queryString = null, object body = null);

        public void Notify(string key, object queryString = null, object body = null)
        {
            try
            {
                var qs = ClientHelpers.GetQueryString(queryString);
                var json = ClientHelpers.GetJsonBody(body);

                var data = ConfigSection.Webhook.Data[key];
                if (data.Method == "GET")
                {
                    _client.httpGetRequest(data.Url + qs);
                }
                else if (data.Method == "POST")
                {
                    _client.httpPostRequest(data.Url, json);
                }
                else
                {
                    throw new NotImplementedException(String.Format("Http method {0} is not implemented yet", data.Method));
                }
            }
            catch (Exception ex)
            {
                if (OnError != null)
                    OnError(ex);
            }
        }

        public void NotifyAsync(string key, object queryString = null, object body = null)
        {
            NotifyMethodCaller workerDelegate = new NotifyMethodCaller(Notify);
            workerDelegate.BeginInvoke(key, queryString, body, null, null);
        }
    }
}