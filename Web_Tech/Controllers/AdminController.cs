using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Tech.Models;

namespace Web_Tech.Controllers
{
    public class AdminController : Controller
    {
        Project_DbContext db = new Project_DbContext();

        private readonly IWebHostEnvironment webHostEnvironment;
        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;   
        }
        //public IActionResult Index()
        //{
        //    var res = db.Markups.ToList();
        //    return View(res);
        //}

        //public async Task<IActionResult> Index()
        //{
        //    //var collection = await db.Markups.ToListAsync();
        //    //var model = new HomeViewModel();
        //    //model.selectListItems = new List<SelectListItem>();
        //    //foreach (var item in collection)
        //    //{
        //    //    model.selectListItems.Add(new SelectListItem { Text = item.NamePage, Value = item.Id.ToString() });
        //    //}
        //    //return View(model);

        //    var res = db.Markups.ToListAsync();
        //    return View(res);  
        //}

        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Markup markup)
        {
            db.Markups.Add(markup);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Markup user = await db.Markups.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Markup markup = new Markup { Id = id.Value };
                db.Entry(markup).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult CreateDocuments(int id)
        {
            Markup model = new Markup() { Id = id };
            return PartialView();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Markup markup = await db.Markups.FirstOrDefaultAsync(x => x.Id == id);
                if (markup != null)
                    return View(markup);
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult>Edit(Markup markup)
        {
            if (markup != null)
            {
                db.Markups.Update(markup);
                await db.SaveChangesAsync();  
                return RedirectToAction("Index");
            }
            return NotFound();


        }

        public IActionResult Privacy()
        {

            return View();
        }

        public IActionResult Ckeditor()
        {
            return View();

        }


        [HttpPost]
        [RequestSizeLimit(808 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 808 * 1024 * 1024)]
        public IActionResult Ckeditor(Markup markup)
        {

            db.Markups.Add(markup);
            db.SaveChanges();
            return RedirectToAction("Index","Admin");



        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                var filename = upload.FileName;
                var path = Path.Combine(webHostEnvironment.WebRootPath, "Files", filename);
                var stream = new FileStream(path, FileMode.Create);

                upload.CopyToAsync(stream);



                return new JsonResult(new
                {
                    uploaded = 1,
                    fileName = upload.FileName,
                    url = "/Files/" + filename
                });


            }

            return View();
        }

        public IActionResult Index()
        {
            //var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), webHostEnvironment.WebRootPath, "Files"));
            //ViewBag.fileInfo = dir.GetFiles();

            return View(db.Markups.ToList());
        }

        [HttpGet]
        public IActionResult FIleExplorer()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), webHostEnvironment.WebRootPath, "Files"));
            ViewBag.fileInfo = dir.GetFiles();


            return View();
        }
    }
}
