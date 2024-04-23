using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace DallaiStudios.Plugins.HttpRequester
{
    /// <summary>
    /// Class that defines a request to be send as a HTTP Request using the HTTP class
    /// </summary>
    /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
    public class Request
    {
        public Dictionary<string, string> Headers { get; private set; }
        public Dictionary<string, string> QueryParameters { get; private set; }
        public StringContent Body { get; private set; }
        
        public Request() => this.Headers = new Dictionary<string, string>();
        
        /// <summary>
        /// Define the request default headers
        /// </summary>
        /// <param name="RequestHeaders">A Dictionary with the header keys (string) and values (string)</param>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void SetRequestHeaders(Dictionary<string, string> RequestHeaders) => this.Headers = RequestHeaders;

        /// <summary>
        /// Adds a new request header.
        /// </summary>
        /// <param name="key">The header Key.</param>
        /// <param name="value">The header value for the provided key.</param>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void AddHeader(string key, string value) => this.Headers.Add(key, value);

        /// <summary>
        /// Set a new request query parameters.
        /// </summary>
        /// <param name="key">The header Key.</param>
        /// <param name="value">The header value for the provided key.</param>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void SetQueryParameters(Dictionary<string, string> QueryParameters) => this.QueryParameters = QueryParameters;
        
        /// <summary>
        /// Adds a new query parameter to the request.
        /// </summary>
        /// <param name="key">The header Key.</param>
        /// <param name="value">The header value for the provided key.</param>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void AddQueryParameter(string key, string value) => this.QueryParameters.Add(key, value);
        
        /// <summary>
        /// Define the request body payload
        /// </summary>
        /// <param name="BodyContent">The class that will be used as the body content</param>
        /// <typeparam name="T">The class type to map the request body to</typeparam>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void SetRequestBody<T>(T BodyContent) where T : class
        {
            string stringContent = JsonUtility.ToJson(BodyContent);
            this.Body = new StringContent(stringContent, Encoding.UTF8, "application/json");
        }
        
        /// <summary>
        /// Clear the request body payload
        /// </summary>
        /// <typeparam name="T">The class type to map the request body to</typeparam>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void Clear() => this.Body = null;
        
        /// <summary>
        /// Clear the request headers
        /// </summary>
        /// <typeparam name="T">The class type to map the request body to</typeparam>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        public void ClearHeaders() => this.Headers.Clear();
    }
}
