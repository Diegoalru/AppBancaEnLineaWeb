using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLineaWeb.Models;
using AppBancaEnLineaWeb.Controllers;
using AppBancaEnLineaWeb.Views;

namespace AppBancaEnLineaWeb
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            //Txt_Username.Text = string.Empty;
            //Txt_Password.Text = string.Empty;
            Txt_Username.Text = "jperez";
            Txt_Password.Text = "123456";
        }

        private Dispositivo dispositivo = new Dispositivo();
        
        /// <summary>
        /// Inicio de sesión. ¡¡Cambiar!!
        /// </summary>
        private async void Btn_Login_Clicked(object sender, System.EventArgs e)
        {

            if (dispositivo.ValidarConexionInternet())
            {
                var manager = new UsuarioManager();
                App.usuarioActual = await manager.Validar(Txt_Username.Text, Txt_Password.Text);
                if (App.usuarioActual != null)
                {
                    var jwthandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    App.usuarioActual.Token = jwthandler.ReadToken(App.usuarioActual.TOKEN);

                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Error al ingresar", "Credenciales inválidas", "Ok");
                }
            }
            else
                await DisplayAlert("Error de conexion", "No hay conexion con internet", "Ok");
        }

        private void Btn_Register_Clicked(object sender, EventArgs e)
        {

        }
    }
}