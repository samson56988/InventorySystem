using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementSofware.Models;

namespace InventoryManagementSofware.BuisnessInterface.SubCategoryService
{
    public interface ISubCategoryRepository
    {
        IList<Subcategory> GetSubCategory();

        Subcategory GetCategoryId(int? id);

        void InsertNew(Subcategory subcategory);

        void Update(Subcategory subcategory);

        void Delete(Subcategory subcategory);

        List<Category> PopulateCategory();
    }
}