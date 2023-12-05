using Gamerz.Business.Services;
using Gamerz.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace Gamerz.WebUI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke(int? categoryId = null,int? productModelId = null)
        {
            var productDtos = _productService.GetProductsByCategoryId(categoryId,productModelId);

            var viewModel = productDtos.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryName = x.CategoryName,
                CategoryId = x.CategoryId,
                ImagePath = x.ImagePath,
                ProductModelName = x.ProductModelName
            }).ToList();

            return View(viewModel);
        }
        
    }
}

