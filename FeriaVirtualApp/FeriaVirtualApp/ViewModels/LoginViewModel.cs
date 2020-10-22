using Acr.UserDialogs;
using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Helpers.Network;
using FeriaVirtualApp.Models;
using FeriaVirtualApp.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeriaVirtualApp.ViewModels
{
    public class LoginViewModel
    {
        public Models.LoginModel Model { get; set; }
        public Command LoginCommand { get; set; }
        private INavigation Nav { get; set; }
        private IUserDialogs Dialogs { get; set; }

        public LoginViewModel(INavigation nav)
        {
            Nav = nav;
            Model = new Models.LoginModel();
            Dialogs = UserDialogs.Instance;
            LoginCommand = new Command(_ => OnLogin().ConfigureAwait(false));
        }

        public async Task OnDebug()
        {
            var response = await Connection.Get(Urls.Productores)
                .ConfigureAwait(false);

            Dialogs.Alert(new AlertConfig
            {
                Message = response.ResponseBody,
                OkText = "Aceptar"
            });
        }

        public async Task OnLogin()
        {
            if (Model.Busy) return;

            if (Model.IsEmpty())
            {
                Dialogs.Alert(new AlertConfig
                {
                    Message = "Debes completar todos los campos",
                    OkText = "Aceptar"
                });
                return;
            }

            Model.Busy = true;
            Dialogs.ShowLoading("Cargando");

            var json = JsonConvert.SerializeObject(new
            {
                Id = Model.User,
                Password = Model.Pass
            });

            var response = await Connection.Post(Urls.Login, json, 120)
                .ConfigureAwait(true);

            Dialogs.HideLoading();

            if (response.Succeeded)
            {
                var loginData = response.ParseBody<Models.Api.LoginModel>();
                var userType = loginData.Client ? "cliente" : "productor";

                Preferences.Set(Config.UserLogged, true, Config.SharedName);
                Preferences.Set(Config.UserId, loginData.Id, Config.SharedName);
                Preferences.Set(Config.UserType, userType, Config.SharedName);

                Messaging.Send(this, Constants.LoginDone);

            } else
            {
                Dialogs.Alert(new AlertConfig
                {
                    Message = $"Ocurrió un error al iniciar sesión - {response.Code}",
                    OkText = "Aceptar"
                });
            }

            Model.Busy = false;
        }
    }
}
