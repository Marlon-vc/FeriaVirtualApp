using Acr.UserDialogs;
using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Helpers.Network;
using FeriaVirtualApp.Models;
using FeriaVirtualApp.Models.Api;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeriaVirtualApp.ViewModels
{
    public class CarritoViewModel
    {
        public Command RealizarCompraCommand { get; set; }
        public ObservableRangeCollection<CarritoListItem> CarritoItems { get; set; }
        private INavigation Nav { get; set; }
        private IUserDialogs Dialogs { get; set; }
        private bool Busy { get; set; }
        public CarritoViewModel(INavigation nav)
        {
            Nav = nav;
            Dialogs = UserDialogs.Instance;
            CarritoItems = new ObservableRangeCollection<CarritoListItem>();
            RealizarCompraCommand = new Command(_ => RealizarCompraAsync().ConfigureAwait(false));

            LoadCarritoAsync().ConfigureAwait(false);
        }

        private async Task RealizarCompraAsync()
        {
            if (Busy)
                return;

            var idCliente = Preferences.Get(Config.UserId, null, Config.SharedName);
            if (idCliente == null)
                return;

            Dialogs.ShowLoading();

            var response = await Connection.Post($"{Urls.Ordenes}/{idCliente}", "")
                .ConfigureAwait(false);

            Dialogs.HideLoading();

            if (response.Succeeded)
            {
                var detalle = response.ParseBody<ComprobanteDetalleModel>();

                if (detalle == null)
                    return;

                await Dialogs.AlertAsync(new AlertConfig
                {
                    Title = "Compra realizada",
                    Message = $"Comprobante sinpe: ",
                    OkText = "Aceptar"
                });

                await LoadCarritoAsync()
                    .ConfigureAwait(false);
            } else
            {
                await Dialogs.AlertAsync(new AlertConfig
                {
                    Message = "No se pudo completar la operación, intentalo de nuevo",
                    OkText = "Aceptar"
                });
            }
        }

        private async Task LoadCarritoAsync()
        {
            var idCliente = Preferences.Get(Config.UserId, null, Config.SharedName);

            if (idCliente == null)
                return;

            Dialogs.ShowLoading();

            var response = await Connection.Get($"{Urls.Carrito}/completo/{idCliente}")
                .ConfigureAwait(false);

            Dialogs.HideLoading();

            if (response.Succeeded)
            {
                var carritos = response.ParseBody<List<CarritoListItem>>();
                CarritoItems.ReplaceRange(carritos);

            } else
            {
                await Dialogs.AlertAsync(new AlertConfig
                {

                });
                Device.BeginInvokeOnMainThread(() =>
                {
                    Nav?.PopAsync(true);
                });
            }
        }
    }
}
