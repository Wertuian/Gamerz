using Gamerz.Business.Dtos;
using Gamerz.Business.Services;
using Gamerz.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamerz.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _environment;
        private readonly IProductModelService _productModelService;

        public ProductController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment environment, IProductModelService productModelService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _environment = environment;
            _productModelService = productModelService;
        }
        public IActionResult List()
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
        public IActionResult New()
        {

            ViewBag.ProductModels = _productModelService.GetProductModels();
            ViewBag.Categories = _categoryService.GetCategories();
            return View("Form", new ProductFormViewModel());

        }
        public IActionResult Edit(int id)
        {
            var editProductDto = _productService.GetProductById(id);

            var viewModel = new ProductFormViewModel()
            {
                Id = editProductDto.Id,
                Name = editProductDto.Name,
                Description = editProductDto.Description,
                UnitInStock = editProductDto.UnitInStock,
                UnitPrice = editProductDto.UnitPrice,
                CategoryId = editProductDto.CategoryId,
                ProductModelId=editProductDto.ProductModelId,
            };

           
            ViewBag.ImagePath = editProductDto.ImagePath;
            ViewBag.ProductModels = _productModelService.GetProductModels();
            ViewBag.Categories = _categoryService.GetCategories();
            return View("form", viewModel);

        }
        [HttpPost]
        public IActionResult Save(ProductFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryService.GetCategories();
                ViewBag.ProductModels = _productModelService.GetProductModels();
                return View("Form", formData);
            }

            var newFileName = "";

            if (formData.File is not null) 
            {

                var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif" };
                

                var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif" };
                

                var fileContentType = formData.File.ContentType; 

                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName); 

                var fileExtension = Path.GetExtension(formData.File.FileName); 

                if (!allowedFileTypes.Contains(fileContentType) ||
                    !allowedFileExtensions.Contains(fileExtension))
                {
                    ViewBag.FileError = "Dosya formatı veya içeriği hatalı";

                    ViewBag.ProductModels = _productModelService.GetProductModels();
                    ViewBag.Categories = _categoryService.GetCategories();
                    return View("Form", formData);

                }

                newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;
               

                var folderPath = Path.Combine("images", "products");
                

                var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);
                

                var wwwrootFilePath = Path.Combine(wwwrootFolderPath, newFileName);
                

                Directory.CreateDirectory(wwwrootFolderPath); 

                using (var fileStream = new FileStream(
                    wwwrootFilePath, FileMode.Create))
                {
                    formData.File.CopyTo(fileStream);
                }

            }

            if (formData.Id == 0) 
            {
                var addProductDto = new AddProductDto()
                {
                    Name = formData.Name.Trim(),
                    Description = formData.Description,
                    UnitPrice = formData.UnitPrice,
                    UnitInStock = formData.UnitInStock,
                    CategoryId = formData.CategoryId,
                    ProductModelId = formData.ProductModelId,
                    ImagePath = newFileName

                };

                _productService.AddProduct(addProductDto);
            }
            else 
            {
                var editProductDto = new EditProductDto()
                {
                    Id = formData.Id,
                    Name = formData.Name,
                    Description = formData.Description,
                    UnitInStock = formData.UnitInStock,
                    UnitPrice = formData.UnitPrice,
                    CategoryId = formData.CategoryId,
                    ProductModelId= formData.ProductModelId

                };

                if (formData.File is not null)
                {
                    editProductDto.ImagePath = newFileName;
                }
             
                _productService.EditProduct(editProductDto);

            }


            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);

            return RedirectToAction("List");
        }


    }


}
