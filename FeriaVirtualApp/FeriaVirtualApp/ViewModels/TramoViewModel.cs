using Acr.UserDialogs;
using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Helpers.Network;
using FeriaVirtualApp.Models.Api;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FeriaVirtualApp.ViewModels
{
    public class TramoViewModel
    {
        public ProductorModel ProductorActual { get; set; }
        public Command AgregarAlCarritoCommand { get; set; }
        public ObservableRangeCollection<ProductoModel> Productos { get; set; }
        private INavigation Nav { get; set; }
        private IUserDialogs Dialogs { get; set; }

        public TramoViewModel(INavigation nav, ProductorModel productor)
        {
            ProductorActual = productor;
            Nav = nav;
            Dialogs = UserDialogs.Instance;
            Productos = new ObservableRangeCollection<ProductoModel>();
            AgregarAlCarritoCommand = new Command(p => AgregarAlCarrito((ProductoModel)p).ConfigureAwait(false));

            Task.Delay(TimeSpan.FromSeconds(1))
                .ContinueWith(_ => LoadProductosAsync().ConfigureAwait(false));
        }

        private async Task AgregarAlCarrito(ProductoModel producto)
        {
            var result = await Dialogs.PromptAsync(new PromptConfig
            {
                InputType = InputType.Number,
                CancelText = "Cancelar",
                Message = "Cantidad",
                MaxLength = 2,
                Text = "1",
                OkText = "Agregar"
            });

            if (!result.Ok)
                return;

            var succeeded = int.TryParse(result.Text, out int cantidad);

            if (!succeeded || cantidad < 1 || cantidad > producto.Disponibilidad)
                return;

            var idCliente = Preferences.Get(Config.UserId, null, Config.SharedName);
            if (idCliente == null)
                return;

            var json = JsonConvert.SerializeObject(new
            {
                idCliente,
                idProducto = producto.Id,
                cantidad
            });

            var response = await Connection.Post($"{Urls.Carrito}/{idCliente}", json)
                .ConfigureAwait(false);

            if (response.Succeeded)
            {
                Dialogs.Toast("Producto agregado", TimeSpan.FromSeconds(3));
                await LoadProductosAsync()
                    .ConfigureAwait(false);
            } else
            {
                await Dialogs.AlertAsync(new AlertConfig
                {
                    Message = "No se pudo agregar el producto al carrito",
                    OkText = "Aceptar"
                });
            }
        }

        private async Task LoadProductosAsync()
        {
            Dialogs.ShowLoading();

            var response = await Connection.Get($"{Urls.Productos}/productor/{ProductorActual.Identificacion}")
                .ConfigureAwait(false);

            Dialogs.HideLoading();

            if (response.Succeeded)
            {
                var productos = response.ParseBody<List<ProductoModel>>();

                foreach (var producto in productos)
                {
                    if (string.IsNullOrWhiteSpace(producto.Foto))
                        continue;

                    if (producto.Foto.Contains("https"))
                    {
                        producto.Foto = producto.Foto.Replace("https", "http");
                    }
                    if (producto.Foto.Contains("localhost"))
                    {
                        producto.Foto = producto.Foto.Replace("localhost", Urls.Host);
                    }
                }

                Productos.ReplaceRange(productos);

            } else {
                await Dialogs.AlertAsync(new AlertConfig
                {
                    Message = "No se pudieron cargar los productos, intentalo de nuevo",
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
