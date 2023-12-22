using CrudProducts.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CrudProducts.Pages.ShoppingCart
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; }

        public void OnGet()
        {
            // Initialisez votre liste d'éléments du panier depuis le Local Storage ou d'autres sources
            // Exemple :
            CartItems = GetCartItemsFromLocalStorage();
        }

        private List<CartItem> GetCartItemsFromLocalStorage()
        {
            var cartItemsJson = HttpContext.Session.GetString("cart");

            if (string.IsNullOrEmpty(cartItemsJson))
            {
                return new List<CartItem>();
            }

            // Convertissez la chaîne JSON en une liste d'objets CartItem
            return JsonConvert.DeserializeObject<List<CartItem>>(cartItemsJson);// Remplacez cela par votre logique réelle
        }
    }
}
