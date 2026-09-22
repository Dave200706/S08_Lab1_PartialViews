using Microsoft.AspNetCore.Mvc;
using ZombieParty.Models;
using ZombieParty.Models.Data;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class WeaponController : Controller
    {
        private ZombiePartyDbContext _baseDonnees { get; set; }

        public WeaponController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            List<Weapon> weapons = _baseDonnees.Weapons.ToList();
            return View(weapons);
        }

        public IActionResult Upsert(int? id)
        {
            if(id ==0|| id == null)
            {
                //create
                return View(new Weapon());
            }
            else
            {
                return View(_baseDonnees.Weapons.Find(id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                // Ajouter à la BD
                if (weapon.WeaponId == 0)
                {


                    _baseDonnees.Weapons.Add(weapon);
                    TempData["Success"] = $"{weapon.Name} weapon added";
                }
                else
                {
                    _baseDonnees.Weapons.Update(weapon);
                    TempData["Success"] = $"{weapon.Name} weapon added";
                }
                _baseDonnees.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(weapon);
        }
    }
}
