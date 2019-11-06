using Client.WebApi.Domain.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Client.WebApi.Controllers
{
    #region ClientController
    [Produces("application/json")]
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        #endregion

        // GET: api/client
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_clientRepository.GetAll());
        }

        #region snippet_GetByID
        // GET: api/client/5
        [HttpGet("{id}")]
        public ActionResult<Domain.Entities.Client> GetClient(int id)
        {
            var client = _clientRepository.Get(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }
        #endregion

        #region snippet_Create
        // POST: api/client
        [HttpPost]
        public ActionResult<Domain.Entities.Client> PostClient(Domain.Entities.Client client)
        {
            var result = _clientRepository.Add(client);

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }
        #endregion

        #region snippet_Update
        // PUT: api/client/5
        [HttpPut("{id}")]
        public IActionResult PutClient(int id, Domain.Entities.Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            try
            {
                _clientRepository.Update(client);
            }
            catch (Exception /* ex */)
            {
                var entity = _clientRepository.Get(id);
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
        public ActionResult<Domain.Entities.Client> DeleteClient(int id)
        {
            var client = _clientRepository.Get(id);
            if (client == null)
            {
                return NotFound();
            }

            _clientRepository.Remove(id);

            return client;
        }
        #endregion
    }
}
