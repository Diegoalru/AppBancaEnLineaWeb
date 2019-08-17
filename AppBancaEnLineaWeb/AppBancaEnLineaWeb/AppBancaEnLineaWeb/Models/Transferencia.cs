using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Transferencia
    {
        #region Variables
        public int TRA_CODIGO { get; set; }
        public int CUO_CODIGO { get; set; }
        public int CUD_CODIGO { get; set; }
        public string TRA_DESCRIPCION { get; set; }
        public DateTime TRA_FECHA { get; set; }
        public string CUO_MONEDA { get; set; }
        public string CUD_MONEDA { get; set; }
        public decimal TRA_MONTO_ORIGEN { get; set; }
        public decimal TRA_MONTO_FINAL { get; set; }
        public string TRA_DESCRIPCION_label
        {
            get
            {
                return string.Format("Transferencia #{0} | Cuenta Origen #{1} | MonedaO: {2} |  Cuenta Destino: {3} | MonedaD: {4} |MontoF: {5} | MontoF: {6} |Fecha: {7}",
                    TRA_CODIGO
                    , CUO_CODIGO
                    , CUO_MONEDA
                    , CUD_CODIGO
                    , CUD_MONEDA
                    , TRA_MONTO_ORIGEN
                    , TRA_MONTO_FINAL
                    , TRA_FECHA.ToString("dd/MM/yyyy  hh:mm"));
            }
        }
        #endregion

        #region Constructor
        public Transferencia()
        {   }
        #endregion
    }
}
