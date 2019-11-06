using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Client.WebApi.Domain.DataAccess.Interfaces
{
    public interface IClientRepository
    {
        List<Entities.Client> GetAll();

        Entities.Client Get(int id);

        EntityEntry<Entities.Client> Add(Entities.Client client);

        EntityEntry<Entities.Client> Update(Entities.Client client);

        bool Remove(int id);

    }
}
