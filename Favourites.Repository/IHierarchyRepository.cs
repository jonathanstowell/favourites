using System;
using System.Collections.Generic;
using Favourites.Domain;

namespace Favourites.Repository
{
    public interface IHierarchyRepository<T> where T : class
    {
        IList<Root<T>> GetAll();
        Root<T> GetForLevel(Guid id);
        IList<T> GetFavouritesForLevel(Guid id);
        void AddChild(Guid? parent, string description);
        void AddFavourite(Guid? owner, string sedol);
    }
}
