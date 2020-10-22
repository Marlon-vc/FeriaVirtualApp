using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FeriaVirtualApp.Models.Api;
using FeriaVirtualApp.ViewModels;

namespace FeriaVirtualApp.Views.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TramoPage : ContentPage
    {
        public TramoPage(ProductorModel productor)
        {
            InitializeComponent();
            BindingContext = new TramoViewModel(Navigation, productor);
        }
    }
}