using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace TCom
{
    public delegate void CompletedEventHandler(object sender, EventArgs e);

    public class ApiRequest
    {
        public int Timeout = 5000;
        public string ResponseText { get; set; }
        public event CompletedEventHandler Completed;
        private readonly string _postData;
        private HttpWebRequest _request;
        private HttpWebResponse _response;

        public ApiRequest(string url, object data = null, int timeout = 5000)
        {
            _request = (HttpWebRequest) WebRequest.Create(url);
            _request.ContentType = "application/json";
            _request.Timeout = timeout;

            if (data != null) _postData = new JavaScriptSerializer().Serialize(data);
        }

        protected virtual void OnCompleted(EventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        public void Call()
        {
            if (_postData != null) PostCall();
            else GetCall();
        }

        public string CallSync()
        {
            _request.Method = _postData == null ? WebRequestMethods.Http.Get : WebRequestMethods.Http.Put; // POST arbitrarily doesn't work

            if (_postData != null)
            {
                var requestStream = _request.GetRequestStream();
                var byteArray = Encoding.UTF8.GetBytes(_postData);
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
            }

            var responseStream = _request.GetResponse().GetResponseStream();

            if (responseStream == null) return ResponseText;

            using (var httpWebStreamReader = new StreamReader(responseStream))
            {
                ResponseText = httpWebStreamReader.ReadToEnd();
            }

            return ResponseText;
        }

        private void GetCall()
        {
            _request.Method = WebRequestMethods.Http.Get;
            _request.BeginGetResponse(GetResponseStreamCallback, _request);
        }

        private void PostCall()
        {
            _request.Method = WebRequestMethods.Http.Put; // POST arbitrarily doesn't work
            _request.BeginGetRequestStream(GetRequestStreamCallback, _request);
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                _request = (HttpWebRequest)asynchronousResult.AsyncState;
                var postStream = _request.EndGetRequestStream(asynchronousResult);

                var byteArray = Encoding.UTF8.GetBytes(_postData);

                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();

                _request.BeginGetResponse(GetResponseStreamCallback, _request);
            }
            catch (Exception ex)
            {
                Session.logException(ex);
            }
        }

        private void GetResponseStreamCallback(IAsyncResult callbackResult)
        {
            try
            {
                _request = (HttpWebRequest) callbackResult.AsyncState;
                _response = (HttpWebResponse) _request.EndGetResponse(callbackResult);
                var responseStream = _response.GetResponseStream();

                if (responseStream == null) return; // Avoid NullReferenceException

                using (var httpWebStreamReader = new StreamReader(responseStream))
                {
                    ResponseText = httpWebStreamReader.ReadToEnd();
                }

                OnCompleted(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Session.logException(ex);
            }
        }

        public override string ToString()
        {
            return ResponseText;
        }
    }
}
