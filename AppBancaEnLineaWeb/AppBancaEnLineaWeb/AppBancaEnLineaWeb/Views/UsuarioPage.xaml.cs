using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLineaWeb.Controllers;
using AppBancaEnLineaWeb.Models;

namespace AppBancaEnLineaWeb.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UsuarioPage : ContentPage
	{
		public UsuarioPage ()
		{
			InitializeComponent();
            CargaDatos();
		}

        /// <summary>
        /// Carga los datos del usuario.
        /// </summary>
        private void CargaDatos()
        {
            var manager = App.usuarioActual;
            Lbl_Usuario.Text += ": " + manager.USU_USERNAME;
            Lbl_Nombre.Text += ": " + manager.USU_NOMBRE;
            Lbl_Cedula.Text += ": " + manager.USU_IDENTIFICACION;
            Lbl_Correo.Text += ": " + manager.USU_EMAIL;
            Lbl_Date.Text = "Fecha Nacimiento: " + manager.USU_FEC_NAC.ToShortDateString();
        }

        /// <summary>
        /// Volver al Login.
        /// </summary>
        private void Btn_Resume_Clicked(object sender, EventArgs e)
        {
            App.usuarioActual = null;
            Application.Current.MainPage = new LoginPage();
        }

        /// <summary>
        /// Cierra la sesión actual.
        /// </summary>
        public async void CerrarSesionTapped(object sender, EventArgs e)
        {
            try
            {
                bool respuesta = await DisplayAlert("Cerrar Sesión", "¿Desea cerrar la sesión?", "Aceptar", "Cancelar");
                if (respuesta)
                {
                    Application.Current.MainPage = new LoginPage();
                    App.usuarioActual = null;
                }
                else { /* No hacer codigo aqui. */ }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}