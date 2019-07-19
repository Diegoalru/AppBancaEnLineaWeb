using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Pago
    {
        #region Variables
        public int PAG_CODIGO { get; set; }
        public int SER_CODIGO { get; set; }
        public int CUE_CODIGO { get; set; }
        public DateTime PAG_FECHA { get; set; }
        public string PAG_MONEDA { get; set; }
        public decimal PAG_MONTO { get; set; }
        /// <summary>
        /// ¡No usar! Falta la logica de la variable.
        /// </summary>
        /// <see cref="https://github.com/Diegoalru/AppBancaEnLinea/blob/master/AppBancaEnLinea/AppBancaEnLinea/Models/Pago.cs"/>
        public string PAG_DESCRIPCION_label { get; }
        #endregion

        #region Constructor
        public Pago(){}
        #endregion
    }
}
