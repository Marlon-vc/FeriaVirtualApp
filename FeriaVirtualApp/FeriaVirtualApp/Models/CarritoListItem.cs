using System;
using System.Collections.Generic;
using System.Text;

namespace FeriaVirtualApp.Models
{
    public class CarritoListItem
    {
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}
