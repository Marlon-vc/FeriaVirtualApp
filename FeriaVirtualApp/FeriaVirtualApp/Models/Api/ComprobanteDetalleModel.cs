using System;
using System.Collections.Generic;
using System.Text;

namespace FeriaVirtualApp.Models.Api
{
    public class DetalleModel
    {
        public int IdProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
    }

    public class ComprobanteDetalleModel
    {
        public string ComprobanteSinpe { get; set; }
        public List<DetalleModel> Productos { get; set; }
    }
}
