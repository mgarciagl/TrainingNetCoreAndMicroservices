using Client.WebApi.Domain.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.WebApi.Domain.DataAccess
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _clientContext;

        public ClientRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }

        public List<Entities.Client> GetAll()
        {
            return _clientContext.Clients.ToList();
        }

        public Entities.Client Get(int id)
        {
            return _clientContext.Clients.FirstOrDefault(d => d.Id == id);
        }

        public EntityEntry<Entities.Client> Add(Entities.Client client)
        {
            var clientWithId = _clientContext.Clients.Add(client);

            _clientContext.SaveChanges();

            return clientWithId;
        }

        public EntityEntry<Entities.Client> Update(Entities.Client client)
        {
            var clientUpdated = _clientContext.Clients.Update(client);

            _clientContext.SaveChanges();

            return clientUpdated;
        }

        public bool Remove(int id)
        {
            try
            {
                var clientToRemove = _clientContext.Clients.FirstOrDefault(d => d.Id == id);
                if (clientToRemove != null)
                {
                    _clientContext.Clients.Remove(clientToRemove);

                    _clientContext.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // LOGUEAR
                return false;
                //throw;
            }

        }
    }
}
