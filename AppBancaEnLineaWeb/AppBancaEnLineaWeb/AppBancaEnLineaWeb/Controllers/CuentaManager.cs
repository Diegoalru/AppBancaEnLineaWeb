﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppBancaEnLineaWeb.Models;
using Newtonsoft.Json;

namespace AppBancaEnLineaWeb.Controllers
{
    public class CuentaManager
    {
        #region Variables
        /// <summary>
        /// URL que retorna (GET) y recibe (PUT) información de una cuenta actualizada.
        /// </summary>
        const string Url = "https://www.gruposama.com/WebApiSecureSAMA/api/cuenta/";

        /// <summary>
        /// URL que inserta (POST) una cuenta.
        /// </summary>
        const string UrlAgregar = "https://www.gruposama.com/WebApiSecureSAMA/api/cuenta/ingresar/";

        /// <summary>
        /// Llamado a la clase que posee la validación del usuario.
        /// </summary>
        UsuarioManager Usuario = new UsuarioManager();
        #endregion

        #region Constructor
        public CuentaManager()
        { }
        #endregion

        #region Metodos
        /// <summary>
        /// Devuelve todas las cuentas del usuario.
        /// </summary>
        /// <param name="userCod">Codigo del usuario actual.</param>
        /// <returns>Devuele una lista de las cuentas del usuario.</returns>
        public async Task<IEnumerable<Cuenta>> ObtenerCuentas(string userCod)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var uri = new Uri(string.Format(Url + userCod));
            string respuesta = await cliente.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<IEnumerable<Cuenta>>(respuesta);
        }

        /// <summary>
        /// Agrega una cuenta.
        /// </summary>
        /// <param name="cuenta">Cuenta que se desea agregar.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Cuenta> AgregarCuenta(Cuenta cuenta)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.PostAsync(UrlAgregar,
                new StringContent(JsonConvert.SerializeObject(cuenta),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Cuenta>(await respuesta.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Actualiza los datos de una cuenta.
        /// </summary>
        /// <param name="cuenta">Cuenta modificada.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<Cuenta> ActualizarCuenta(Cuenta cuenta)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(cuenta),
                Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Cuenta>(await respuesta.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Elimina una cuenta mediante su codigo.
        /// </summary>
        /// <param name="cuentaCod">Codigo de la cuenta a eliminar.</param>
        /// <returns>Respuesta del servidor.</returns>
        public async Task<string> EliminarCuenta(string cuentaCod)
        {
            HttpClient cliente = Usuario.ObtenerCliente();
            var respuesta = await cliente.DeleteAsync(Url + cuentaCod);
            return JsonConvert.DeserializeObject<string>(await respuesta.Content.ReadAsStringAsync());
        }
        #endregion
    }
}
