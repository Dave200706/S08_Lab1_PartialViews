using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ZombieParty.Models;
using ZombieParty.Models.Data;

namespace ZombieParty.Controllers
{
    public class HuntingLogController : Controller
    {
        private ZombiePartyDbContext _baseDonnees { get; set; }

        public HuntingLogController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            if (id == 0 || id == null)
            {
                //create
                return View(new HuntingLog());
            }
            else
            {
                return View(_baseDonnees.HuntingLogs.Find(id));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(HuntingLog hunting)
        {
            if (ModelState.IsValid)
            {
                if (hunting.Id == 0)
                {
                    _baseDonnees.HuntingLogs.Add(hunting);
                    TempData["Success"] = $"{hunting.Title} hunting created";
                }
                else
                {
                    _baseDonnees.HuntingLogs.Update(hunting);
                    TempData["Success"] = $"{hunting.Title} hunitng created";
                }
                _baseDonnees.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(hunting);
        }
    }
}
