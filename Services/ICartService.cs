using System.Collections.Generic;
using Flor.Models;

public interface ICartService
{
    List<CartItem> GetCart();
    int GetCartCount();
}
