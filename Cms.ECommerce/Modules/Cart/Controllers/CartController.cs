// using Cms.ECommerce.Modules.Cart.Services;
// using Cms.Shared.Shared.Controllers;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Cms.ECommerce.Modules.Cart.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class CartController : SharedController<Entities.Cart>
// {
//     private readonly CartService _cartService;
//
//     public CartController(CartService cartService)
//     {
//         _cartService = cartService;
//     }
//
//     [HttpGet]
//     public Task<Entities.Cart> Read()
//     {
//         return _cartService.GetCart();
//     }
// }