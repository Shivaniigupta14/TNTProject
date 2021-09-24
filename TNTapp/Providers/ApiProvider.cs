using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TNTapp.Models;

namespace TNTapp.Providers
{
    public class ApiProvider: IApiProvider
    { 
        //To deifne local variable....
        private readonly HttpClient _httpClient;
        #region Constructor
        /// <summary>
        /// TODO : To define Constructor...
        /// </summary>
        public ApiProvider()
        {
            // Create an HttpClientHandler object and set to use default credentials
            HttpClientHandler handler = new HttpClientHandler();

            // Create an HttpClient object
            _httpClient = new HttpClient(handler);

             //Time required to  a process
            TimeSpan ts = TimeSpan.FromMilliseconds(100000);
            _httpClient.Timeout = ts;
        }
        #endregion
        /// <summary>
        /// TODO : Delete to the specified ...
        /// </summary>
        /// <typeparam name="T">The Response type (return type) (Jsonable object).</typeparam>
        /// <param name="url"> URL to Del </param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public ApiResult<T> Delete<T>(string url, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    result = _httpClient.DeleteAsync(url).Result;
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    }
                }

                var rawResult = result.Content.ReadAsStringAsync().Result;
                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)result.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message is :-" + ex.Message);
            }

            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The Response type (return type) (Jsonable object).</typeparam>
        /// <param name="url">URL to GET to.</param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public ApiResult<T> Get<T>(string url, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    result = _httpClient.GetAsync(url).Result;
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    }
                }

                var rawResult = result.Content.ReadAsStringAsync().Result;

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)result.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Message is :-" + e.Message);
            }

            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }
        /// <summary>
        /// TODO : 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public string GetString(string url, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    result = _httpClient.GetAsync(url).Result;
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    }
                }

                var rawResult = result.Content.ReadAsStringAsync().Result;

                try
                {
                    return rawResult;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message is :-" + ex.Message);
                return null;
            }
            return null;
        }
        /// <summary>
        /// TODO : To load Cookies
        /// </summary>
        /// <param name="path"></param>
        public void LoadCookies(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  TODO : Post to the specified url the body.
        /// </summary>
        /// <param name="url">URL to post to.</param>
        /// <param name="body">Body of post.</param>
        /// <typeparam name="T">The Response type (return type) (Jsonable object).</typeparam>
        /// <typeparam name="TR">The RequestType (the body) (Jsonable object).</typeparam>
        public async Task<ApiResult<T>> Post<T, TR>(string url, TR body, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            string returnResult = string.Empty;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    var x = JsonConvert.SerializeObject(body);
                    var y = JsonConvert.SerializeObject(headers);
                    if (body != null)
                    {
                        result = _httpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")).Result;
                    }
                    else
                    {
                        _httpClient.DefaultRequestHeaders.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 1500000);
                    }
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    } 
                }
                var rawResult = result.Content.ReadAsStringAsync().Result;
                dynamic deserialized = "";
                try
                {
                    deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }
                try
                {
                    return new ApiResult<T>(rawResult, (int)result.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debugger.Break();
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Message is :-" + e.Message);
            }
            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string cleanForJSON(string s)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }

            char c = '\0';
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            String t;

            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                switch (c)
                {
                    case '\\':
                    case '"':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '/':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        if (c < ' ')
                        {
                            t = "000" + String.Format("X", c);
                            sb.Append("\\u" + t.Substring(t.Length - 4));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }
        //PDF POST METHOD
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<ApiResult<T>> PostPDF<T, TR>(string url, TR body, Dictionary<string, string> headers = null)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public ApiResult<T> Put<T, TR>(string url, TR body, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    result = _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")).Result;
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    }
                }

                var rawResult = result.Content.ReadAsStringAsync().Result;

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)result.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Message is :-" + e.Message);
            }
            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }
        /// <summary>
        /// TODO : to save cookies
        /// </summary>
        /// <param name="path"></param>
        public void SaveCookies(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO : To delete Cookies.....
        /// </summary>
        /// <param name="path"></param>
        public void DeleteCookies(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO : To ExpiresCookies.....
        /// </summary>
        public void ExpireCookies()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headers"></param>
        void AddHeadersToClient(Dictionary<string, string> headers)
        {
            foreach (var kv in headers)
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error Message is :-" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="headers"></param>
        void RemoveHeadersFromClient(Dictionary<string, string> headers)
        {
            foreach (var kv in headers)
            {
                _httpClient.DefaultRequestHeaders.Remove(kv.Key);
            }
        }

      
    }
}
