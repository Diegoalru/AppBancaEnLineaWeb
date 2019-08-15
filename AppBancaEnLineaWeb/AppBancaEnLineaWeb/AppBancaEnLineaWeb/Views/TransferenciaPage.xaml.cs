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

        private void Btn_ListaTransferencias(object sender, EventArgs e)
        {
            /*
             * Crear link con la pagina TransferenciaPageList.
             */
            //App.Current.MainPage 
        }
    }
}