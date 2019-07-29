using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLineaWeb.Controllers;
using AppBancaEnLineaWeb.Models;

namespace AppBancaEnLineaWeb.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CuentaPage : ContentPage
    {
        /// <summary>
        /// Se usa para actualizar una cuenta. Esta posee los datos iniciales de la cuenta.
        /// </summary>
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
        /// <param name="cuenta">Cuenta que se va a modificar.</param>
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

        /// <summary>
        /// Regresa al menu pricipal.
        /// </summary>
        private void RegresarTapped(Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }

        /// <summary>
        /// Actualizar una cuenta.
        /// </summary>
        private async void ActualizarTapped(Object sender, System.EventArgs e)
        {
            try
            {
                CuentaManager cuentaManager = new CuentaManager();
                Cuenta cuentaActualizada = new Cuenta();

                #region Cargar datos fijos
                string moneda = string.Empty;
                switch (Pkr_Moneda.SelectedItem.ToString())
                {
                    case "Colones":
                        moneda = "COL";
                        break;
                    case "Dolares":
                        moneda = "DOL";
                        break;
                    default:
                        moneda = "EUR";
                        break;
                }
                #endregion

                //Cuenta que almacena los cambios nuevos.
                Cuenta cuentaNueva = new Cuenta()
                {
                    CUE_CODIGO  =   Convert.ToInt32(Txt_Codigo.Text),
                    USU_CODIGO  =   App.usuarioActual.USU_CODIGO,
                    CUE_DESCRIPCION = Txt_Descripcion.Text,
                    CUE_MONEDA  =   moneda,
                    CUE_SALDO   =   Convert.ToDecimal(Txt_Saldo.Text),
                    CUE_ESTADO  =   Pkr_Estado.SelectedItem.ToString().Substring(0, 1)
                };

                //Datos de la cuenta vieja.
                string cuentaAnterior = string.Format("Cuenta Anterior:\nDescripcion: {0}\nMoneda: {1}\nSaldo: {2}\nEstado: {3}",
                    this.cuentaVieja.CUE_DESCRIPCION, this.cuentaVieja.CUE_MONEDA, this.cuentaVieja.CUE_SALDO, this.cuentaVieja.CUE_ESTADO);

                //Datos actualizados.
                string cuentaActual = string.Format("Cuenta Actualizada:\nDescripcion: {0}\nMoneda: {1}\nSaldo: {2}\nEstado: {3}",
                    cuentaNueva.CUE_DESCRIPCION, cuentaNueva.CUE_MONEDA, cuentaNueva.CUE_SALDO, cuentaNueva.CUE_ESTADO);

                //Validar la respuesta del usuario.
                bool resultado = await DisplayAlert("Verifique los datos", cuentaAnterior + "\n" + cuentaActual + "\n¿Desea Continuar?", "Aceptar", "Cancelar");
                if (resultado)
                {
                    await DisplayAlert("Mensaje", "Cuenta actualizada correctamente", "Aceptar");
                    cuentaActualizada = await cuentaManager.ActualizarCuenta(cuentaNueva);
                    Application.Current.MainPage = new MainPage();
                }
                else { /* No hacer codigo aqui. */ }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", "Error: " + ex.Message, "Aceptar");
            }
        }

        /// <summary>
        /// Metodo para agregar una cuenta.
        /// </summary>
        private async void AgregarTapped(Object sender, System.EventArgs e)
        {
            try
            {
                CuentaManager cuentaManager = new CuentaManager();
                //Preguntar para que sirve esta variable.
                Cuenta cuentaIngresada = new Cuenta();

                #region Cargar datos fijos.
                string moneda = string.Empty;
                switch (Pkr_Moneda.SelectedItem.ToString())
                {
                    case "Colones":
                        moneda = "COL";
                        break;
                    case "Dolares":
                        moneda = "DOL";
                        break;
                    default:
                        moneda = "EUR";
                        break;
                }
                #endregion

                //Cuenta que va a almacenar los datos
                Cuenta cuenta = new Cuenta()
                {
                    USU_CODIGO = App.usuarioActual.USU_CODIGO,
                    CUE_DESCRIPCION = Txt_Descripcion.Text,
                    CUE_MONEDA = moneda,
                    CUE_SALDO = Convert.ToDecimal(Txt_Saldo.Text),
                    CUE_ESTADO =
                    Pkr_Estado.SelectedItem.ToString().Substring(0, 1)

                };

                //Validar la creacion de la cuenta.
                bool respuesta = await DisplayAlert("Agregar Cuenta", "Desea agregar la cuenta:\n" + cuenta.ToString(), "Agregar", "Cancelar");
                if (respuesta)
                {
                    //Agregar la cuenta.
                    cuentaIngresada = await cuentaManager.AgregarCuenta(cuenta);
                    /*
                     * FIXME:
                     *  1. Validar que realmente se creo la cuenta.
                     *      Opciones:
                     *          a. Validar con la respuesta del servidor.
                     *          b. Llamar otro metodo para validar la creación de la cuenta.
                     */
                    //Mensaje de exito.
                    await DisplayAlert("Mensaje", "Cuenta agregada correctamente", "Aceptar");
                }
                else { /* No hacer codigo aqui. */ }
                Limpiar();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso",
                    "Error: " + ex.Message,
                    "Aceptar");
            }
        }
        #endregion
    }
}