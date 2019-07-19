using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppBancaEnLineaWeb.Models;
using Newtonsoft.Json;

namespace AppBancaEnLineaWeb.Controllers
{
    public class UsuarioManager
    {
        const string UrlAuth = "https://www.gruposama.com/WebApiSecureSAMA/api/loginext/authenticate/";
        const string UrlRegister = "https://www.gruposama.com/WebApiSecureSAMA/api/loginext/register/";
        const string appType = "application/json";

        public UsuarioManager()
        {
        }

        public async Task<Usuario> Validar(string username, string password)
        {
            LoginRequest login = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            HttpClient client = new HttpClient();

            var response = await client.PostAsync(UrlAuth,
                new StringContent(JsonConvert.SerializeObject(login),
                Encoding.UTF8,
                appType));

            return JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Usuario> Registar(Usuario usuario)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsync
            (
                UrlRegister,
                new StringContent(
                    JsonConvert.SerializeObject(usuario),
                    Encoding.UTF8,
                    appType)
            );
            return JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
        }
    }
}