using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppBancaEnLineaWeb
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            /*
             * FIXME: 
             *      1. Crear la vista LoginPage. 
             *      2. Una ves creada LoginPage, eliminar la siguiente linea (19). 
             */
            MainPage = new MainPage();
            //Una ves hecho lo anterior eliminar MainPage del proyecto.
        }

        protected override void OnStart()
        {
            //FIXME: Habilitar cuando este creada la vista LoginPage().
            //MainPage = new LoginPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            //FIXME: Habilitar cuando este creada la vista TestPage() en la carpeta Views.
            //MainPage = new Views.TestPage();
        }
    }
}
