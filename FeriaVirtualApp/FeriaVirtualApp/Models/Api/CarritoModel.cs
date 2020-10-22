using System;
using System.Collections.Generic;
using System.Text;

namespace FeriaVirtualApp.Models.Api
{
    public class CarritoModel
    {
        public string IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
