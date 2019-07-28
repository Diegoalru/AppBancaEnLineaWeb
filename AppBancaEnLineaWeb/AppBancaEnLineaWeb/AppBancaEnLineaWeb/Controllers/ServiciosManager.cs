using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppBancaEnLineaWeb.Models;
using Newtonsoft.Json;

namespace AppBancaEnLineaWeb.Controllers
{
    public class ServiciosManager
    {
        #region Variables
        /// <summary>
        /// URL que retorna (GET) y recibe (PUT) información de un servicio actualizado.
        /// </summary>
        const string Url = "https://www.gruposama.com/WebApiSecureSAMA/api/servicio/";

        /// <summary>
        /// URL que inserta (POST) un servicio.
        /// </summary>
        const string UrlAgregar = "https://www.gruposama.com/WebApiSecureSAMA/api/servicio/ingresar";

        /// <summary>
        /// Llamado a la clase que posee la validación del usuario.
        /// </summary>
        UsuarioManager Usuario = new UsuarioManager();
        #endregion

        #region Constructor
        public ServiciosManager()
        {

        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo que retorna el listado de los servicios.
        /// </summary>
        /// <returns>Retorna un listado del servicios.</returns>
        public async Task<IEnumerable<Servicio>> ObtenerServicios()
        {
            HttpClient cliente = new HttpClient();
            string respuesta = await cliente.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Servicio>>(respuesta);
        }

        /// <summary>
        /// Agrega servicios.
        /// </summary>
        /// <param name="servicio">Objeto que contiene el nuevo servicio.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Servicio> AgregarServicio(Servicio servicio)
        {
            HttpClient cliente = new HttpClient();
            var respuesta = await cliente.PostAsync(UrlAgregar,
                new StringContent(JsonConvert.SerializeObject(servicio),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Servicio>(await respuesta.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Actualiza los datos de un servicio.
        /// </summary>
        /// <param name="servicio">Servicio modificado.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Servicio> ActualizarServicio(Servicio servicio)
        {
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(servicio),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Servicio>(await
            response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Elimina un servicio mediante su codigo.
        /// </summary>
        /// <param name="servicioCod">Codigo del servicio a eliminar.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<string> Eliminar(string servicioCod)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync(Url + servicioCod);
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
        #endregion
    }
}
