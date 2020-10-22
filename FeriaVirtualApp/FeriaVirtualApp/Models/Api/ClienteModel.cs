using System;
using System.Collections.Generic;
using System.Text;

namespace FeriaVirtualApp.Models.Api
{
    public class ClienteModel: BaseModel
    {
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public DateTimeOffset FechaNacimiento { get; set; }
        public string Nombre { get; set; }
        
        #region Apellido
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        #endregion

        #region Dirección
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }
        #endregion

        #region LogIn
        public string Usuario { get; set; }
        public string Password { get; set; }
        #endregion

        public ClienteModel()
        {
            Identificacion = "";
            Telefono = "";
            FechaNacimiento = DateTimeOffset.UtcNow;
            Nombre = "";
            Apellido1 = "";
            Apellido2 = "";
            Provincia = "";
            Canton = "";
            Distrito = "";
            DireccionExacta = "";
            Usuario = "";
            Password = "";
        }
    }
}
