using System;
using Webhook.Helpers;

namespace Webhook
{
    public class Hook : IHook
    {
        private Client _client;

        public Hook()
        {
            _client = new Client();
        }

        public void Notify(string key, object queryString = null, object body = null)
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
    }
}