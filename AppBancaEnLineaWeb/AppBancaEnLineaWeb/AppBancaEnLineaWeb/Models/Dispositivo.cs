using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AppBancaEnLineaWeb.Models
{
    public class Dispositivo
    {
        #region Variables
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string DeviceName { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string Idiom { get; set; }
        public string DeviceType { get; set; }
        #endregion

        #region Constructor
        public Dispositivo(){}
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para validar conexion a internet.
        /// </summary>
        /// <returns>Retorna si existe conexion a Internet.</returns>
        public bool ValidarConexionInternet()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }
        #endregion
    }
}