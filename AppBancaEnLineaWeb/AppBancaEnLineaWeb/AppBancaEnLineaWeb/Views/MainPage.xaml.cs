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
        private CuentaManager cuentaManager = new CuentaManager();

        #region Constructor
        public MainPage()
        {
            InitializeComponent();
            CargaUser();
            CargarCuentas();
        }
        #endregion

        #region Cuentas
        /// <summary>
        /// Muestra el usuario del usuario actual.
        /// </summary>
        public void CargaUser()
        {
            string user = App.usuarioActual.USU_USERNAME;
        }

        /// <summary>
        /// Muestra la lista de cuentas del usuario.
        /// </summary>
        private async void CargarCuentas()
        {
            try
            {
                //Obtiene las cuentas desde CuentaManager y las agrega a una lista.
                IEnumerable<Cuenta> cuentas = await cuentaManager.ObtenerCuentas(App.usuarioActual.USU_CODIGO.ToString());
                CuentasList.ItemsSource = cuentas;
                CuentasList.BindingContext = cuentas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo usado para redireccionar a CuentaPage.
        /// </summary>
        private void AgregarTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new CuentaPage();
        }

        /// <summary>
        /// Metodo usado para cambiar la informacion de una cuenta.
        /// </summary>
        private void ModificarTapped(object sender, EventArgs e)
        {
            //Castemos el objeto selecionado para poder agregarlo en un objeto de tipo Cuenta.
            Cuenta cuenta = (Cuenta)CuentasList.SelectedItem;
            if (cuenta != null)
                Application.Current.MainPage = new CuentaPage(cuenta);
            else
                DisplayAlert("Advertencia", "Si desea modificar una cuenta, debes seleccionarla primero.", "OK");
        }

        /// <summary>
        /// Elimina una cuenta.
        /// </summary>
        private async void EliminarTapped(object sender, EventArgs e)
        {
            try
            {
                Cuenta cuenta = (Cuenta)CuentasList.SelectedItem;

                bool respuesta = await DisplayAlert("Advertencia", "Desea eliminar la cuenta " + cuenta.CUE_CODIGO + ".", "Aceptar", "Cancelar");
                if (respuesta)
                {
                    await cuentaManager.EliminarCuenta(cuenta.CUE_CODIGO.ToString());
                    await DisplayAlert("Mensaje", "Cuenta eliminada correctamente", "OK");
                    CargarCuentas();
                }
                else { /* No hacer codigo aqui. */ }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", "Error: " + ex.Message, "Aceptar");
            }
        }
        #endregion

        #region Pagos
        /// <summary>
        /// Carga la pantalla de PagoPage.
        /// </summary>
        private void Btn_PagarServicio_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PagoPage();
        }

        /// <summary>
        /// Carga la pantalla PagoPageList.
        /// </summary>
        private void Btn_PagoList_Clicked(object sender, EventArgs e)
        {

        }

        #endregion

        #region Otros metodos.
        [Obsolete("Este metodo no se usa por falta del botón en el MainPage.", false)]
        private void Btn_RefrescarPantalla(object sender, EventArgs e)
        {
        }
        #endregion

    }
}