namespace TripLog.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public abstract class BaseHttpService
    {
        protected async Task<T> SendRequestAsync<T>(
            Uri url,
            HttpMethod httpMethod = null,
            IDictionary<string, string> headers = null,
            object requestData = null)
        {
            var result = default(T);

            var method = httpMethod ?? HttpMethod.Get;

            var data = SerializeObjectData<T>(requestData);

            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                PopulateRequest<T>(headers, data, requestMessage);

                result = await ExtractResponse<T>(requestMessage);
            }

            return result;
        }

        private void PopulateRequest<T>(IDictionary<string, string> headers, string data, HttpRequestMessage request)
        {
            if (data != null)
            {
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        private async Task<T> ExtractResponse<T>(HttpRequestMessage requestMessage)
        {
            var result = default(T);

            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    using (var response = await ExtractResponse<T>(requestMessage, client))
                    {
                        var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            result = DeserializeObjectData<T>(content);
                        }
                    }
                }
            }

            return result;
        }

        private static async Task<HttpResponseMessage> ExtractResponse<T>(HttpRequestMessage requestMessage, HttpClient client)
        {
            return await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead);
        }

        private T DeserializeObjectData<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        private string SerializeObjectData<T>(object requestData)
        {
            return requestData == null ? null : JsonConvert.SerializeObject(requestData);
        }
    }
}
