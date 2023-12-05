using Gamerz.Business.Dtos;
using Gamerz.Business.Services;
using Gamerz.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamerz.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class DashboardController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        private readonly IProductModelService _productModelService;

        public DashboardController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment environment, IProductModelService productModelService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _environment = environment;
            _productModelService = productModelService;
        }
        public IActionResult Index()
        {
            var productDtoList = _productService.GetProducts();


            var viewModel = productDtoList.Select(x => new ListProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                ProductModelId = x.ProductModelId,
                ProductModelName = x.ProductModelName,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                ImagePath = x.ImagePath
            }).ToList();

            return View(viewModel);

        }


    }
}
