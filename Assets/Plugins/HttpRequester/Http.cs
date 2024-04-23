using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

namespace DallaiStudios.Plugins.HttpRequester
{
    /// <summary>
    /// This class can be used to perform HTTP actions such as GET, POST, PUT, PATCH, and DELETE.
    /// Every single one will return a Response class instance and demands a Request instance to create
    /// the HTTP request.
    /// </summary>
    /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
    public class Http 
    {
        private HttpClient client;
        private string baseURL;

        public Http() => this.InitializeHttpClient();

        public Http(string BaseURL)
        {
            this.SetBaseURL(BaseURL); 
            this.InitializeHttpClient();
        }
        
        private void InitializeHttpClient() => this.client = new HttpClient();

        private void SetDefaultHeaders(Dictionary<string, string> DefaultHeaders)
        {
            this.client.DefaultRequestHeaders.Clear();
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (DefaultHeaders is null || DefaultHeaders.Count == 0) return;
            foreach (KeyValuePair<string, string> entry in DefaultHeaders)
            {
                this.client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
        }

        private Uri PrepareURL(string URL) 
        {
            return new Uri(string.IsNullOrEmpty(this.baseURL) ? URL : this.baseURL + URL);
        }
        
        private string SetQueryParameters(string URL, Dictionary<string, string> QueryParameters)
        {
            string query = "?";
            foreach (KeyValuePair<string, string> entry in QueryParameters)
            {
                query += entry.Key + "=" + entry.Value + "&";
            }
            return URL + query.TrimEnd('&');
        }
        
        /// <summary>
        /// This method can be used to provide for the Http class a default base URL for every request.
        /// </summary>  
        /// <remarks>
        /// The base URL will be used for each new request created, so keep in mind that if you need to 
        /// use another base URL you must change the defined URL or create a new instance of the Http class.
        /// </remarks>
        /// <param name="BaseURL">Define the base URL to be used.</param>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void SetBaseURL(string BaseURL) 
        {
            this.baseURL = BaseURL;
        }
        
        /// <summary>
        /// Performs a GET request method
        /// </summary>
        /// <param name="URL">The URL to make the request</param>
        /// <param name="request">The request object</param>
        /// <returns>Returns a new Response instance</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public async Task<Response> GetAsync(string URL, Request request = null)
        {
            if (request is null) request = new Request();
            this.SetDefaultHeaders(request.Headers);
            if (request.QueryParameters.Count > 0) URL = this.SetQueryParameters(URL, request.QueryParameters);
            HttpResponseMessage httpResponse = await this.client.GetAsync(this.PrepareURL(URL));
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a POST request method
        /// </summary>
        /// <param name="URL">The URL to make the request</param>
        /// <param name="Request">The request to send</param>
        /// <returns>Returns a new Response instance</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public async Task<Response> PostAsync(string URL, Request request) 
        {
            if (request is null)
            {
                Debug.LogError("POST requests must have a Request Object. Make sure you are sending a Request Object.");
                return null;
            }
            this.SetDefaultHeaders(request.Headers);
            if (request.QueryParameters.Count > 0) URL = this.SetQueryParameters(URL, request.QueryParameters);
            HttpResponseMessage httpResponse = await this.client.PostAsync(this.PrepareURL(URL), request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a PUT request method
        /// </summary>
        /// <param name="URL">The URL to make the request</param>
        /// <param name="Request">The request instance to send</param>
        /// <returns>Returns a new Response instance</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public async Task<Response> PutAsync(string URL, Request request)
        {
            if (request is null)
            {
                Debug.LogError("PUT requests must have a request object. Make sure you are sending a request object.");
                return null;
            }
            this.SetDefaultHeaders(request.Headers);
            if (request.QueryParameters.Count > 0) URL = this.SetQueryParameters(URL, request.QueryParameters);
            HttpResponseMessage httpResponse = await this.client.PutAsync(this.PrepareURL(URL), request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a PATCH request method
        /// </summary>
        /// <param name="URL">The URL to make the request</param>
        /// <param name="request">The request instance to send</param>
        /// <returns>Returns a new Response instance</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public async Task<Response> PatchAsync(string URL, Request request)
        {
            if (request is null)
            {
                Debug.LogError("PATCH requests must have a request object. Make sure you are sending a request object.");
                return null;
            }
            this.SetDefaultHeaders(request.Headers);
            if (request.QueryParameters.Count > 0) URL = this.SetQueryParameters(URL, request.QueryParameters);
            HttpResponseMessage httpResponse = await this.client.PatchAsync(this.PrepareURL(URL), request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a DELETE request method
        /// </summary>
        /// <param name="URL">The URL to make the request</param>
        /// <param name="request">The request instance to send</param>
        /// <returns>Returns a new Response instance</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public async Task<Response> DeleteAsync(string URL, Request Request = null)
        {
            if (Request is null) Request = new Request();
            this.SetDefaultHeaders(Request.Headers);
            if (Request.QueryParameters.Count > 0) URL = this.SetQueryParameters(URL, Request.QueryParameters);
            HttpResponseMessage httpResponse = await this.client.DeleteAsync(this.PrepareURL(URL));
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }
    }
}