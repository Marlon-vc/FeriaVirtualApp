using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeriaVirtualApp.ViewModels
{
    public class HomeViewModel
    {
        public HomeModel Model { get; set; }

        private INavigation _nav;

        public HomeViewModel(INavigation nav)
        {
            _nav = nav;
            Model = new HomeModel();

            //TODO: Verificar tipo de usuario.

            LoadMenu().ConfigureAwait(false);
        }

        public void OnItemTapped(Models.MenuItem item)
        {
            if (item.Target == "logout")
            {
                //_nav.PushAsync(new LoginPage(), true);
                return;
            }

            var page = Type.GetType(item.Target);
            if (page == null) return;
            
            var instance = (Page)Activator.CreateInstance(page);
            if (instance == null) return;

            _nav.PushAsync(instance, true);
        }

        private async Task LoadMenu()
        {
            string menuToLoad = null;

            var role = Preferences.Get(Config.UserType, "cliente", Config.SharedName);

            switch (role)
            {
                case "admin":
                    menuToLoad = "FeriaVirtualApp.Resources.MenuAdmin.json";
                    break;
                case "cliente":
                    menuToLoad = "FeriaVirtualApp.Resources.MenuCliente.json";
                    break;
                case "productor":
                    menuToLoad = "FeriaVirtualApp.Resources.MenuProductor.json";
                    break;
            }

            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(menuToLoad);
            
            if (stream == null)
                return;

            using (var reader = new StreamReader(stream))
            {
                var json = await reader.ReadToEndAsync();
                var menu = JsonConvert.DeserializeObject<List<Models.MenuItem>>(json);

                Model.AddItems(menu);
            }
        }
    }
}
