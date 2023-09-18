using PruebaIngreso.Models;
using System.IO;
using System.Net;
using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PruebaIngreso
{
    //Servicio de llamada a la API externa
    public class MarginService
    {
        //Metodo que hace la llamada por medio del codigo de servicio
        public MarginResponse GetMargin(string code)
        {
            //declaración de los protocolos de seguridad
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //Declaración de la respuesta y definición del llamado a la API
            string response = "0";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://refactored-pancake.free.beeceptor.com/margin/{code}");
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                //Se obtiene la respuesta de la API
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                //En caso de que la respuesta sea satisfactoria (Códigos de estado 2xx)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    response = responseReader.ReadToEnd();
                    Console.Out.WriteLine(response);
                }
            }
            catch (Exception e)
            {
                //En caso de obtener un error (Códigos de estado 4xx y 5xx)
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
                return null;
            }
            //Se declara la respuesta
            var margin = new MarginResponse();
            //Se deserializa la respuesta
            margin = JsonConvert.DeserializeObject<MarginResponse>(response);

            return margin;
        }
    }
}