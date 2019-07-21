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
        public decimal TRA_MONTO { get; set; }
        #endregion

        #region Constructor
        public Transferencia()
        {
        }
        #endregion
    }
}
