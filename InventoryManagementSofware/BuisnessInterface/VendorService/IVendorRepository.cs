using InventoryManagementSofware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSofware.BuisnessInterface.VendorService
{
    public interface IVendorRepository
    {
        IList<Vendor> GetVendor();

        Vendor GetVendorId(int? id);

        void InsertNew(Vendor vendor);

        void Update(Vendor vendor);

        void Delete(Vendor vendor);

        List<Vendor> PopulateVendor();
    }
}
