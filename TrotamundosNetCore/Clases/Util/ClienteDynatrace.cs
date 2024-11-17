using Newtonsoft.Json;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TrotamundosNetCore.Clases.Util.Dynatrace;

namespace TrotamundosNetCore.Clases.Util
{
    public class ClienteDynatrace
    {
        private string Endpoint { get; set; }
        private string Ambiente { get; set; }
        private string Token { get; set; }

        public ClienteDynatrace(string endpoint, string ambiente, string token)
        {
            Endpoint = endpoint.Trim().TrimEnd('/');
            Ambiente = ambiente.Trim();
            Token = token;
        }

        public Metrics ConsultaAPIMetricas(string metric, DateTime fechaInicio, DateTime fechaFin, string resolutor = "10080")
        {
            return ConsultaDynatrace<Metrics>(GeneraUrl($"/api/v2/metrics/query?metricSelector={metric}&from={new DateTimeOffset(fechaInicio).ToUnixTimeMilliseconds()}&to={new DateTimeOffset(fechaFin).ToUnixTimeMilliseconds()}&resolutor={resolutor}"));
        }

        public Entity ConsultaEntidad(string entityId)
        {
            return ConsultaDynatrace<Entity>(GeneraUrl($"/api/v2/entities/{entityId}"));
        }

        public List<Entity> ConsultaEntidadesEnHostgroup(string hostgroupId)
        {
            RespuestaEntities respuesta = ConsultaDynatrace<RespuestaEntities>(GeneraUrl($"/api/v2/entities?entitySelector=type(\"HOST\"),fromRelationships.isInstanceOf(entityId(\"{hostgroupId}\"))&fields=properties"));

            return respuesta.entities;
        }

        public T ConsultaDynatrace<T>(string url) where T : class
        {
            ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = HttpWebRequest.CreateHttp(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", $"Api-Token {Token}");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string res = "";

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        res = reader.ReadToEnd();
                    }

                    T respuesta = JsonConvert.DeserializeObject<T>(res);

                    return respuesta;
                }
                else
                {
                    //Error

                }
            }

            return null;
        }

        private string GeneraUrl(string url)
        {
            return $"{Endpoint}/e/{Ambiente}/{url.TrimStart('/')}";
        }
    }
}
