using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi2.Models;
using WebApi2.Extensions;
using WebApi2.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi2.Controllers
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
            var products = storageInstance.GetAll(typeof(Product));

            foreach (var item in products)
            {
                item.Description = MyExtensionMethods.GetDescription(item);
            }

            return products.ToList();
        }

        #region snippet_GetByID
        // GET: api/product/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(Guid id)
        {
            var product = (Product)storageInstance.Get(typeof(Product), id);

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
        public ActionResult<Product> PostProduct(Product product)
        {
            storageInstance.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        #endregion

        #region snippet_Update
        // PUT: api/product/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(Guid id, Product product)
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
                var entity = storageInstance.Get(typeof(Product), id);
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
        public ActionResult<Product> DeleteProduct(Guid id)
        {
            var product = (Product)storageInstance.Get(typeof(Product), id);
            if (product == null)
            {
                return NotFound();
            }

            storageInstance.Remove(typeof(Product), id);

            return product;
        }
        #endregion
    }
}
