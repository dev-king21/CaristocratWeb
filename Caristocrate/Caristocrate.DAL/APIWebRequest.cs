using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Caristocrate.Common
{
    public class APIWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public APIWebRequest(string url)
        {
            // Create a request using a URL that can receive a post.

            request = WebRequest.Create(url);
        }

        public APIWebRequest(string url, string method)
            : this(url)
        {

            if (method.Equals("GET") || method.Equals("POST"))
            {
                // Set the Method property of the request to POST.
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public APIWebRequest(string url, string method, string data, string AccessToken, int Authorization = 0)
            : this(url, method)
        {

            HttpWebResponse response = null;

            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //    request.KeepAlive = true;
            //    request.Headers.Set(HttpRequestHeader.CacheControl, "no-cache");
            //    //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //    request.ContentType = "application/json";
            //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.125 Safari/537.36";
            //    request.Accept = "/";
            //    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            //    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8");
            //    if (Authorization == 1)
            //    {
            //        request.Headers.Add("Authorization", "Bearer " + AccessToken);
            //    }
            //    request.Method = method;
            //    //request.ServicePoint.Expect100Continue = false;               
            //    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(data);
            //    request.ContentLength = postBytes.Length;
            //    Stream stream = request.GetRequestStream();
            //    stream.Write(postBytes, 0, postBytes.Length);
            //    stream.Close();

            //    response = (HttpWebResponse)request.GetResponse();
            //}
            //catch (Exception ex)
            //{

            //}

            try
            {

                // Create POST data and convert it to a byte array.
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                //request.ContentType = "application/x-www-form-urlencoded" ;
                request.ContentType = "application/json";

                if (Authorization == 1)
                {
                    request.Headers.Add("Authorization", "Bearer " + AccessToken);
                }
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                dataStream = request.GetRequestStream();

                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            this.Status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }        
    }
}
