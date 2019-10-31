using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi1.Models;
using WebApi1.Extensions;
using WebApi1.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi1.Controllers
{
    #region ClientController
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly Storage storageInstance = Storage.GetInstance();
        #endregion

        // GET: api/client
        [HttpGet]
        public ActionResult<IEnumerable<StorableEntity>> GetAll()
        {
            var clients = storageInstance.GetAll(typeof(Client));

            foreach (var item in clients)
            {
                item.Description = MyExtensionMethods.GetDescription(item);
            }

            return clients.ToList();
        }

        #region snippet_GetByID
        // GET: api/client/5
        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(Guid id)
        {
            var client = (Client)storageInstance.Get(typeof(Client), id);

            if (client == null)
            {
                return NotFound();
            }

            client.Description = MyExtensionMethods.GetDescription(client);

            return client;
        }
        #endregion

        #region snippet_Create
        // POST: api/client
        [HttpPost]
        public ActionResult<Client> PostClient(Client client)
        {
            storageInstance.Add(client);

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }
        #endregion

        #region snippet_Update
        // PUT: api/client/5
        [HttpPut("{id}")]
        public IActionResult PutClient(Guid id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            try
            {
                storageInstance.Update(client);
            }
            catch (Exception /* ex */)
            {
                var entity = storageInstance.Get(typeof(Client), id);
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
        // DELETE: api/client/5
        [HttpDelete("{id}")]
        public ActionResult<Client> DeleteClient(Guid id)
        {
            var client = (Client)storageInstance.Get(typeof(Client), id);
            if (client == null)
            {
                return NotFound();
            }

            storageInstance.Remove(typeof(Client), id);

            return client;
        }
        #endregion
    }
}
