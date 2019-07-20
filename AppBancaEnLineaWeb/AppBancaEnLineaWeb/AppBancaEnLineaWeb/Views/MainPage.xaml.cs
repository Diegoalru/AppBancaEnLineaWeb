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
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            CargaUser();
            CargarCuentas();
		}

        private CuentaManager cuentaManager = new CuentaManager();

        /// <summary>
        /// Muestra la lista de cuentas del usuario.
        /// </summary>
        private async void CargarCuentas()
        {
            try
            {
                IEnumerable<Cuenta> cuentas = await cuentaManager.GetCuentas(App.usuarioActual.USU_CODIGO.ToString());
                CuentasList.ItemsSource = cuentas;
                CuentasList.BindingContext = cuentas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Muestra el usuario del usuario actual.
        /// </summary>
        public void CargaUser()
        {
            string user = App.usuarioActual.USU_USERNAME;
            Lbl_Username.Text = "Usuario: " + user;
        }

        private void Btn_PagarServicio_Clicked(object sender, EventArgs e)
        {

        }

        private void Btn_PagoList_Clicked(object sender, EventArgs e)
        {

        }

        private void AgregarTapped(object sender, EventArgs e)
        {

        }

        private void ModificarTapped(object sender, EventArgs e)
        {

        }

        private void EliminarTapped(object sender, EventArgs e)
        {

        }

        private void Btn_RefrescarPantalla(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Cierra la sesión actual.
        /// </summary>
        public async void CerrarSesionTapped(object sender, EventArgs e)
        {
            try
            {
                bool respuesta = await DisplayAlert("Cerrar Sesión", "¿Desea cerrar la sesión?", "OK", "Cancelar");
                if (respuesta)
                {
                    Application.Current.MainPage = new LoginPage();
                    App.usuarioActual = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}