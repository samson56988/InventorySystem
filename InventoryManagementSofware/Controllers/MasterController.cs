
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagementSofware.BuisnessInterface.CategoryService;
using InventoryManagementSofware.BuisnessLogic.CategoryBL;
using InventoryManagementSofware.Models;
using InventoryManagementSofware.BuisnessLogic.SubCategoryBL;
using InventoryManagementSofware.BuisnessLogic.ProductService;
using System.Data.SqlClient;
using InventoryManagementSofware.BuisnessLogic.AuthenticationBL;
using System.Web.Security;
using InventoryManagementSofware.BuisnessLogic.VendorBL;
using System.Data;
using InventoryManagementSofware.BuisnessLogic.PurchaseProductBL;
using InventoryManagementSofware.BuisnessInterface.PurchaseProductService;

namespace InventoryManagementSofware.Controllers
{
    public class MasterController : Controller
    {
        private readonly CategoryRepository db = new CategoryRepository();
        private readonly SubCategoryRespository db2 = new SubCategoryRespository();
        private readonly ProductRepository db3 = new ProductRepository();
        private readonly AuthenticationRepository db4 = new AuthenticationRepository();
        private readonly VendorRepository db5 = new VendorRepository();
        private readonly PurchaseProductRepository db6 = new PurchaseProductRepository();
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-Q19ESHF;Initial Catalog=InventoryDb;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        // GET: Master
        public ActionResult Category(int PageNumber = 1)
        {
            var category = db.GetCategory();
            ViewBag.TotalPages = Math.Ceiling(category.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            category = category.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(category);

        }

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.InsertNew(category);
                TempData["SuccessMessage"] = "Record Saved Successfully";
                return RedirectToAction("Category");
            }
            return View();
        }

        public ActionResult EditCategory()
        {
            return View();
        }

        public ActionResult SubCategory(int PageNumber = 1)
        {
            var subcategory = db2.GetSubCategory();
            ViewBag.TotalPages = Math.Ceiling(subcategory.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            subcategory = subcategory.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(subcategory);
        }

        private List<Category> PopulateCategory()
        {
            var category = db2.PopulateCategory();
            return category;
        }

        private List<Subcategory> Populatesubcategory()
        {
            var category = db3.PopulateSubcategory();
            return category;
        }

        public ActionResult AddSubCategory()
        {
            ViewBag.Category = PopulateCategory();
            return View();
        }

        [HttpPost]
        public ActionResult AddSubCategory(Subcategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db2.InsertNew(subcategory);
                TempData["SuccessMessage"] = "Record Saved Successfully";
                return RedirectToAction("SubCategory");
            }
            return View();
        }

        public ActionResult Product(int PageNumber = 1)
        {

            var product = db3.GetProduct();
            ViewBag.TotalPages = Math.Ceiling(product.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            product = product.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(product);
        }

        public ActionResult AddProduct()
        {
            ViewBag.Subcategory = Populatesubcategory();
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db3.InsertNew(product);
                TempData["SuccessMessage"] = "Record Saved Successfully";
                return RedirectToAction("Product");
            }
            return View();
        }

        public ActionResult ProductStock(int PageNumber = 1)
        {
            var product = db3.GetProductStock();
            ViewBag.TotalPages = Math.Ceiling(product.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            product = product.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(product);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login log)
        {
            
            if (ModelState.IsValid)
            {
                var data = db4.Login(log);
                if (data.Count() > 0)
                {
                    if (log.Role == "Admin")
                    {
                        FormsAuthentication.SetAuthCookie(log.username, true);
                        Session["Username"] = log.username.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(log.username, true);
                        Session["Username"] = log.username.ToString();
                        return RedirectToAction("Cashier", "Home");
                    }
                      

                   
                }

            }
            return View();
        }

        public ActionResult Users(int PageNumber = 1)
        {
            var users = db4.GetUsers();
            ViewBag.TotalPages = Math.Ceiling(users.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            users = users.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(users);
        }

        public ActionResult Createusers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult  Createusers(Login log)
        {
            if (ModelState.IsValid)
            {
                db4.Registerusers(log);
                TempData["SuccessMessage"] = "Record Saved Successfully";
                return RedirectToAction("Users");
            }
            return View();
        }

        public ActionResult Vendor(int PageNumber = 1)
        {          
            var vendor = db5.GetVendor();
            ViewBag.TotalPages = Math.Ceiling(vendor.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            vendor = vendor.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(vendor);
        }

        public ActionResult Createvendor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Createvendor(Vendor vendor)
        {

            if (ModelState.IsValid)
            {
                db5.InsertNew(vendor);
                TempData["SuccessMessage"] = "Record Saved Successfully";
                return RedirectToAction("Vendor");
            }
            return View();
        }

        public ActionResult AddPurchase()
        {
            Purchaseproducts purchase = new Purchaseproducts();
                SqlDataReader dr;
                string sdate = "T" + DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int Count;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 1 TransactionNo from PurchasedProducts where TransactionNo like '" + sdate + "%' order by ID desc";
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    Count = int.Parse(transno.Substring(8, 4));
                    purchase.TransactionNo = sdate + (Count + 1);                  
                }
                else
                {

                    transno = sdate + "1001";
                    purchase.TransactionNo = transno;

                }
                dr.Close();
                con.Close();
            ViewBag.Product = PopulateProduct();
            ViewBag.Vendor = PopulateVendor();
                return View(purchase);
        }

        [HttpPost]
        public ActionResult AddPurchase(Purchaseproducts purchase)
        {
            string Username = (string)Session["Username"];
            if (ModelState.IsValid)
            {
                purchase.PurchasedBy = Username;
                db6.InsertNew(purchase);          
            }
            TempData["SuccessMessage"] = "Item Added Succefully";
            return RedirectToAction("AddPurchase");
        }

        [HttpGet]
        public ActionResult GetPurchase()
        {
            return View();
        }

        private List<Product> PopulateProduct()
        {
            var product = db6.PopulateProduct();
            return product;
        }

        private List<Vendor> PopulateVendor()
        {
            var vendor= db6.PopulateVendor();
            return vendor;
        }
       
        public ActionResult ConfirmPurchase(string user)
        {



            SqlDataReader dr;
           
            user = (string)Session["Username"];
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select sum(Total) from PurchasedProducts where Status = 'Pending' and PurchaseBy = '"+user+"' ";
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
              var sum = dr[0].ToString();
              ViewBag.Sum = sum;

            }

            
            var confirm = db6.ConfirmPurchasedProduct(user);
            return View(confirm);
        }

        [HttpPost]
        public ActionResult ConfirmPurchase( )
        {
            string user;
            user = (string)Session["Username"];
            SqlDataReader dr;
            con.Open();
            string sdate = "T" + DateTime.Now.ToString("yyyyMMdd");
            string transno;
            string Transno;
            int Count;
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "select top 1 TransactionNo from PurchasedProducts where TransactionNo like '" + sdate + "%' order by ID desc";
            dr = cmd3.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                transno = dr[0].ToString();
                Count = int.Parse(transno.Substring(8, 4));
                Transno = sdate + (Count + 1);
            }
            else
            {

                transno = sdate + "1001";
                Transno = transno;
            }
            dr.Close();

            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "Update PurchasedProducts set Status = 'Sold' , TransactionNo = '" + Transno + "' where Status = 'Pending' and PurchaseBy = '" + user + "' ";
            cmd2.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("AddPurchase");
        }

        public ActionResult TransactionList(int PageNumber = 1)
        {
            var Transaction = db6.TransactionList();
            ViewBag.TotalPages = Math.Ceiling(Transaction.Count() / 10.0);
            ViewBag.PageNumber = PageNumber;
            Transaction = Transaction.Skip((PageNumber - 1) * 10).Take(10).ToList();
            return View(Transaction);
        }


    }
}