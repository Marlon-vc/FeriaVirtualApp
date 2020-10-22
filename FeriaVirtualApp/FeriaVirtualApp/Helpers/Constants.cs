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
        public static string Port => "5001";

        public static string Productores => $"https://{Host}:{Port}/api/Productores";
        public static string Productos => $"https://{Host}:{Port}/api/Productos";
        public static string Clientes => $"https://{Host}:{Port}/api/Clientes";
        public static string Categorias => $"https://{Host}:{Port}/api/Categorias";
        public static string Carrito => $"https://{Host}:{Port}/api/Carrito";
        public static string Ordenes => $"https://{Host}:{Port}/api/Orden";
        public static string Afiliaciones => $"https://{Host}:{Port}/api/Afiliaciones";
        public static string Login => $"https://{Host}:{Port}/api/ClienteLog";
    }
}
