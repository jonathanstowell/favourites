using System;

namespace Favourites.WebUI.Models
{
    public class FavouriteViewModel
    {
        public Guid? Owner { get; set; }
        public string Sedol { get; set; }
    }
}