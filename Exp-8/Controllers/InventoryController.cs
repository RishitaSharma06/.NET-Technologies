using Microsoft.AspNetCore.Mvc;

public class InventoryController : Controller
{
    private readonly InventoryDAL _dal;

    public InventoryController(IConfiguration configuration)
    {
        _dal = new InventoryDAL(configuration);
    }

    public IActionResult Index()
    {
        var products = _dal.GetAllProducts();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _dal.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = _dal.GetProductById(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _dal.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _dal.DeleteProduct(id);
        return RedirectToAction(nameof(Index));
    }
}
