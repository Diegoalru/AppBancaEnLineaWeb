using System;
using System.Collections.Generic;
using System.Text;

namespace AppBancaEnLineaWeb.Models
{
    public class Cuenta
    {

        #region Variables
        public int CUE_CODIGO { get; set; }
        public int USU_CODIGO { get; set; }
        public string CUE_DESCRIPCION { get; set; }
        public string CUE_MONEDA { get; set; }
        public decimal CUE_SALDO { get; set; }
        public string CUE_ESTADO { get; set; }
        public string CUE_DESCRIPCION_label
        {
            get
            {
                return string.Format("Cuenta: {0} - {1} | Saldo: {2} {3:N2}",
                    CUE_CODIGO,
                    CUE_DESCRIPCION,
                    (CUE_MONEDA.Trim().Equals("DOL") ? "$" : CUE_MONEDA.Trim().Equals("COL") ? "₡" : "€"),
                    CUE_SALDO);
            }
        }
        public string CUE_SALDO_label
        {
            get
            {
                return string.Format("Saldo: {0} {1:N2}",
                    (CUE_MONEDA.Trim().Equals("DOL") ? "$" : CUE_MONEDA.Trim().Equals("COL") ? "₡" : "€"),
                    CUE_SALDO);
            }
        }
        #endregion

        #region Constructor
        public Cuenta()
        { }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return string.Format("Cuenta:\nDescripcion: {0}\nMoneda: {1}\nSaldo: {2}\nEstado: {3}", CUE_DESCRIPCION, CUE_MONEDA, CUE_SALDO, CUE_ESTADO);
        }
        #endregion

    }
}