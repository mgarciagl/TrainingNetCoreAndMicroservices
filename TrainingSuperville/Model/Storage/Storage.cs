using System;
using System.Collections.Generic;
using System.Linq;
using TrainingSuperville.Models.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace TrainingSuperville.Models
{
    public class Storage : IStorage<StorableEntity>
    {
        private static readonly Storage _storage = new Storage();

        private static readonly string baseUrl = "http://localhost:44300/";
        //private static readonly string baseUrl = "http://localhost:7000/";
        private static readonly string clientUrl = baseUrl + "client-api/";
        private static readonly string productUrl = baseUrl + "product-api/";

        public Storage() { }

        public static Storage GetInstance() => _storage;

        public static List<StorableEntity> storableEntities { get; set; } = new List<StorableEntity>();

        public async Task<IEnumerable<StorableEntity>> GetAll()
        {
            IEnumerable<StorableEntity> result;

            using (var handler = new HttpClientHandler())
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.BaseAddress = new Uri(clientUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync("client");

                var clients = await DeserializeResponseContentClients(response);

                result = clients;
            }

            using (var handler = new HttpClientHandler())
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.BaseAddress = new Uri(productUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync("product");

                var products = await DeserializeResponseContentProducts(response);

                result = result.Concat(products);
            }

            return result;
        }

        public async Task<StorableEntity> Get(int entityId, Type type)
        {
            if (type.Equals(typeof(Client)))
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(clientUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.GetAsync("client/" + entityId);

                    var client = await DeserializeResponseContentClient(response);

                    return client;
                }
            }
            else
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(productUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.GetAsync("product/" + entityId);

                    var product = await DeserializeResponseContentProduct(response);

                    return product;
                }
            }
        }

        public async Task<StorableEntity> Add(StorableEntity entity)
        {
            if (entity.GetType().Equals(typeof(Client)))
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(clientUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var myContent = JsonConvert.SerializeObject(entity);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync("client", byteContent);

                    var client = await DeserializeResponseContentClient(response);

                    return client;
                }
            }
            else
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(productUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var myContent = JsonConvert.SerializeObject(entity);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync("product", byteContent);

                    var product = await DeserializeResponseContentProduct(response);

                    return product;
                }
            }
        }

        public async Task<StorableEntity> Update(StorableEntity entity)
        {
            if (entity.GetType().Equals(typeof(Client)))
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(clientUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var myContent = JsonConvert.SerializeObject(entity);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PutAsync("client/" + entity.Id, byteContent);

                    var client = await DeserializeResponseContentClient(response);

                    return client;
                }
            }
            else
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    httpClient.BaseAddress = new Uri(productUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var myContent = JsonConvert.SerializeObject(entity);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PutAsync("product/" + entity.Id, byteContent);

                    var product = await DeserializeResponseContentProduct(response);

                    return product;
                }
            }
        }

        public async Task<StorableEntity> Remove(int entityId, Type type)
        {
            if (type.Equals(typeof(Client)))
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    var entity = await Get(entityId, type);

                    httpClient.BaseAddress = new Uri(clientUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.DeleteAsync("client/" + entity.Id);

                    var client = await DeserializeResponseContentClient(response);

                    return client;
                }
            }
            else
            {
                using (var handler = new HttpClientHandler())
                using (var httpClient = new HttpClient(handler))
                {
                    var entity = await Get(entityId, type);

                    httpClient.BaseAddress = new Uri(productUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.DeleteAsync("product/" + entity.Id);

                    var product = await DeserializeResponseContentProduct(response);

                    return product;
                }
            }
        }

        static async Task<Client> DeserializeResponseContentClient(HttpResponseMessage response)
        {
            var transactionResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Client>(transactionResult);
            return result;
        }

        static async Task<IEnumerable<Client>> DeserializeResponseContentClients(HttpResponseMessage response)
        {
            var transactionResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Client>>(transactionResult);
            return result;
        }

        static async Task<Product> DeserializeResponseContentProduct(HttpResponseMessage response)
        {
            var transactionResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Product>(transactionResult);
            return result;
        }

        static async Task<IEnumerable<Product>> DeserializeResponseContentProducts(HttpResponseMessage response)
        {
            var transactionResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Product>>(transactionResult);
            return result;
        }
    }
}
