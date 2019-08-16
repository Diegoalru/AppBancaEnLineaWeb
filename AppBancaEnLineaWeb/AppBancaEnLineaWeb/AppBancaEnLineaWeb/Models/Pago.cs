#define Pag_label
//#undef Pag_label
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public string PAG_DESCRIPCION_label
        {
            get
            {
            #if Pag_label
                string Servicio, Label;
                foreach (var item in serviciosList)
                {
                    if (item.SER_CODIGO == SER_CODIGO)
                    {
                        Servicio = item.SER_DESCRIPCION.ToString();
                        Label = string.Format("Pago: {0} | Servicio: {1} | Saldo: {2} {3:N2}",PAG_CODIGO,Servicio,
                                (PAG_MONEDA.Trim().Equals("DOL") ? "$" : PAG_MONEDA.Trim().Equals("COL") ? "₡" : "€"),PAG_MONTO);
                    }
                }
                Label = string.Format("Cuenta: {0}|Monto: {1:N2}|Fecha: {2}",
                    CUE_CODIGO, PAG_MONTO, PAG_FECHA.ToString("dd/MM/yyyy"));
                return Label;
            #else
                return string.Format("Cuenta: {0}|Monto: {1:N2}|Fecha: {2}",
                    CUE_CODIGO, PAG_MONTO, PAG_FECHA.ToString("dd/MM/yyyy"));
            #endif
            }


        }

        #endregion

        #region Constructor
        public Pago()
        {
            //CargarDatos();
        }
        #endregion


        #region Metodos
        private static List<Servicio> serviciosList = new List<Servicio>();
        private Controllers.ServiciosManager serviciosManager = new Controllers.ServiciosManager();
        public async void CargarDatos()
        {
            //Lista de los servicios.
            IEnumerable<Servicio> servicios = await serviciosManager.ObtenerServicios();
            foreach (var item in servicios)
            {
                serviciosList.Add(item);
            }
        }
        #endregion
    }
}
