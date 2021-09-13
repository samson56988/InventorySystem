using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSofware.Models;

namespace InventoryManagementSofware.BuisnessInterface.ProductService
{
    public interface IProductRepository
    {
        IList<Product> GetProduct();

        Product GetProduct(int? id);

        void InsertNew(Product product);

        void Update(Product product);

        void Delete(Product product);

        List<Subcategory> PopulateSubcategory();

        IList<Product> GetProductStock();
        


    }
}
