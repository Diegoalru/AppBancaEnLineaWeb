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
        private Dispositivo dispositivo = new Dispositivo();

        #region Constructor
        public LoginPage ()
		{
			InitializeComponent ();

            //Dia de la presentación descomentar

            //Txt_Username.Text = string.Empty;
            //Txt_Password.Text = string.Empty;

            Txt_Username.Text = "jperez";
            Txt_Password.Text = "123456";
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Inicio de sesión. ¡¡Cambiar!!
        /// </summary>
        private async void Btn_Login_Clicked(object sender, System.EventArgs e)
        {
            if (VerificaForm())
            {
                //Validamos que los datos esten completos.
                bool ValidacionUsername = VerificaCampo(Txt_Username.Text);
                bool ValidacionPassword = VerificaCampo(Txt_Password.Text);

                #region Mostramos mensaje de error para el dato incompleto.
                if (ValidacionUsername)
                {
                    await DisplayAlert("Validación de ingreso.", "El campo de usuario no puede estar vacio.", "OK");
                }
                if (ValidacionPassword)
                {
                    await DisplayAlert("Validación de ingreso.", "El campo de la contraseña no puede estar vacio.", "OK");
                }
                #endregion
                if (!(ValidacionUsername && ValidacionPassword))
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
                }
                else
                {
                    await DisplayAlert("Error de conexion", "No hay conexion con internet", "OK");
                }
            }
            else
            {
                await DisplayAlert("Validación de ingreso.", "Debes proporcionar los datos solicitados.", "OK");
            }
            
        }

        /// <summary>
        /// Verifica que se encuentren los datos necesarios para iniciar sesión.
        /// </summary>
        /// <returns>True en caso de que esten todos los datos. False en caso de que falte algun dato.</returns>
        private bool VerificaForm()
        {
            string Username = Txt_Username.Text;
            string Password = Txt_Password.Text;

            if (Username.Equals("") && Password.Equals(""))
                return false;

            return true;
        }

        /// <summary>
        /// Verifica que el campo no este vacio o nulo.
        /// </summary>
        /// <param name="Campo">Cadena de texto que desea verificar.</param>
        /// <returns>True en caso de que este vacio o nulo. False en caso de tener información.</returns>
        private bool VerificaCampo(string Campo)
        {
            if (Campo.Equals(null) || Campo.Equals(""))
                return true;

            return false;
        }

        /// <summary>
        /// Botón sin función.
        /// Preguntar al profe cuando agregará la opción de agregar user.
        /// </summary>
        [Obsolete("No usar hasta no obtener la URL del API para crear usuarios", true)]
        private void Btn_Register_Clicked(object sender, EventArgs e)
        {

        }
        #endregion
    }
}