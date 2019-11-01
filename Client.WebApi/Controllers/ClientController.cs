using Client.WebApi.Extensions;
using Client.WebApi.Models;
using Client.WebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.WebApi.Controllers
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
            var clients = storageInstance.GetAll(typeof(Models.Entities.Client));

            foreach (var item in clients)
            {
                item.Description = MyExtensionMethods.GetDescription(item);
            }

            return clients.ToList();
        }

        #region snippet_GetByID
        // GET: api/client/5
        [HttpGet("{id}")]
        public ActionResult<Models.Entities.Client> GetClient(Guid id)
        {
            var client = (Models.Entities.Client)storageInstance.Get(typeof(Models.Entities.Client), id);

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
        public ActionResult<Models.Entities.Client> PostClient(Models.Entities.Client client)
        {
            storageInstance.Add(client);

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }
        #endregion

        #region snippet_Update
        // PUT: api/client/5
        [HttpPut("{id}")]
        public IActionResult PutClient(Guid id, Models.Entities.Client client)
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
                var entity = storageInstance.Get(typeof(Models.Entities.Client), id);
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
        public ActionResult<Models.Entities.Client> DeleteClient(Guid id)
        {
            var client = (Models.Entities.Client)storageInstance.Get(typeof(Models.Entities.Client), id);
            if (client == null)
            {
                return NotFound();
            }

            storageInstance.Remove(typeof(Models.Entities.Client), id);

            return client;
        }
        #endregion
    }
}
