using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppBancaEnLineaWeb.Models;
using AppBancaEnLineaWeb.Controllers;
using Newtonsoft.Json;

namespace AppBancaEnLineaWeb.Controllers
{
    public class TransferenciasManager
    {

        #region Variables
        /// <summary>
        /// URL que retorna (GET) y recibe (PUT) información de una transferencia actualizada.
        /// </summary>
        const string Url = "https://www.gruposama.com/WebApiSecureSAMA/api/transferencia/";

        /// <summary>
        /// URL que inserta (POST) una transferencia.
        /// </summary>
        const string UrlAgregar = "https://www.gruposama.com/WebApiSecureSAMA/api/transferencia/ingresar";

        /// <summary>
        /// Llamado a la clase que posee la validación del usuario.
        /// </summary>
        UsuarioManager Usuario = new UsuarioManager();
        #endregion

        #region Constructor
        public TransferenciasManager()
        {

        }
        #endregion

        #region Metodos
        /// <summary>
        /// Devuelve todas las transferencias del usuario.
        /// </summary>
        /// <param name="userCod">Codigo del usuario actual.</param>
        /// <returns>Devuele una lista de las cuentas del usuario.</returns>
        public async Task<IEnumerable<Transferencia>> ObtenerTransferencias()
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var uri = new Uri(string.Format(Url));
            string respuesta = await cliente.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<IEnumerable<Transferencia>>(respuesta);
        }

        /// <summary>
        /// Agregar una transferencia.
        /// </summary>
        /// <param name="transferencia">Objeto transferencia que va a ser agregado.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Transferencia> AgregarTransferencia(Transferencia transferencia)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.PostAsync(UrlAgregar,
                new StringContent(JsonConvert.SerializeObject(transferencia),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Transferencia>(await
            respuesta.Content.ReadAsStringAsync());
        }
        #endregion
    }
}

