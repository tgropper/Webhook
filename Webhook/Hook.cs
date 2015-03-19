using System;
using System.Linq;
using System.Web;

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
            var qs = GetQueryString(queryString);
            var json = GetJsonBody(body);

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
                throw new NotImplementedException(String.Format("Method {0} is not implemented yet", data.Method));
        }

        private string GetQueryString(object obj = null)
        {
            if (obj == null)
                return null;

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return "?" + String.Join("&", properties.ToArray());
        }

        private string GetJsonBody(object obj = null)
        {
            if (obj == null)
                return null;

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}