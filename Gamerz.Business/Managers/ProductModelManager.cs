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
    public class ProductModelManager : IProductModelService
    {
        private readonly IRepository<ProductModelEntity> _productModelRepository;
        private readonly IRepository<CategoryEntity> _categoryRepository;
        public ProductModelManager(IRepository<ProductModelEntity> ProductModelRepository, IRepository<CategoryEntity> categoryRepository)
        {
            _productModelRepository = ProductModelRepository;
            _categoryRepository = categoryRepository;
        }

        
        public bool AddProductModel(AddProductModelDto addProductModelDto)
        {
            var hasProductModel = _productModelRepository.GetAll(x => x.Name.ToLower() == addProductModelDto.Name.ToLower()).ToList();

            if (hasProductModel.Any())
            {
                return false;

            }

            var productModelEntity = new ProductModelEntity()
            {
                
                Name = addProductModelDto.Name,
                CategoryId = addProductModelDto.CategoryId,

            };

            _productModelRepository.Add(productModelEntity);

            return true;
        }

        public void DeleteProductModel(int id)
        {
            _productModelRepository.Delete(id);
        }


        public List<ListProductModelDto> GetProductModels()
        {
            var productModelEntities = _productModelRepository.GetAll().OrderBy(x => x.Name);

            var productModelDtoList = productModelEntities.Select(x => new ListProductModelDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId
            }).ToList();


            return productModelDtoList;
        }

        public EditProductModelDto GetProductModelById(int id)
        {
            var productModelEntity = _productModelRepository.GetById(id);

            var editProductModelDto = new EditProductModelDto()
            {

                Name = productModelEntity.Name,
                CategoryId= productModelEntity.CategoryId
            };

            return editProductModelDto;
        }

        public void EditProductModel(EditProductModelDto editProductModelDto)
        {
            var productModelEntity = _productModelRepository.GetById(editProductModelDto.Id);

            productModelEntity.Name = editProductModelDto.Name;


            _productModelRepository.Update(productModelEntity);
        }
    }
}
