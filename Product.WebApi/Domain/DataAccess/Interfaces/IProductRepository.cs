using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Product.WebApi.Domain.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        List<Entities.Product> GetAll();

        Entities.Product Get(int id);

        EntityEntry<Entities.Product> Add(Entities.Product product);

        EntityEntry<Entities.Product> Update(Entities.Product product);

        bool Remove(int id);

    }
}
