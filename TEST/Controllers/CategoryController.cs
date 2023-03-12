using Microsoft.AspNetCore.Mvc;


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
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = result.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);

            //return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var c = new Category()
                {
                    CategoryName = model.CategoryName
                };
                context..Add(c);
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
        public IActionResult Delete(int CategoryId)
        {
            var c = context.Category.SingleOrDefault(p => c.CategoryId == CategoryId);
            context.Category.Remove(c);
            context.SaveChanges();
            TempData["error"] = "Record Deleted";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int CategoryId)
        {
            var c = context.Category.SingleOrDefault(c => c.Category == CategoryId);
            var result = new Category()
            {
              
                CategoryName = c.CategoryName
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            var c = new Category()
            {
                
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName
            };
            context.Category.Update(c);
            context.SaveChanges();
            TempData["error"] = "Category Details Updated";
            return RedirectToAction("Index");
        }
    }
}
