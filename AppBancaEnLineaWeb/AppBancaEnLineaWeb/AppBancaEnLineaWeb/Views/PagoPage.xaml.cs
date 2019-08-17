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
    public partial class PagoPage : ContentPage
    {
        #region Variables
        private List<Cuenta> cuentasList = new List<Cuenta>();
        private List<Servicio> serviciosList = new List<Servicio>();
        private CuentaManager cuentaManager = new CuentaManager();
        private ServiciosManager serviciosManager = new ServiciosManager();
        private PagoManager pagoManager = new PagoManager();
        #endregion

        public PagoPage()
        {
            InitializeComponent();
            Lbl_Date.Text = "Fecha Actual: " + DateTime.Now.Date.ToShortDateString();
            CargarDatos();
        }

        public async void CargarDatos()
        {
            //Lista de los servicios.
            IEnumerable<Servicio> servicios = await serviciosManager.ObtenerServicios();
            foreach (var item in servicios)
            {
                serviciosList.Add(item);
            }
            Pkr_Servicios.ItemsSource = serviciosList;

            IEnumerable<Cuenta> cuentas = await cuentaManager.ObtenerCuentas(App.usuarioActual.USU_CODIGO.ToString());
            //Lista de las cuentas del usuario.
            foreach (var item in cuentas)
            {
                cuentasList.Add(item);
            }
            Pkr_Cuentas.ItemsSource = cuentasList;
        }


        private async void Btn_Pagar_Clicked(object sender, EventArgs e)
        {
            try
            {
                int indexCuenta = Pkr_Cuentas.SelectedIndex;
                int indexServicio = Pkr_Servicios.SelectedIndex;
                bool BCuenta = true;
                bool BServicio = true;

                if (indexCuenta == -1)
                {
                    BCuenta = false;
                    await DisplayAlert("Alerta", "Debes seleccionar una cuenta.", "OK");
                }

                if (indexServicio == -1)
                {
                    BServicio = false;
                    await DisplayAlert("Alerta", "Debes seleccionar un servicio.", "OK");
                }

                if (BCuenta && BServicio)
                {
                    if (!VerificaCampo(Txt_Monto.Text))
                    {
                        decimal monto = Convert.ToDecimal(Txt_Monto.Text);
                        decimal montoFinal = cuentasList[indexCuenta].CUE_SALDO - monto;
                        if (monto > 0)
                        {

                            if (montoFinal >= 0)
                            {
                                Pago pago = new Pago()
                                {
                                    PAG_CODIGO = 1,
                                    CUE_CODIGO = cuentasList[indexCuenta].CUE_CODIGO,
                                    SER_CODIGO = serviciosList[indexServicio].SER_CODIGO,
                                    PAG_MONEDA = cuentasList[indexCuenta].CUE_MONEDA,
                                    PAG_MONTO = monto,
                                    PAG_FECHA = DateTime.Now
                                };
                                string signo = cuentasList[indexCuenta].CUE_MONEDA.Trim().Equals("DOL") ? "$" : cuentasList[indexCuenta].CUE_MONEDA.Trim().Equals("COL") ? "₡" : "€";
                                string msj = string.Format("Comprobación:\nServicio: {0}\nCuenta: {1}\nMonto: {2}{3:N2}\nSaldo despues del pago: {2}{4:N2}", serviciosList[indexServicio].SER_DESCRIPCION, pago.CUE_CODIGO, signo, pago.PAG_MONTO, montoFinal);

                                bool respuesta = await DisplayAlert("Comprobación:", msj, "OK", "Cancelar");
                                if (respuesta)
                                {
                                    Cuenta cuenta = cuentasList[indexCuenta];
                                    cuenta.CUE_SALDO -= monto;
                                    await cuentaManager.ActualizarCuenta(cuenta);
                                    await pagoManager.AgregarPago(pago);
                                    await DisplayAlert("Pago", "Pago completado.", "OK");
                                    Limpiar();
                                }
                            }
                            else
                            {
                                await DisplayAlert("Error", "Dinero insuficiente.\n", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Alerta", "Debes indicar un monto valido.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Debes indicar el monto a pagar.", "OK");
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Error al crear pago.", "OK");
            }
        }



        private void Limpiar()
        {
            Pkr_Servicios.SelectedItem = 0;
            Pkr_Cuentas.SelectedItem = 0;
            Txt_Monto.Text = "";
        }

        private void OnBackPressed()
        {
            DisplayAlert("Alerta", "Back Button Pressed Detected", "OK");
        }

        /// <summary>
        /// Verifica que el campo no este vacio o nulo.
        /// </summary>
        /// <param name="Campo">Cadena de texto que desea verificar.</param>
        /// <returns>True en caso de que este vacio o nulo. False en caso de tener información.</returns>
        private bool VerificaCampo(string Campo)
        {
            if (Campo == null || Campo.Equals(""))
                return true;

            return false;
        }

        [Obsolete("Boton en desuso, gracias al TabbedPage.", false)]
        private void Btn_Volver_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}