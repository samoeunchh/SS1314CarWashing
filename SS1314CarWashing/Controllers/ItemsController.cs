using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SS1314CarWashing.Models;
using SS1314CarWashing.Services;

namespace SS1314CarWashing.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironmen;
        public ItemsController(IItemService itemService,
            AppDbContext context,
            IWebHostEnvironment webHostEnvironmen)
        {
            _itemService = itemService;
            _context = context;
            this.webHostEnvironmen = webHostEnvironmen;
        }
        // GET: ItemsController
        public async Task<ActionResult> Index()
        {
            return View(await _itemService.GetAll());
        }

        // GET: ItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemsController/Create
        public ActionResult Create()
        {
            ViewData["ItemTypes"] = new SelectList(_context.ItemType, "ItemTypeId", "ItemTypeName");
            ViewData["Models"] = new SelectList(_context.Models, "ModelId", "ModelName");
            ViewData["Brands"] = new SelectList(_context.Brand, "BrandId", "BrandName");
            ViewData["OilTypes"] = new SelectList(_context.OilType, "OilTypeId", "OilTypeName");
            return View();
        }
        private string UploadFile(ItemDTO item)
        {
            var fileName = "";
            if(item.Image != null)
            {
                fileName = Guid.NewGuid().ToString() +"_"+  item.Image.FileName;
                var path = webHostEnvironmen.WebRootPath + "/Uploads/";
                var fullPath =Path.Combine(path, fileName);
                using(var stream=new FileStream(fullPath, FileMode.Create))
                {
                    item.Image.CopyTo(stream);
                }
            }
            return fileName;
        }
        // POST: ItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemDTO item)
        {
            if (ModelState.IsValid)
            {
                var itm = new Item
                {
                    Image=UploadFile(item),
                    IsStock=item.IsStock,
                    ItemId=Guid.NewGuid(),
                    ItemName=item.ItemName,
                    ItemTypeId=item.ItemTypeId,
                    BrandId=item.BrandId,
                    OilTypeId=item.OilTypeId,
                    ModelId=item.ModelId,
                    Price=item.Price,
                    Qty =item.Qty,
                    Size=item.Size
                };
                if (await _itemService.Save(itm))
                {
                    return RedirectToAction("Index");
                }
                
            }
            ViewData["ItemTypes"] = new SelectList(_context.ItemType, "ItemTypeId", "ItemTypeName",item.ItemTypeId);
            ViewData["Models"] = new SelectList(_context.Models, "ModelId", "ModelName",item.ModelId);
            ViewData["Brands"] = new SelectList(_context.Brand, "BrandId", "BrandName",item.BrandId);
            ViewData["OilTypes"] = new SelectList(_context.OilType, "OilTypeId", "OilTypeName",item.OilTypeId);
            return View();
        }

        // GET: ItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            return View(await _itemService.Get(id));
        }

        // POST: ItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Item item)
        {
                if(await _itemService.Delete(id))
                {
                    return RedirectToAction(nameof(Index));
                }
            return View();
        }
    }
}
