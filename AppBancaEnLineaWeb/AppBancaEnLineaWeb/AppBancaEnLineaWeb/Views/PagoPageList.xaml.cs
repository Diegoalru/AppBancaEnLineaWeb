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
	public partial class PagoPageList : ContentPage
	{
		public PagoPageList ()
		{
			InitializeComponent ();
		}

        private void Btn_Regresar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}