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

        private CuentaManager cuentaManager = new CuentaManager();
        private List<Cuenta> cuentaList = new List<Cuenta>();

        public TransferenciaPage()
        {
            InitializeComponent();
            CargaDatos();
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
                bool BCuentaOrigen = true;
                bool BCuentaDestino = true;
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

                string Desc = Txt_Descripcion.Text;
                string Monto = Txt_Saldo.Text;
                bool BDesc = true;
                bool BMonto = true;

                if (!VerificaCampo(Desc))
                {
                    BDesc = false;
                }
                if (!VerificaCampo(Monto))
                {
                    BMonto = false;
                }
                #endregion
                if(BMonto && BDesc && BCuentaDestino && BCuentaOrigen && BEstado)
                {
                    if(indexDestino != indexOrigen)
                    {
                        int imonto = int.Parse(Monto);
                        if(imonto > 0)
                        {
                            if(cuentaList[indexOrigen].CUE_SALDO < imonto)
                            {
                                if (cuentaList[indexOrigen].CUE_MONEDA.Equals(cuentaList[indexDestino].CUE_MONEDA))
                                {
                                    //cuentaList[indexDestino].CUE_SALDO 
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
            catch (Exception)
            {
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
                return true;

            return false;
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