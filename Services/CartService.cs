using Flor.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Flor.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "Cart";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var sessionCart = session.GetString(CartSessionKey);

            if (string.IsNullOrEmpty(sessionCart))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
        }

        public int GetCartCount()
        {
            var cart = GetCart();
            return cart.Sum(item => item.Kolicina);
        }
    }
}
