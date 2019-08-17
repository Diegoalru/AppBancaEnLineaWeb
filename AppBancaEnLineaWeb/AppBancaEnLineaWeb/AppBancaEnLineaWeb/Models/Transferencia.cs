using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Transferencia
    {
        #region Variables
        public int TRA_CODIGO { get; set; }
        public int CUE_ORIGEN { get; set; }
        public int CUE_DSTINO { get; set; }
        public string TRA_DESCRIPCION { get; set; }
        public char TRA_ESTADO { get; set; }
        public DateTime TRA_FECHA { get; set; }
        public decimal TRA_MONTO_ORIGEN { get; set; }
        public decimal TRA_MONTO_FINAL { get; set; }
        public string PAG_DESCRIPCION_label
        {
            get
            {
                return string.Format("Transferencia #{0} | Cuenta Origen #{1} | Cuenta Destino: {2} | Monto: {3} | Fecha: {4}",
                    TRA_CODIGO
                    , CUE_ORIGEN
                    , CUE_DSTINO
                    , TRA_MONTO_ORIGEN
                    , TRA_MONTO_FINAL
                    , TRA_FECHA.ToString("dd/MM/yyyy  hh:mm"));
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
