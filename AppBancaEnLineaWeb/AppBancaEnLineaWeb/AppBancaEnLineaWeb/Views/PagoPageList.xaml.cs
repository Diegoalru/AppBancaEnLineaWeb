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
    public partial class PagoPageList : ContentPage
    {
        #region Variables
        private PagoManager pagoManager = new PagoManager();
        #endregion

        #region Constructor
        public PagoPageList()
        {
            InitializeComponent();
            CargaDatos();
        }
        #endregion


        #region Metodos
        private async void CargaDatos()
        {
            try
            {
                //Obtenermos la lista de las cuentas del usuario.
                IEnumerable<Pago> pagos = await pagoManager.ObtenerPagos(App.usuarioActual.USU_CODIGO.ToString());
                PagoList.ItemsSource = pagos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Boton para regresar al TabbedPage.
        /// </summary>
        private void Btn_Regresar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainTabbedPage();
        }
        #endregion

        private void Btn_RefrescarPantalla(object sender, EventArgs e)
        {
            CargaDatos();
        }
    }
}