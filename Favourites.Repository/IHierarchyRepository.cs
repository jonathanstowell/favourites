using System;
using System.Collections.Generic;
using Favourites.Domain;

namespace Favourites.Repository
{
    public interface IHierarchyRepository<T> where T : class
    {
        Root<Favourite> GetUser(Guid id);
        IList<T> GetForUser(Guid id);
    }
}
