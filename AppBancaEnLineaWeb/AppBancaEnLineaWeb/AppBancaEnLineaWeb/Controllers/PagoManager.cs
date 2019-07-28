using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppBancaEnLineaWeb.Models;
using Newtonsoft.Json;

namespace AppBancaEnLineaWeb.Controllers
{
    public class PagoManager
    {

        #region Variables
        /// <summary>
        /// URL que retorna (GET) y recibe (PUT) información de un pago actualizada.
        /// </summary>
        const string Url = "https://www.gruposama.com/WebApiSecureSAMA/api/pago/";

        /// <summary>
        /// URL que inserta (POST) un pago.
        /// </summary>
        const string UrlAgregar = "https://www.gruposama.com/WebApiSecureSAMA/api/pago/ingresar";

        /// <summary>
        /// Llamado a la clase que posee la validación del usuario.
        /// </summary>
        UsuarioManager Usuario = new UsuarioManager();
        #endregion

        #region Constructor
        public PagoManager()
        {

        }
        #endregion

        #region Metodos
        
        /// <summary>
        /// Crea una lista de los pagos del usuario.
        /// </summary>
        /// <param name="userCod">Codigo del usuario actual.</param>
        /// <returns>Lista con todos los pagos del usuario.</returns>
        public async Task<IEnumerable<Pago>> ObtenerPagos(string userCod)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var uri = new Uri(string.Format(Url + userCod));
            string respuesta = await cliente.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<IEnumerable<Pago>>(respuesta);
        }

        /// <summary>
        /// Agregar un pago.
        /// </summary>
        /// <param name="pago">Objeto pago que va a ser agregado.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Pago> AgregarPago(Pago pago)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.PostAsync(UrlAgregar,
                new StringContent(JsonConvert.SerializeObject(pago),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Pago>(await
            respuesta.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Actualiza los datos de un pago.
        /// </summary>
        /// <param name="pago">Pago modificada.</param>
        /// <returns>Respuesta del servidor.</returns>
        [Obsolete("Esta función no está permitida.", true)]
        public async Task<Pago> ActualizarPago(Pago pago)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(pago),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Pago>(await respuesta.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Elimina un pago mediante su codigo.
        /// </summary>
        /// <param name="pagoCod">Codigo del pago a eliminar.</param>
        /// <returns>Respuesta del servidor.</returns>
        [Obsolete("Esta función no está permitida.", true)]
        public async Task<string> EliminarPago(string pagoCod)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.DeleteAsync(Url + pagoCod);
            return JsonConvert.DeserializeObject<string>(await respuesta.Content.ReadAsStringAsync());
        }
        #endregion

    }
}
