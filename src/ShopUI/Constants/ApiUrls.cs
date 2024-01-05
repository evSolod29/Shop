using System;

namespace ShopUI.Constants
{
    public static class ApiUrls
    {
        public const string AuthUrl = "http://localhost:5000/";
        public const string ShopUrl = "http://localhost:5001/";
        public const string Login = AuthUrl + "login/";
        public const string Users = AuthUrl + "users/";
        public const string Products = ShopUrl + "products/";
        public const string ProductsFull = Products + "full/";
        public const string Categories = ShopUrl + "categories/";
    }
}
