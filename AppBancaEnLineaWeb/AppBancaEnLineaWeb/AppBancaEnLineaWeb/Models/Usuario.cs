using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Usuario
    {
        #region Variables
        public int USU_CODIGO { get; set; }
        public string USU_IDENTIFICACION { get; set; }
        public string USU_NOMBRE { get; set; }
        public string USU_USERNAME { get; set; }
        public string USU_PASSWORD { get; set; }
        public string USU_EMAIL { get; set; }
        public DateTime USU_FEC_NAC { get; set; }
        public string USU_ESTADO { get; set; }
        public SecurityToken Token { get; set; }
        public string TOKEN { get; set; }
        #endregion

        #region Constructores
        public Usuario()
        { }

        public Usuario(int code, string user)
        { this.USU_CODIGO = code; this.USU_USERNAME = user; }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return string.Format("Verifique los datos:\nCedula: {0}\nNombre: {1}\nUsuario: {2}\nEmail: {3}\n", USU_IDENTIFICACION, USU_NOMBRE, USU_USERNAME, USU_EMAIL);
        }
        #endregion
    }
}
