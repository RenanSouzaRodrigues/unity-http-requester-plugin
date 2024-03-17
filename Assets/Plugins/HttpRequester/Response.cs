using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace DallaiStudios.Plugins.HttpRequester
{
    /// <summary>
    /// Class that represents the HTTP result response
    /// </summary>
    /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
    /// <version>1.2.0</version>
    public class Response
    {
        public HttpStatusCode HttpStatusCode;

        private string headers;
        private string body;
        private readonly HttpResponseMessage responseMessage;
        
        public Response(HttpResponseMessage ResponseMessage) => this.responseMessage = ResponseMessage;

        /// <summary>
        /// This method will abstract the response and build it. This process runs automatically
        /// </summary>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        /// <version>1.0.0</version>
        public async Task BuildResponseContent()
        {
            this.body = await this.responseMessage.Content.ReadAsStringAsync();
            this.headers = this.responseMessage.Headers.ToString();
            this.HttpStatusCode = (HttpStatusCode) this.responseMessage.StatusCode;
        }

        /// <summary>
        /// This method will parse the response body for the provided class.
        /// </summary>
        /// <typeparam name="T">The class type to map the response body to.</typeparam>
        /// <returns>An instance of the provided class.</returns>
        /// <author><a href="https://github.com/RenanSouzaRodrigues">Renan Souza (Dallai)</a></author>
        /// <version>1.2.0</version>
        public T ParseBody<T>()
        {
            return JsonUtility.FromJson<T>(this.body);
        }
    }
}