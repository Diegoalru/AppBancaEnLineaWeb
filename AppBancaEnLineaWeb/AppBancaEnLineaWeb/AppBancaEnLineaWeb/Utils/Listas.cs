using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppBancaEnLineaWeb.Controllers;
using AppBancaEnLineaWeb.Models;
using AppBancaEnLineaWeb.Views;

namespace AppBancaEnLineaWeb.Utils
{
    class Listas
    {
        private ServiciosManager serviciosManager = new ServiciosManager();

        public async List<Servicio> GetServicios()
        {
            List<Servicio> ListaServicios = null;
            IEnumerable<Servicio> IServicios = await serviciosManager.ObtenerServicios();
            foreach (var item in IServicios)
            {
                ListaServicios.Add(item);
            }
            return ListaServicios;
        }
    }
}
