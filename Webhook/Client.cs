using System.IO;
using System.Net;

namespace Webhook
{
    public class Client
    {
        public bool httpPostRequest(string url, string body = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                if (body != null)
                {
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    return httpResponse.StatusCode == HttpStatusCode.OK;
                }
            }
        }

        public bool httpGetRequest(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";

            try
            {
                using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    return httpResponse.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException ex)
            {
                //Log(ex.Message);
                return false;
            }
        }
    }
}