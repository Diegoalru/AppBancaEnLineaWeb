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
	public partial class ResumePage : ContentPage
	{
		public ResumePage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// Volver al Login.
        /// </summary>
        private void Btn_Resume_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}