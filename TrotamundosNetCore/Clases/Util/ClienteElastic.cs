using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;

namespace TrotamundosNetCore.Clases.Util
{
    public class ClienteElastic
    {
        private ConnectionSettings ElasticConfig { get; set; }

        public ClienteElastic(string url, string username, string password)
        {
            ElasticConfig = new ConnectionSettings(new Uri(url));

            ElasticConfig.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);
            ElasticConfig.BasicAuthentication(username, password);
            ElasticConfig.DisableDirectStreaming();
        }

        public T Busqueda<T>(string index, string query) where T : class, new()
        {
            ElasticLowLevelClient client = new ElasticLowLevelClient(ElasticConfig);

            try
            {
                StringResponse respuesta = client.Search<StringResponse>(index, query);

                if (respuesta.Success)
                {
                    return JsonConvert.DeserializeObject<T>(respuesta.Body);
                }
                else
                {
                    throw new Exception(respuesta.Body);
                }
            }
            catch
            {
                throw;
            }
        }

        public T Conteo<T>(string index, string query) where T : class, new()
        {
            ElasticLowLevelClient client = new ElasticLowLevelClient(ElasticConfig);

            try
            {
                StringResponse respuesta = client.Count<StringResponse>(index, query);

                if (respuesta.Success)
                {
                    return JsonConvert.DeserializeObject<T>(respuesta.Body);
                }
                else
                {
                    throw new Exception(respuesta.Body);
                }
            }
            catch
            {
                throw;
            }
        }


        public ISearchResponse<T> NestQuery<T>(Func<SearchDescriptor<T>, ISearchRequest> func) where T : class, new()
        {
            ElasticClient client = new ElasticClient(ElasticConfig);
            return client.Search(func);
        }

        public CountResponse NestCount<T>(Func<CountDescriptor<T>, ICountRequest> func) where T : class, new()
        {
            ElasticClient client = new ElasticClient(ElasticConfig);
            return client.Count(func);
        }

    }
}
