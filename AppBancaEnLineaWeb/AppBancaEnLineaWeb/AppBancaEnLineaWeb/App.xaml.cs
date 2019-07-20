﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppBancaEnLineaWeb.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppBancaEnLineaWeb
{
    public partial class App : Application
    {
        static public Usuario usuarioActual = new Usuario();

        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            MainPage = new LoginPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            //FIXME: Habilitar cuando este creada la vista ResumePage() en la carpeta Views.
            //MainPage = new Views.ResumePage();
        }
    }
}
