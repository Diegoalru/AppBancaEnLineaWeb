using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Transferencia
    {
        #region Variables
        public int TRA_CODIGO { get; set; }
        public int TRA_CUENTA_ORIGEN { get; set; }
        public int TRA_CUENTA_DESTINO { get; set; }
        public string TRA_DESCRIPCION { get; set; }
        public string TRA_ESTADO { get; set; }
        public DateTime TRA_FECHA { get; set; }
        public decimal TRA_MONTO { get; set; }

        public string TRA_DESCRIPCION_label
        {
            get
            {
                return string.Format("Transferencia #{0} | Cuenta Origen #{1} | Cuenta Destino: #{2} | Monto: {3:N2} | Fecha: {4}"
                , TRA_CODIGO
                , TRA_CUENTA_ORIGEN
                , TRA_CUENTA_DESTINO
                , TRA_MONTO
                , TRA_FECHA.ToString("dd/MM/yyyy"));
            }
        }
        #endregion

        #region Constructor
        public Transferencia()
        {
        }
        #endregion
    }
}
