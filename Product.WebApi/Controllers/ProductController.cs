using Microsoft.AspNetCore.Mvc;
using Product.WebApi.Extensions;
using Product.WebApi.Models;
using Product.WebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.WebApi.Controllers
{
    #region ProductController
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Storage storageInstance = Storage.GetInstance();
        #endregion

        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<StorableEntity>> GetAll()
        {
            var products = storageInstance.GetAll(typeof(Models.Entities.Product));

            foreach (var item in products)
            {
                item.Description = MyExtensionMethods.GetDescription(item);
            }

            return products.ToList();
        }

        #region snippet_GetByID
        // GET: api/product/5
        [HttpGet("{id}")]
        public ActionResult<Models.Entities.Product> GetProduct(Guid id)
        {
            var product = (Models.Entities.Product)storageInstance.Get(typeof(Models.Entities.Product), id);

            if (product == null)
            {
                return NotFound();
            }

            product.Description = MyExtensionMethods.GetDescription(product);

            return product;
        }
        #endregion

        #region snippet_Create
        // POST: api/product
        [HttpPost]
        public ActionResult<Models.Entities.Product> PostProduct(Models.Entities.Product product)
        {
            storageInstance.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        #endregion

        #region snippet_Update
        // PUT: api/product/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(Guid id, Models.Entities.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                storageInstance.Update(product);
            }
            catch (Exception /* ex */)
            {
                var entity = storageInstance.Get(typeof(Models.Entities.Product), id);
                if (entity == null)
                {
                    return NotFound();
                }
                else
                {
                    return Conflict(); ;
                }
            }

            return NoContent();
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public ActionResult<Models.Entities.Product> DeleteProduct(Guid id)
        {
            var product = (Models.Entities.Product)storageInstance.Get(typeof(Models.Entities.Product), id);
            if (product == null)
            {
                return NotFound();
            }

            storageInstance.Remove(typeof(Models.Entities.Product), id);

            return product;
        }
        #endregion
    }
}
