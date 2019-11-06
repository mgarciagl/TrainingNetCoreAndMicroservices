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

        // GET: StoreableEntities
        public async Task<IActionResult> Index()
        {
            var storeableEntities = await storageInstance.GetAll();
            storeableEntities = storeableEntities.OrderBy(d => d.Id).ToList();

            foreach (var item in storeableEntities)
            {
                item.Description = MyExtensionMethods.GetDescription(item);
            }

            return View(storeableEntities);
        }

        // GET: StoreableEntities/CreateClient
        public IActionResult CreateClient()
        {
            return View();
        }

        // GET: StoreableEntities/CreateProduct
        public IActionResult CreateProduct()
        {
            return View();
        }

        // POST: StoreableEntities/CreateClient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await storageInstance.Add(client);
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
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await storageInstance.Add(product);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeableEntity = await storageInstance.Get((int)id);

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
        public async Task<IActionResult> EditClient(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await storageInstance.Update(client);
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
        public async Task<IActionResult> EditProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await storageInstance.Update(product);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeableEntity = await storageInstance.Get((int)id);

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
        public async Task<IActionResult> DeletePost(int id)
        {
            var storeableEntity = await storageInstance.Get(id);

            if (storeableEntity == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var result = await storageInstance.Remove(storeableEntity.Id);

                if (result == null)
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
            else if (storeableEntity.GetType().Equals(typeof(Product)))
            {
                return View("DeleteProduct", storeableEntity);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
