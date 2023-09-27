using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> obj = _context.category.ToList();


            return View(obj);
        }
        public IActionResult Create()
        {
           


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The displayorder cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _context.category.Add(obj);

                _context.SaveChanges();
                TempData["success"] = "Category Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();   
            }
            var categoryListFromDb = _context.category.Find(id);
            //SingleOrDefault- It throws exception if we get more than one element.Dont throw exception if didnt found any element.
            // we can use SIngleOrDefault- var categoryListFromDb = _context.category.SingleorDefault(u => u.id==id);
            //SIngle - It will throw exception if if didnt found any element;
            //FirstOrDefault - It will return first data if multile element exists in database.
            // we can use FirstOrDefault - var categoryListFromDb = _context.category.FirstOrDefault(u => u.id==id);
            //Find- It will return unique value thats why it is used above bcz id is primary key.

            if(categoryListFromDb == null)
            {
                return NotFound();
            }
            return View(categoryListFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The displayorder cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _context.category.Update(obj);

                _context.SaveChanges();
                TempData["success"] = "Updated Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryListFromDb = _context.category.Find(id);

            if (categoryListFromDb == null)
            {
                return NotFound();
            }
            return View(categoryListFromDb);
        }
        [HttpPost , ActionName("Delete")]
        
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.category.Remove(obj);

                _context.SaveChanges();
                TempData["success"] = "Deleted Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
