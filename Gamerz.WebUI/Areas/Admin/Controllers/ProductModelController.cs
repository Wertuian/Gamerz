using Gamerz.Business.Dtos;
using Gamerz.Business.Services;
using Gamerz.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Gamerz.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductModelController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductModelService _productModelService;
        public ProductModelController(ICategoryService categoryService,IProductModelService productModelService)
        {
            _categoryService = categoryService;
            _productModelService = productModelService; 

        }
        public IActionResult List()
        {
            var listProductModelDtos = _productModelService.GetProductModels();

            var viewModel = listProductModelDtos.Select(x => new ProductListViewModel
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();

            return View(viewModel);
        }

        public IActionResult New()
        {
            ViewBag.Categories = _categoryService.GetCategories();
            return View("Form",new ProductModelFormViewModel());
        }
        public IActionResult Edit(int id)
        {
            var editProductModelDto = _productModelService.GetProductModelById(id);

            var viewModel = new ProductModelFormViewModel()
            {
                Id = id,
                Name = editProductModelDto.Name,
                CategoryId = editProductModelDto.CategoryId,

            };
            ViewBag.Categories = _categoryService.GetCategories();
            return View("form", viewModel);

        }
        [HttpPost]
        public IActionResult Save(ProductModelFormViewModel formData)
        {


            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetCategories();
                return View("Form", formData);
            }


            if (formData.Id == 0)
            {

                var addProductModelDto = new AddProductModelDto()
                {
                    Name = formData.Name.Trim()
                };


                if (formData.Name is not null)
                {
                    addProductModelDto.CategoryId = formData.CategoryId;
                    addProductModelDto.Name = formData.Name.Trim();
                }

                var result = _productModelService.AddProductModel(addProductModelDto);


                if (result)
                {
                    RedirectToAction("List");

                }
                else
                {
                    ViewBag.ErrorMessage = "Bu isimde bir model zaten var.";
                    return View("Form", formData);
                }

            }
            else
            {
                var editProductModelDto = new EditProductModelDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    CategoryId = formData.CategoryId,
                    
                };

                _productModelService.EditProductModel(editProductModelDto);

                return RedirectToAction("List");
            }


            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            _productModelService.DeleteProductModel(id);
            return RedirectToAction("List");
        }

    }
}
