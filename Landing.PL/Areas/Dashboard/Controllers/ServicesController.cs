using AutoMapper;
using Landing.DAL.Data;
using Landing.DAL.Models;
using Landing.PL.Areas.Dashboard.ViewModels;
using Landing.PL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Landing.PL.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]

    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServicesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public IActionResult Index()
        { // convert from Service to ServicesVM
            var services = context.Services.ToList();
            var servicesVM = mapper.Map<IEnumerable<ServicesVM>>(services);
            return View(servicesVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceFormVM vm)
        {   // Mapping

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // UploadFile : عملناه static class
            var ImageName = FilesSettings.UploadFile(vm.Image, "images"); // بوخذ مني الاتربيوت و اسم الفولدر الي اخزن فيه
            
            var service = mapper.Map<Service>(vm); // <Destination> (Source)
            service.ImageName = ImageName; // هاي انا ضفتها , بدونها مش رح يتعرف عليها ورح يجيب ايرور

            context.Add(service); // OR :  context.Services.Add(service);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var service = context.Services.Find(id);
            if (service is null)
            {
                return NotFound();
            }

            //Mapping (M to VM)
            var serviceModel = mapper.Map<ServiceDetailsVM>(service);

            return View(serviceModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var service = context.Services.Find(id);
            if(service is null)
            {
                return NotFound();
            }

            //Mapping (with ServicesVM) => just (Name,IsDeleted)
            var serviceVM = mapper.Map<ServicesVM>(service);
            return View(serviceVM);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var service = context.Services.Find(id);
            if(service is null)
            {
                return RedirectToAction(nameof(Index));
            }

            // لازم احذف الصورة الي فيها service كل ما احذف 
            FilesSettings.DeleteFile(service.ImageName, "images"); // وبحذف الصورة الي جواها images بوصل مجلد

            context.Services.Remove(service);
            context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

        public IActionResult Edit(int id)
        {
            var service = context.Services.Find(id);
            if(service is null)
            {
                return NotFound();
            }

            var serviceVm = mapper.Map<ServiceFormVM>(service);
            return View(serviceVm);
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ServiceFormVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            
            var service = context.Services.Find(vm.Id);
            if(service is null)
            {
                return NotFound();
            }
            mapper.Map(vm,service); // Mapping from VM to M
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
