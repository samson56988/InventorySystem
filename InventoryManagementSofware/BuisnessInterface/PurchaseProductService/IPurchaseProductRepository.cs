using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSofware.Models;

namespace InventoryManagementSofware.BuisnessInterface.PurchaseProductService
{
    public interface IPurchaseProductRepository
    {
        IList<Purchaseproducts> GetPurchaseProduct();

        Purchaseproducts GetPurchaseProductID(int? id);

        void InsertNew(Purchaseproducts purchase);

        void Update(Purchaseproducts purchase);

        void Delete(Purchaseproducts purchase);

        List<Product> PopulateProduct();

        List<Vendor> PopulateVendor();

        IList<Purchaseproducts> ConfirmPurchasedProduct(string user);

        IList<Purchaseproducts> TransactionList();
    }
}
