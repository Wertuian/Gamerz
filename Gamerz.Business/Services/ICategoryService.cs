using Gamerz.Business.Dtos;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Business.Services
{
    public interface ICategoryService
    {
        bool AddCategory(AddCategoryDto addCategoryDto);

        List<ListCategoryDto> GetCategories();

        EditCategoryDto GetCategoryById(int id);

        void EditCategory(EditCategoryDto editCategoryDto);

        void DeleteCategory(int id);
    }
}
