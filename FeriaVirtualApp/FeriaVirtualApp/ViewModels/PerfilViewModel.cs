using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Helpers.Network;
using FeriaVirtualApp.Models;
using FeriaVirtualApp.Models.Api;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeriaVirtualApp.ViewModels
{
    public class PerfilViewModel: BaseModel
    {
        private ClienteModel model;

        public ClienteModel Model { get => model; set { model = value; NotifyPropertyChanged(); } }

        private IUserDialogs Dialogs { get; set; }
        private INavigation Nav { get; set; }

        public PerfilViewModel(INavigation nav)
        {
            Model = new ClienteModel();
            Nav = nav;
            Dialogs = UserDialogs.Instance;

            Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(_ => LoadClienteAsync().ConfigureAwait(false));
            //LoadClienteAsync().ConfigureAwait(false);
        }

        private async Task LoadClienteAsync()
        {
            var idCliente = Preferences.Get(Config.UserId, null, Config.SharedName);

            if (string.IsNullOrWhiteSpace(idCliente))
                return;

            Dialogs.ShowLoading();

            var response = await Connection.Get($"{Urls.Clientes}/{idCliente}")
                .ConfigureAwait(false);

            Dialogs.HideLoading();

            if (response.Succeeded)
            {
                Model = response.ParseBody<ClienteModel>();
            }
            else
            {
                await Dialogs.AlertAsync(new AlertConfig
                {
                    Message = "Ocurrió un error al cargar el perfil, intenta de nuevo más tarde",
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
