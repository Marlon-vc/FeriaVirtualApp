using Acr.UserDialogs;
using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Helpers.Network;
using FeriaVirtualApp.Models;
using FeriaVirtualApp.Models.Api;
using FeriaVirtualApp.Views.Cliente;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeriaVirtualApp.ViewModels
{
    public class ProductoresViewModel : BaseModel
    {
        public Command ProductorSelectedCommand { get; set; }
        public ObservableRangeCollection<ProductorModel> Productores { get; set; }

        private INavigation Nav { get; set; }
        private IUserDialogs Dialogs { get; set; }

        public ProductoresViewModel(INavigation nav)
        {
            Nav = nav;
            Dialogs = UserDialogs.Instance;
            Productores = new ObservableRangeCollection<ProductorModel>();

            ProductorSelectedCommand = new Command(p => OnProductorSelected((ProductorModel) p));

            Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(_ => LoadProductoresAsync().ConfigureAwait(false));
        }

        private void OnProductorSelected(ProductorModel productor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Nav?.PushAsync(new TramoPage(productor), true);
            });
        }

        private async Task LoadProductoresAsync()
        {
            var idCliente = Preferences.Get(Config.UserId, null, Config.SharedName);

            if (string.IsNullOrWhiteSpace(idCliente))
                return;

            Dialogs.ShowLoading();

            var response = await Connection.Get($"{Urls.Productores}/region/{idCliente}")
                .ConfigureAwait(false);

            Dialogs.HideLoading();

            if (response.Succeeded)
            {
                var productores = response.ParseBody<List<ProductorModel>>();
                Productores.ReplaceRange(productores);

            } else
            {
                await Dialogs.AlertAsync(new AlertConfig
                {
                    Message = "No se pudieron obtener los productores, intentalo de nuevo.",
                    OkText = "Aceptar"
                });

                Device.BeginInvokeOnMainThread(() =>
                {
                    Nav?.PopAsync(true);
                });
            }
        }


    }
}
