﻿using FeriaVirtualApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeriaVirtualApp.Views.Cliente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductoresPage : ContentPage
    {
        public ProductoresPage()
        {
            InitializeComponent();
            BindingContext = new ProductoresViewModel(Navigation);
        }
    }
}