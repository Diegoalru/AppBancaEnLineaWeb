using System;

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
        public string PAG_DESCRIPCION_label
        {
            get
            {
                return string.Format("Cuenta: {0} | Monto: {1:N2} | Fecha: {2}",
                    CUE_CODIGO
                    , PAG_MONTO
                    , PAG_FECHA.ToString("dd/MM/yyyy"));
            }
        }
        #endregion

        #region Constructor
        public Pago()
        { }
        #endregion
    }
}
