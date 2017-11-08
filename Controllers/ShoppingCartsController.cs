using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pc_store.Controllers.Resources;
using pc_store.Core;
using pc_store.Core.Models;
using pc_store.Persistence;

namespace pc_store.Controllers
{
    [Route("/api/shoppingcart")]
    public class ShoppingCartsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartsController(IMapper mapper, IUnitOfWork unitOfWork, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCart(int id)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCart(id);
            if (shoppingCart == null) return NotFound("Not found.");

            return Ok(_mapper.Map<ShoppingCart, ShoppingCartResource>(shoppingCart));
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingCart([FromBody] SaveShoppingCartResource saveShoppingCart)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cart = _mapper.Map<SaveShoppingCartResource, ShoppingCart>(saveShoppingCart);

            cart.CartItems.Select(item =>
            {
                var product = _productRepository.GetProduct(item.ProductId, includeRelated: false).Result;
                item.UnitPrice = product.Price;
                cart.TotalPrice += item.UnitPrice;
                return item;
            }).ToList();

            cart.Created = DateTime.Now;
            cart.Modified = DateTime.Now;

            _shoppingCartRepository.Add(cart);
            await _unitOfWork.CompleteAsync();

            var result = await _shoppingCartRepository.GetShoppingCart(cart.Id);

            return Ok(_mapper.Map<ShoppingCart, ShoppingCartResource>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingCart(int id, [FromBody] SaveShoppingCartResource saveShoppingCart)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cart = await _shoppingCartRepository.GetShoppingCart(id);
            _mapper.Map<SaveShoppingCartResource, ShoppingCart>(saveShoppingCart, cart);
            cart.Modified = DateTime.Now;

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<ShoppingCart, ShoppingCartResource>(cart));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            var cart = await _shoppingCartRepository.GetShoppingCart(id, includeRelated: false);
            if (cart == null) return NotFound("Not found.");

            _shoppingCartRepository.Remove(cart);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}