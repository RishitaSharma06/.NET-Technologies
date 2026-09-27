using Microsoft.AspNetCore.Mvc;
using ProductCatalog_App.Models;

namespace ProductCatalog_App.Controllers
{
    public class ProductController : Controller
    {
        // Product list
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 50000,
                Category = "Electronics"
            },

            new Product
            {
                Id = 2,
                Name = "Mobile",
                Price = 25000,
                Category = "Electronics"
            },

            new Product
            {
                Id = 3,
                Name = "Chair",
                Price = 5000,
                Category = "Furniture"
            }
        };

        // Display products
        public IActionResult Index()
        {
            return View(products);
        }


        // GET: Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = products.Count + 1;

            products.Add(product);

            return RedirectToAction("Index");
        }


        // GET: Edit
        public IActionResult Edit(int id)
        {
            Product product = products.FirstOrDefault(p => p.Id == id);

            return View(product);
        }


        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            Product existingProduct =
                products.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
            }

            return RedirectToAction("Index");
        }


        // Delete
        public IActionResult Delete(int id)
        {
            Product product =
                products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                products.Remove(product);
            }

            return RedirectToAction("Index");
        }
    }
}
