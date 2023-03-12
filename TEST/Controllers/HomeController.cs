using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TEST.Data;
using TEST.Models;

namespace TEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;


        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult List()
        {
            List<Product> products = _context.Products
                .Include(p => p.Product)
              .Include(c => c.Category)
              .ToList();
            return View(products);

        }
        private List<SelectListItem> GetCategories()
        {
            var lstCategories = new List<SelectListItem>();
            List<Categories> categories = _context.Categories.ToList();
            lstCategories = categories.Select(c => new SelectListItem()
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = " ",
                Text = "--Select Category--"

            };
            lstCategories.Insert(0, defItem);
            return lstCategories;
        }
        [HttpGet]
        public IActionResult Create()
        {
            Product Product = new Product();
            ViewBag.CategoryId = GetCategories();
            return View(Product);

        }

        [HttpGet]
        public IActionResult Details(int ProductId)
        {
            Product product = GetProduct(ProductId);
            Product product = GetProduct(ProductName);
            ViewBag.CategoryId = GetCategoryName(Product.CategoryId)
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int ProductId)
        {
            Product product = GetProduct(ProductId);
            Product product = GetProduct(ProductName);
            ViewBag.CategoryId = GetCategoryName(Product.CategoryId)
            return View(product);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        [HttpGet]
        public JsonResult GetCategoryIdByCategory(int categoryId)
        {
            List<SelectListItem> Categories = GetCategoryId(CategoryName);
            return Json(Categories);
        }
        private Product GetProduct(int ProductId)
        {
            Product product = _context.Products
                .Where(p => p.ProductId == ProductId).FirstOrDefault();
            return product;
        }

        [HttpGet]
        public IActionResult Edit(int ProductId)
        {
            Product product = GetProduct(ProductId);
            ViewBag.CategoryId = GetCategories();
            return View(product);

        }

        private string GetCategoryName(int CategoryId)
        {
            string CategoryName = _context.Categories.Where(c.CategoryId == CategoryId).SingleOrDefault().CategoryName;
            return CategoryName;
        }
        

    }
}