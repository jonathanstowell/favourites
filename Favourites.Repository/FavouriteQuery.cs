using System;

namespace Favourites.Repository
{
    public class FavouriteQuery
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? FavouriteId { get; set; }
        public string FavouriteSedol { get; set; }
        public int Generation { get; set; }
    }
}