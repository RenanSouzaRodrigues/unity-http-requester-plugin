using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
        public async Task<Response> GetAsync(string URL, Request request)
        {
            this.SetDefaultHeaders(request.Headers);
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
        public async Task<Response> PostAsync(string URL, Request Request) 
        {
            this.SetDefaultHeaders(Request.Headers);
            HttpResponseMessage httpResponse = await this.client.PostAsync(this.PrepareURL(URL), Request.Body);
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
        public async Task<Response> PutAsync(string URL, Request Request)
        {
            this.SetDefaultHeaders(Request.Headers);
            HttpResponseMessage httpResponse = await this.client.PutAsync(this.PrepareURL(URL), Request.Body);
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }

        /// <summary>
        /// Performs a PATCH request method
        /// </summary>
        /// <param name="URL">The URL to make the request</param>
        /// <param name="Request">The request instance to send</param>
        /// <returns>Returns a new Response instance</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public async Task<Response> PatchAsync(string URL, Request Request)
        {
            this.SetDefaultHeaders(Request.Headers);
            HttpResponseMessage httpResponse = await this.client.PatchAsync(this.PrepareURL(URL), Request.Body);
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
        public async Task<Response> DeleteAsync(string URL, Request request = null)
        {
            if (request is not null) request = new Request();
            this.SetDefaultHeaders(request.Headers);
            HttpResponseMessage httpResponse = await this.client.DeleteAsync(this.PrepareURL(URL));
            Response response = new Response(httpResponse);
            await response.BuildResponseContent();
            return response;
        }
    }
}