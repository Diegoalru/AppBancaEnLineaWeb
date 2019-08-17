using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLineaWeb.Controllers;
using AppBancaEnLineaWeb.Models;
using AppBancaEnLineaWeb.Views;

namespace AppBancaEnLineaWeb.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransferenciaPageList : ContentPage
	{
        #region Variables
        private TransferenciasManager transferenciasManager = new TransferenciasManager();
        private List<Transferencia> transferenciasList = new List<Transferencia>();
        #endregion

        #region Constructor
        public TransferenciaPageList()
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
                IEnumerable<Transferencia> transferencias = await transferenciasManager.ObtenerTransferencias();
                foreach (var item in transferencias)
                {
                    transferenciasList.Add(item);
                }
                TransferenciaList.ItemsSource = transferenciasList;
                TransferenciaList.BindingContext = transferenciasList;
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
                await DisplayAlert("Error", "Ha ocurrido un error al mostrar las transferencias.", "OK");
            }
        }

        /// <summary>
        /// Boton para regresar a la pantalla de TransferenciaPage.
        /// </summary>
        private void Btn_Regresar_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainTabbedPage();
        }
        #endregion

        private void Btn_RefrescarPantalla(object sender, EventArgs e)
        {
            CargaDatos();
        }
    }
}