using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Flor.Services;

namespace Flor.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = _cartService.GetCartCount();
            return View("Default", count);
        }
    }
}
