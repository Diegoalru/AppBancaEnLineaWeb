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
        #region Variables
        /// <summary>
        /// URL que valida que exista el usuario.
        /// </summary>
        const string UrlAuthenticate = "https://www.gruposama.com/WebApiSecureSAMA/api/loginext/authenticate/";
        /// <summary>
        /// URL para registrar un usuario.
        /// </summary>
        const string UrlRegister = "https://www.gruposama.com/WebApiSecureSAMA/api/loginext/register/";
        #endregion

        #region Constructor
        public UsuarioManager()
        {
        }

        #endregion

        /// <summary>
        /// Valida que el usuario exista.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Constraseña</param>
        /// <returns>Respuesta del usuario</returns>
        public async Task<Usuario> Validar(string username, string password)
        {
            LoginRequest login = new LoginRequest() { Username = username, Password = password };
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(UrlAuthenticate,
                new StringContent(JsonConvert.SerializeObject(login),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="usuario">Informacion del nuevo usuario.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Usuario> Registrar(Usuario usuario)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(UrlRegister,
                new StringContent(JsonConvert.SerializeObject(usuario),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Verifica que exista el usuario ingresado.
        /// </summary>
        /// <returns>Retorna un cliente con su TOKEN agregado.</returns>
        public HttpClient ObtenerCliente()
        {
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("Authorization", App.usuarioActual.TOKEN);
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");
            return cliente;
        }

    }
}