using System;
using System.Collections.Generic;
using System.Text;

namespace FeriaVirtualApp.Helpers
{
    public static class Constants
    {
        public static string LoginDone => "LOGIN_COMPLETED";
    }

    public static class Config
    {
        public static string SharedName => "FeriaVirtualApp";
        public static string UserLogged => "USER_LOGGED_IN";
        public static string UserType => "USER_TYPE";
        public static string UserId => "USER_ID";
    }

    public static class Urls
    {
        public static string Host => "192.168.0.14";
        public static string Productores => "http://192.168.0.14:5001/api/Productores";
        public static string Productos => "http://192.168.0.14:5001/api/Productos";
        public static string Clientes => "http://192.168.0.14:5001/api/Clientes";
        public static string Categorias => "http://192.168.0.14:5001/api/Categorias";
        public static string Carrito => "http://192.168.0.14:5001/api/Carrito";
        public static string Ordenes => "http://192.168.0.14:5001/api/Orden";
        public static string Afiliaciones => "http://192.168.0.14:5001/api/Afiliaciones";
        public static string Login => "http://192.168.0.14:5001/api/ClienteLog";
    }
}
