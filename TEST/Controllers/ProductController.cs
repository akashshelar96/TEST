using Microsoft.AspNetCore.Mvc;
using TEST.Models;
using Microsoft.AspNetCore.Mvc;
using TEST.Data;
using TEST.Models;
using System.Drawing.Design;
using System.Linq;
using System.Threading.Tasks;

namespace TEST.Controllers


{
    public class ProductController : Controller
    {
        private readonly ApplicationContext context;

        public ProductController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int pg = 1)
        {
            var result = context.Products.ToList();
            // pagination
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = result.Count();
            var pagination = new Pagination(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = result.Skip(recSkip).Take(pagination.PageSize).ToList();
            this.ViewBag.Pagination = pagination;
            return View(data);

            //return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                var prd = new Product()
                {
                    ProductName = model.ProductName,
                    
                };
                context.Products.Add(prd);
                context.SaveChanges();
                TempData["error"] = "Record Saved";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Empty field can't submit";
                return View(model);
            }

        }
        public IActionResult Delete(int ProductId)
        {
            var prd = context.Products.SingleOrDefault(p => p.ProductId == ProductId);
            context.Products.Remove(prd);
            context.SaveChanges();
            TempData["error"] = "Record Deleted";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int ProductId)
        {
            var prd = context.Products.SingleOrDefault(p => p.ProductId == ProductId);
            var result = new Product()
            {
                ProductName = prd.ProductName,
                
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            var prd = new Product()
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                
            };
            context.Products.Update(prd);
            context.SaveChanges();
            TempData["error"] = "Product Details Updated";
            return RedirectToAction("Index");
        }
    }
}
