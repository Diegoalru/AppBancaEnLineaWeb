using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLineaWeb.Controllers;
using AppBancaEnLineaWeb.Models;
using AppBancaEnLineaWeb.Views;

namespace AppBancaEnLineaWeb.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoPage : ContentPage
    {
        #region Variables
        private List<Cuenta> cuentasList = new List<Cuenta>();
        private List<Servicio> serviciosList = new List<Servicio>();
        private CuentaManager cuentaManager = new CuentaManager();
        private ServiciosManager serviciosManager = new ServiciosManager();
        #endregion

        public PagoPage()
        {
            InitializeComponent();
            Lbl_Date.Text = "Fecha Actual: " + DateTime.Now.Date.ToShortDateString();
            CargarDatos();
        }

        public async void CargarDatos()
        {
            //Lista de los servicios.
            IEnumerable<Servicio> servicios = await serviciosManager.ObtenerServicios();
            foreach (var item in servicios)
            {
                serviciosList.Add(item);
            }
            Pkr_Servicios.ItemsSource = serviciosList;

            IEnumerable<Cuenta> cuentas = await cuentaManager.ObtenerCuentas(App.usuarioActual.USU_CODIGO.ToString());
            //Lista de las cuentas del usuario.
            foreach (var item in cuentas)
            {
                /*
                if (item.USU_CODIGO == App.repositorioUsuario.GetUsuario().USU_CODIGO)
                {
                    IEnumerable<Cuenta> cuentas = await cuentaManager.GetCuentas(App.usuarioActual.USU_CODIGO.ToString());
                }
                */
                cuentasList.Add(item);
            }
            Pkr_Cuentas.ItemsSource = cuentasList;
        }

        private void Btn_Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void Btn_Pagar_Clicked(object sender, EventArgs e)
        {
            //CODE
        }



        private void Limpiar()
        {
            Pkr_Servicios.SelectedItem = 0;
            Pkr_Cuentas.SelectedItem = 0;
            Txt_Monto.Text = "";
        }

        /// <summary>
        /// Verifica que el campo no este vacio o nulo.
        /// </summary>
        /// <param name="Campo">Cadena de texto que desea verificar.</param>
        /// <returns>True en caso de que este vacio o nulo. False en caso de tener información.</returns>
        private bool VerificaCampo(string Campo)
        {
            if (Campo == null || Campo.Equals(""))
                return true;

            return false;
        }
    }
}