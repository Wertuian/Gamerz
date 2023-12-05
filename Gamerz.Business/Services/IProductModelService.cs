using Gamerz.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Business.Services
{
    public interface IProductModelService
    {
        bool AddProductModel(AddProductModelDto addProductModelDto);

        List<ListProductModelDto> GetProductModels();

        void EditProductModel(EditProductModelDto editProductModelDto);

        EditProductModelDto GetProductModelById(int id);

        void DeleteProductModel(int id);
    }
}
