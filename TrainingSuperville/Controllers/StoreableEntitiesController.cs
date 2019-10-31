using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TrainingSuperville.Models;
using TrainingSuperville.Extensions;
using TrainingSuperville.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TrainingSuperville.Controllers
{
    public class StoreableEntitiesController : Controller
    {
        Storage storageInstance = Storage.GetInstance();

        // GET: Students
        public IActionResult Index()
        {
            var storeableEntities = storageInstance.GetAll();
            storeableEntities = storeableEntities.OrderBy(d => d.Id).ToList();

            foreach (var item in storeableEntities)
            {
                item.Description = MyExtensionMethods.GetDescription(item);
            }

            return View(storeableEntities);
        }

        // GET: Students/CreateClient
        public IActionResult CreateClient()
        {
            return View();
        }

        // GET: Students/CreateProduct
        public IActionResult CreateProduct()
        {
            return View();
        }

        // POST: StoreableEntities/CreateClient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateClient(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storageInstance.Add(client);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to create client. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            return View(client);
        }

        // POST: StoreableEntities/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storageInstance.Add(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to create client. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            return View(product);
        }

        // GET: StoreableEntities/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeableEntity = storageInstance.Get((Guid)id);

            if (storeableEntity != null && storeableEntity.GetType().Equals(typeof(Client)))
            {
                return View("EditClient", storeableEntity);
            }
            else if (storeableEntity != null && storeableEntity.GetType().Equals(typeof(Product)))
            {
                return View("EditProduct", storeableEntity);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: StoreableEntities/EditClient/5
        [HttpPost, ActionName("EditClient")]
        [ValidateAntiForgeryToken]
        public IActionResult EditClient(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storageInstance.Update(client);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save storeable entity. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            return View("EditClient", client);
        }

        // POST: StoreableEntities/EditProduct/5
        [HttpPost, ActionName("EditProduct")]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    storageInstance.Update(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save storeable entity. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            return View("EditProduct", product);
        }

        // GET: StoreableEntities/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeableEntity = storageInstance.Get((Guid)id);

            if (storeableEntity != null && storeableEntity.GetType().Equals(typeof(Client)))
            {
                return View("DeleteClient", storeableEntity);
            }
            else if (storeableEntity != null && storeableEntity.GetType().Equals(typeof(Product)))
            {
                return View("DeleteProduct", storeableEntity);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: StoreableEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid id)
        {
            var storeableEntity = storageInstance.Get(id);

            if (storeableEntity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var result = storageInstance.Remove(storeableEntity.Id);

                if (!result)
                {
                    throw new Exception();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to delete storeable entity. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            if (storeableEntity.GetType().Equals(typeof(Client)))
            {
                return View("DeleteClient", storeableEntity);
            }
            else
            {
                return View("DeleteProduct", storeableEntity);
            }
        }
    }
}
