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
	public partial class TransferenciaPage : ContentPage
	{
        #region Variables
        private List<Cuenta> cuentasList = new List<Cuenta>();
        private TransferenciasManager transferenciasManager= new TransferenciasManager();
        private CuentaManager cuentaManager = new CuentaManager();
        #endregion


        public TransferenciaPage ()
        {
            InitializeComponent();
            Lbl_Date.Text = "Fecha Actual: " + DateTime.Now.Date.ToShortDateString();
            CargarDatos();
        }

        public async void CargarDatos()
        {
            //Lista de las cuentas del usuario.
            IEnumerable<Cuenta> cuentas = await cuentaManager.ObtenerCuentas(App.usuarioActual.USU_CODIGO.ToString());
            foreach (var item in cuentas)
            {
                cuentasList.Add(item);
            }
            Pkr_CuentaOrigen.ItemsSource = cuentasList;
            Pkr_CuentaDestino.ItemsSource = cuentasList; 
        }

        /// <summary>
        /// Boton para redirigir la app a la lista de transferencias.
        /// </summary>
        private async Task Btn_Transferir_Clicked(object sender, EventArgs e)
        {
            try
            {
                int indexCuentaOrigen = Pkr_CuentaOrigen.SelectedIndex;
                int indexCuentaDestino = Pkr_CuentaDestino.SelectedIndex;
                bool BCOrigen = true;
                bool BCDestiny = true;

                if (indexCuentaOrigen == -1)
                {
                    BCOrigen = false;
                    await DisplayAlert("Alerta", "Debes seleccionar una cuenta de Origen.", "OK");
                }

                if (indexCuentaDestino == -1)
                {
                    BCDestiny = false;
                    await DisplayAlert("Alerta", "Debes seleccionar un cuenta de Destino.", "OK");
                }
                if (BCOrigen && BCDestiny)
                {
                    if (!VerificaCampo(Txt_Monto.Text))
                    {
                        decimal monto = Convert.ToDecimal(Txt_Monto.Text); 
                        decimal operacion = 0;
                        string monedaOrigen = cuentasList[indexCuentaOrigen].CUE_MONEDA;
                        string monedaDestino = cuentasList[indexCuentaDestino].CUE_MONEDA;
                        decimal COL_DOL = 572;
                        decimal COL_EUR = 644;
                        decimal DOL_COL = 559;
                        decimal EUR_COL = 611;

                        if (monto >= 0)
                            {
                                if (monedaOrigen == "COLONES")
                                {
                                    if (monedaDestino == "DOLARES")
                                    {
                                        operacion = monto / COL_DOL;
                                    }

                                    if (monedaDestino == "EUROS")
                                    {
                                        operacion = monto / COL_EUR;
                                    }
                                    else
                                    {
                                        operacion = monto;
                                    }
                                }

                                if (monedaOrigen == "DOLARES")
                                {
                                    if (monedaDestino == "COLONES")
                                    {
                                        operacion = monto * DOL_COL;
                                    }

                                    if (monedaDestino == "EUROS")
                                    {
                                        operacion = 1000000;
                                    }
                                    else
                                    {
                                        operacion = monto;
                                    }
                                }

                                if (monedaOrigen == "EUROS")
                                {
                                    if (monedaDestino == "DOLARES")
                                    {
                                        operacion = 5000000;
                                    }

                                    if (monedaDestino == "EUROS")
                                    {
                                        operacion = monto * EUR_COL;
                                    }
                                    else
                                    {
                                        operacion = monto;
                                    }
                                }

                                Transferencia transferencia= new Transferencia()
                                {
                                    TRA_CODIGO = 1,
                                    CUO_CODIGO = cuentasList[indexCuentaOrigen].CUE_CODIGO,
                                    CUO_MONEDA = monedaOrigen,
                                    CUD_CODIGO = cuentasList[indexCuentaDestino].CUE_CODIGO,
                                    CUD_MONEDA = monedaDestino,
                                    TRA_MONTO_ORIGEN = monto,
                                    TRA_MONTO_FINAL = operacion,
                                    TRA_FECHA = DateTime.Now
                                };
                                string msj = string.Format("Comprobación:\nCuenta Origen: {0}\nMondea: {1}\nCuenta Destino: {2}\nMoneda: {3}\nMonto: {4}\nMonto Final: {5}",
                                    cuentasList[indexCuentaOrigen].CUE_DESCRIPCION, cuentasList[indexCuentaOrigen].CUE_MONEDA, cuentasList[indexCuentaDestino].CUE_DESCRIPCION, cuentasList[indexCuentaDestino].CUE_MONEDA, transferencia.TRA_MONTO_ORIGEN, transferencia.TRA_MONTO_FINAL);

                                bool respuesta = await DisplayAlert("Comprobación:", msj, "OK", "Cancelar");
                                if (respuesta)
                                {
                                    Cuenta cuenta1 = cuentasList[indexCuentaOrigen];
                                    cuenta1.CUE_SALDO -= monto;
                                    Cuenta cuenta2 = cuentasList[indexCuentaDestino];
                                    cuenta2.CUE_SALDO += operacion;
                                    await cuentaManager.ActualizarCuenta(cuenta1);
                                    await cuentaManager.ActualizarCuenta(cuenta2);
                                    await transferenciasManager.AgregarTransferencia(transferencia);
                                    await DisplayAlert("Transaccion", "Transaccion completada.", "OK");
                                    Limpiar();
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
                await DisplayAlert("Error", "Error al crear Transferencia.", "OK");
            }
        }

        private void Limpiar()
        {
            Pkr_CuentaDestino.SelectedItem = 0;
            Pkr_CuentaOrigen.SelectedItem = 0;
            Txt_Monto.Text = "";
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

    }
}