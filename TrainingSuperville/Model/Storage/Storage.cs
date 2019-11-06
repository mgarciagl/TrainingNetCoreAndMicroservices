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

        private static readonly string baseUrl = "http://localhost:7000/";
        private static readonly string clientUrl = baseUrl + "client-api/";
        private static readonly string productUrl = baseUrl + "product-api/";

        public Storage() { }

        public static Storage GetInstance() => _storage;

        public static List<StorableEntity> storableEntities { get; set; } = new List<StorableEntity>();

        public async Task<IEnumerable<StorableEntity>> GetAll()
        {

            using (var handler = new HttpClientHandler())
            using (var httpClient = new HttpClient(handler))
            {
                httpClient.BaseAddress = new Uri(clientUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync("client");

                var clients = await DeserializeResponseContentClients(response);

                return clients;
            }
        }

        public async Task<StorableEntity> Get(int entityId)
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

        public async Task<StorableEntity> Add(StorableEntity entity)
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

        public async Task<StorableEntity> Update(StorableEntity entity)
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
            //var storableEntity = storableEntities.FirstOrDefault(d => d.Id == entity.Id);
            //if (storableEntity != null && storableEntity.GetType().Equals(typeof(Client)))
            //{
            //    var client = (Client)storableEntity;
            //    var clientUpdated = (Client)entity;

            //    client.Name = clientUpdated.Name;
            //    client.Email = clientUpdated.Email;
            //}
            //else if (storableEntity != null && storableEntity.GetType().Equals(typeof(Product)))
            //{
            //    var product = (Product)storableEntity;
            //    var productUpdated = (Product)entity;

            //    product.Code = productUpdated.Code;
            //    product.Name = productUpdated.Name;
            //}
        }

        public async Task<StorableEntity> Remove(int entityId)
        {
            using (var handler = new HttpClientHandler())
            using (var httpClient = new HttpClient(handler))
            {
                var entity = await Get(entityId);

                httpClient.BaseAddress = new Uri(clientUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.DeleteAsync("client/" + entity.Id);

                var client = await DeserializeResponseContentClient(response);

                return client;
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
