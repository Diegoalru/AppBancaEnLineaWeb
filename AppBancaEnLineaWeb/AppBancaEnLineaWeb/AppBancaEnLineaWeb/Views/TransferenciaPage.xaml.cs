using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppBancaEnLineaWeb.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransferenciaPage : ContentPage
	{
		public TransferenciaPage ()
		{
			InitializeComponent ();
		}
        
        

        /// <summary>
        /// Boton para redirigir la app a la lista de transferencias.
        /// </summary>
        private void Btn_ListaTransferencias(object sender, EventArgs e)
        {
            App.Current.MainPage = new TransferenciaPageList();
        }
    }
}