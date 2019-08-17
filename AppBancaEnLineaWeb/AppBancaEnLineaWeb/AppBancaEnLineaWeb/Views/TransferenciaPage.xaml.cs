using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppBancaEnLineaWeb.Models;
using AppBancaEnLineaWeb.Controllers;

namespace AppBancaEnLineaWeb.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferenciaPage : ContentPage
    {
        #region Variables
        private CuentaManager cuentaManager = new CuentaManager();
        private TransferenciasManager transferenciaManager = new TransferenciasManager();
        private List<Cuenta> cuentaList = new List<Cuenta>();
        #endregion

        public TransferenciaPage()
        {
            InitializeComponent();
            CargaDatos();
            Lbl_Date.Text = Lbl_Date.ToString() + DateTime.Now.Date.ToShortDateString();
            Lbl_SaldoDisponible.IsVisible = false;
        }

        private async void CargaDatos()
        {
            //Lista de los servicios.
            IEnumerable<Cuenta> cuentas = await cuentaManager.ObtenerCuentas(App.usuarioActual.USU_CODIGO.ToString());
            foreach (var item in cuentas)
            {
                cuentaList.Add(item);
            }

            Pkr_CuentaOrigen.ItemsSource = cuentaList;
            Pkr_CuentaDestino.ItemsSource = cuentaList;
        }

        private async void CrearTranferencia(object sender, EventArgs e)
        {
            try
            {
                #region Validaciones
                int indexOrigen = Pkr_CuentaOrigen.SelectedIndex;
                int indexDestino = Pkr_CuentaDestino.SelectedIndex;
                int indexEstado = Pkr_Estado.SelectedIndex;
                string Desc = Txt_Descripcion.Text;
                string Monto = Txt_Saldo.Text;
                bool BCuentaOrigen = true;
                bool BCuentaDestino = true;
                bool BDesc = true;
                bool BMonto = true;
                bool BEstado = true;

                if (indexOrigen == -1)
                {
                    BCuentaOrigen = false;
                    await DisplayAlert("Alerta", "Debes seleccionar una cuenta de origen.", "OK");
                }

                if (indexDestino == -1)
                {
                    BCuentaDestino = false;
                    await DisplayAlert("Alerta", "Debes seleccionar una cuenta de destino.", "OK");
                }

                if (indexEstado == -1)
                {
                    BCuentaDestino = false;
                    await DisplayAlert("Alerta", "Debes seleccionar un estado.", "OK");
                }

                if (!VerificaCampo(Desc))
                {
                    BDesc = false;
                    await DisplayAlert("Alerta", "Ingrese una descripcion.", "OK");
                }
                if (!VerificaCampo(Monto))
                {
                    BMonto = false;
                    await DisplayAlert("Alerta", "Introduc seleccionar un Monto.", "OK");
                }
                #endregion

                if(BMonto && BDesc && BCuentaDestino && BCuentaOrigen && BEstado)
                {
                    if(indexDestino != indexOrigen)
                    {
                        int imonto = int.Parse(Monto);
                        if(imonto > 0)
                        {
                            if(cuentaList[indexOrigen].CUE_SALDO > imonto)
                            {
                                if (cuentaList[indexOrigen].CUE_MONEDA.Equals(cuentaList[indexDestino].CUE_MONEDA))
                                {

                                    Cuenta cuentaOrigen = new Cuenta()
                                    {
                                        CUE_CODIGO = cuentaList[indexOrigen].CUE_CODIGO,
                                        CUE_SALDO = cuentaList[indexOrigen].CUE_SALDO - imonto,
                                        CUE_DESCRIPCION = cuentaList[indexOrigen].CUE_DESCRIPCION,
                                        CUE_ESTADO = cuentaList[indexOrigen].CUE_ESTADO,
                                        USU_CODIGO = cuentaList[indexOrigen].USU_CODIGO,
                                        CUE_MONEDA = cuentaList[indexOrigen].CUE_MONEDA
                                    };

                                    Cuenta cuentaDestino = new Cuenta()
                                    {
                                        CUE_CODIGO = cuentaList[indexDestino].CUE_CODIGO,
                                        CUE_SALDO = cuentaList[indexDestino].CUE_SALDO + imonto,
                                        CUE_DESCRIPCION = cuentaList[indexDestino].CUE_DESCRIPCION,
                                        CUE_ESTADO = cuentaList[indexDestino].CUE_ESTADO,
                                        USU_CODIGO = cuentaList[indexDestino].USU_CODIGO,
                                        CUE_MONEDA = cuentaList[indexDestino].CUE_MONEDA
                                    };

                                    Transferencia transferencia = new Transferencia()
                                    {
                                        TRA_CUENTA_DESTINO = cuentaList[indexDestino].CUE_CODIGO,
                                        TRA_CUENTA_ORIGEN = cuentaList[indexOrigen].CUE_CODIGO,
                                        TRA_FECHA = DateTime.Now,
                                        TRA_ESTADO = Pkr_Estado.SelectedItem.ToString().Substring(0, 1),
                                        TRA_CODIGO = 1,
                                        TRA_DESCRIPCION = Txt_Descripcion.Text,
                                        TRA_MONTO = imonto
                                    };

                                    //Validar la creacion de la transferencia.
                                    bool respuesta = await DisplayAlert("Agregar transferencia", "", "Agregar", "Cancelar");
                                    if (respuesta)
                                    {
                                        //Agregar la cuenta.
                                        transferencia = await transferenciaManager.AgregarTransferencia(transferencia);
                                        cuentaOrigen = await cuentaManager.ActualizarCuenta(cuentaOrigen);
                                        cuentaDestino = await cuentaManager.ActualizarCuenta(cuentaDestino);
                                        //Mensaje de exito.
                                        await DisplayAlert("Mensaje", "Transferencia agregada correctamente", "Aceptar");
                                    }
                                    else { /* No hacer codigo aqui. */ }
                                }
                                else
                                {
                                    await DisplayAlert("Alerta", "Cambio entre monedas, no disponible.", "OK");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Alerta", "La cuenta de origen, no tiene los suficientes fondos.", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Alerta", "Debes trasferir un monto valido.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "Has seleccionado la misma cuenta.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Alerta", "Verifique los datos.", "OK");
                }
            }
            catch (Exception ex)
            {
                //await DisplayAlert("Error", ex.Message, "OK");
                await DisplayAlert("Error", "Error al crear transferencia.", "OK");
            }
        }


        /// <summary>
        /// Verifica que el campo no este vacio o nulo.
        /// </summary>
        /// <param name="Campo">Cadena de texto que desea verificar.</param>
        /// <returns>True en caso de que este vacio o nulo. False en caso de tener información.</returns>
        private bool VerificaCampo(string Campo)
        {
            if (Campo == null || Campo.Equals(""))
                return false;

            return true;
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