using Product.WebApi.Domain.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Product.WebApi.Controllers
{
    #region ProductController
    [Produces("application/json")]
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        // GET: api/product
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        #region snippet_GetByID
        // GET: api/product/5
        [HttpGet("{id}")]
        public ActionResult<Domain.Entities.Product> GetProduct(int id)
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        #endregion

        #region snippet_Create
        // POST: api/product
        [HttpPost]
        public ActionResult<Domain.Entities.Product> PostProduct(Domain.Entities.Product product)
        {
            var result = _productRepository.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        #endregion

        #region snippet_Update
        // PUT: api/product/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Domain.Entities.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                _productRepository.Update(product);
            }
            catch (Exception /* ex */)
            {
                var entity = _productRepository.Get(id);
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
        public ActionResult<Domain.Entities.Product> DeleteProduct(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Remove(id);

            return product;
        }
        #endregion
    }
}
