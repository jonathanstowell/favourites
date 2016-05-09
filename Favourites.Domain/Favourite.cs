using System;
using System.Collections.Generic;

namespace Favourites.Domain
{
    public class Root<T> where T : class
    {
        public Guid Id { get; set; }
        public Root<T> Parent { get; set; }
        public string Description { get; set; }
        public IList<Root<T>> Children { get; set; }
        public IList<T> Favourites { get; set; }

        public Root()
        {
            Children = new List<Root<T>>();
            Favourites = new List<T>();
        }
    }

    public class Favourite
    {
        public Guid Id { get; set; }
        public string Sedol { get; set; }
    }
}
