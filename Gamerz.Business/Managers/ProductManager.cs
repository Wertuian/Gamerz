using Gamerz.Business.Dtos;
using Gamerz.Business.Services;
using Gamerz.Data.Entities;
using Gamerz.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Business.Managers
{
    public class ProductManager : IProductService
    {
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<CategoryEntity> _categoryRepository;
        private readonly IRepository<ProductModelEntity> _productModelRepository;
        public ProductManager(IRepository<ProductEntity> productRepository, IRepository<CategoryEntity> categoryRepository, IRepository<ProductModelEntity> productModelRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productModelRepository = productModelRepository;
        }
        public bool AddProduct(AddProductDto addProductDto)
        {
            var hasProduct = _productRepository.GetAll(x => x.Name.ToLower() == addProductDto.Name.ToLower()).ToList();

            if (hasProduct.Any())
            {
                return false;
            }

            var productEntity = new ProductEntity()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                UnitInStock = addProductDto.UnitInStock,
                UnitPrice = addProductDto.UnitPrice,
                ProductModelId = addProductDto.ProductModelId,
                ImagePath = addProductDto.ImagePath
            };

            _productRepository.Add(productEntity);
            return true;


        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public void EditProduct(EditProductDto editProductDto)
        {
            var productEntity = _productRepository.GetById(editProductDto.Id);


            productEntity.Name = editProductDto.Name;
            productEntity.Description = editProductDto.Description;
            productEntity.UnitPrice = editProductDto.UnitPrice;
            productEntity.UnitInStock = editProductDto.UnitInStock;
            productEntity.ProductModelId = editProductDto.ProductModelId;

            if (editProductDto.ImagePath is not null)
            {
                productEntity.ImagePath = editProductDto.ImagePath;
            }

            _productRepository.Update(productEntity);

        }

        public EditProductDto GetProductById(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var editProductDto = new EditProductDto()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
                ProductModelId = productEntity.ProductModelId,
                ImagePath = productEntity.ImagePath
            };

            return editProductDto;
        }

        public ProductDetailDto GetProductDetailById(int id)
        {
            var productEntity = _productRepository.GetById(id);

            var productDetailDto = new ProductDetailDto()
            {
                ProductId = productEntity.Id,
                ProductName = productEntity.Name,
                Description = productEntity.Description,
                UnitInStock = productEntity.UnitInStock,
                UnitPrice = productEntity.UnitPrice,
                ImagePath = productEntity.ImagePath,
                ProductModelId=productEntity.ProductModelId
 
            };



            return productDetailDto;
        }

        public List<ListProductDto> GetProducts()
        {
            var productEntites = _productRepository.GetAll().OrderBy(x => x.ProductModel.Category.Name).ThenBy(x => x.Name);



            var productDtoList = productEntites.Select(x => new ListProductDto
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                UnitInStock = x.UnitInStock,
                CategoryName = x.ProductModel.Category.Name,
                ProductModelId = x.ProductModelId,
                ProductModelName = x.ProductModel.Name,
                ImagePath = x.ImagePath
            }).ToList();

            return productDtoList;
        }

        public List<ListProductDto> GetProductsByCategoryId(int? categoryId,int? productModelId)
        {
            if (categoryId.HasValue)
            {
                var productEntites = _productRepository.GetAll(x => x.ProductModel.CategoryId == categoryId).OrderBy(x => x.Name);

                var productDtos = productEntites.Select(x => new ListProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnitInStock = x.UnitInStock,
                    UnitPrice = x.UnitPrice,

                    CategoryName = x.ProductModel.Category.Name,
                    ProductModelId = x.ProductModelId,
                    ProductModelName = x.ProductModel.Name,
                    ImagePath = x.ImagePath
                }).ToList();

                return productDtos;
            }


            return GetProducts();

        }
    }
}
