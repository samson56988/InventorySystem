using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSofware.Models;

namespace InventoryManagementSofware.BuisnessInterface.CategoryService
{
    public interface ICategoryRepository
    {
        IList<Category> GetCategory();

        Category GetCategoryId(int? id);

        void InsertNew(Category category);

        void Update(Category category);

        void Delete(Category category);
    }
}
