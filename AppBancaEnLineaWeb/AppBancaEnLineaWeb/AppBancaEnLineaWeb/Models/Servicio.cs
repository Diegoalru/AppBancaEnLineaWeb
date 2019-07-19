using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Servicio
    {
        #region Variables
        public int SER_CODIGO { get; set; }
        public string SER_DESCRIPCION { get; set; }
        public string SER_ESTADO { get; set; }
        #endregion

        #region Constructor
        public Servicio(){}
        #endregion
    }
}
