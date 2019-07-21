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
	public partial class CuentaPage : ContentPage
	{
        private Cuenta cuentaVieja = new Cuenta();

        #region Constructores

        /// <summary>
        /// Constructor al crear una cuenta.
        /// </summary>
        public CuentaPage()
        {
            InitializeComponent();
            Txt_Codigo.IsVisible = false;
            Btn_Modificar.IsVisible = false;
        }

        /// <summary>
        /// Constructor para actualizar una cuenta.
        /// </summary>
        /// <param name="cuenta">Cuenta queva a modificarse.</param>
        public CuentaPage(Cuenta cuenta)
        {
            InitializeComponent();
            Btn_Agregar.IsVisible = false;
            Txt_Codigo.IsReadOnly = true;
            Txt_Codigo.Text = cuenta.CUE_CODIGO.ToString();
            Txt_Descripcion.Text = cuenta.CUE_DESCRIPCION.ToString();
            Txt_Saldo.Text = cuenta.CUE_SALDO.ToString();
            Pkr_Estado.SelectedItem = (cuenta.CUE_ESTADO.Equals("A") ? "Activo" : "Inactivo");
            string moneda;
            switch (cuenta.CUE_MONEDA)
            {
                case "DOL":
                    moneda = "DOLARES";
                    break;
                case "COL":
                    moneda = "COLONES";
                    break;
                default:
                    moneda = "EUROS";
                    break;
            }
            Pkr_Moneda.SelectedItem = moneda;
            this.cuentaVieja = cuenta;
        }

        #endregion

        #region Metodos
        /*
        private async void AgregarTapped(Object sender, System.EventArgs e)
        {
            try
            {
                string moneda = string.Empty;
                switch (Pkr_Moneda.SelectedItem.ToString())
                {
                    case "DOLARES":
                        moneda = "DOL";
                        break;
                    case "COLONES":
                        moneda = "COL";
                        break;
                    default:
                        moneda = "EUR";
                        break;
                }

                Cuenta cuenta = new Cuenta
                {
                    CUE_CODIGO = 1,
                    USU_CODIGO = App.repositorioUsuario.GetUsuario().USU_CODIGO,
                    CUE_DESCRIPCION = Txt_Descripcion.Text,
                    CUE_MONEDA = moneda,
                    CUE_SALDO = Convert.ToDecimal(Txt_Saldo.Text),
                    CUE_ESTADO = Pkr_Estado.SelectedItem.ToString().Substring(0, 1)
                };

                bool resultado = await DisplayAlert("Cuenta", "¿Desea agregar la siguiente cuenta?\n" + cuenta.ToString(), "OK", "Cancel");
                if (resultado)
                {
                    App.repositorioCuenta.AgregarCuenta(cuenta);
                    await DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK");
                    Limpiar();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Verifica que todos los datos esten completos.", "Ok");
            }
        }

        private async void ActualizarTapped(Object sender, System.EventArgs e)
        {
            try
            {
                string moneda = string.Empty;
                switch (Pkr_Moneda.SelectedItem.ToString())
                {
                    case "DOLARES":
                        moneda = "DOL";
                        break;
                    case "COLONES":
                        moneda = "COL";
                        break;
                    default:
                        moneda = "EUR";
                        break;
                }

                Cuenta cuentaNueva = new Cuenta
                {
                    USU_CODIGO = App.repositorioUsuario.GetUsuario().USU_CODIGO,
                    CUE_CODIGO = Convert.ToInt32(Txt_Codigo.Text),
                    CUE_DESCRIPCION = Txt_Descripcion.Text,
                    CUE_MONEDA = moneda,
                    CUE_SALDO = Convert.ToDecimal(Txt_Saldo.Text),
                    CUE_ESTADO = Pkr_Estado.SelectedItem.ToString().Substring(0, 1)
                };

                string cuentaAnterior = string.Format("Cuenta Anterior:\nDescripcion: {0}\nMoneda: {1}\nSaldo: {2}\nEstado: {3}",
                    this.cuentaVieja.CUE_DESCRIPCION, this.cuentaVieja.CUE_MONEDA, this.cuentaVieja.CUE_SALDO, this.cuentaVieja.CUE_ESTADO);
                string cuentaActual = string.Format("Cuenta Actualizada:\nDescripcion: {0}\nMoneda: {1}\nSaldo: {2}\nEstado: {3}",
                    cuentaNueva.CUE_DESCRIPCION, cuentaNueva.CUE_MONEDA, cuentaNueva.CUE_SALDO, cuentaNueva.CUE_ESTADO);

                bool resultado = await DisplayAlert("Verifique los datos", cuentaAnterior + "\n" + cuentaActual + "\n¿Desea Continuar?", "OK", "Cancel");

                if (resultado)
                {
                    await DisplayAlert("SQLite", App.repositorioCuenta.StatusMessage, "OK");
                    App.repositorioCuenta.ActualizaCuenta(cuentaNueva);
                    Application.Current.MainPage = new MainPage();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Verifica que todos los datos esten completos.", "Ok");
            }
        }
        */

        /// <summary>
        /// Limpia todos los Textboxs.
        /// </summary>
        private void Limpiar()
        {
            Txt_Codigo.Text = "";
            Txt_Descripcion.Text = "";
            Txt_Saldo.Text = "";
            Pkr_Estado.SelectedItem = 0;
            Pkr_Moneda.SelectedItem = 0;
        }

        private void RegresarTapped(Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void ActualizarTapped(Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        private void AgregarTapped(Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        #endregion
    }
}