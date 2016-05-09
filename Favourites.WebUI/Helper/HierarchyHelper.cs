using System.Web;
using System.Web.Mvc;
using Favourites.Domain;

namespace Favourites.WebUI.Helper
{
    public static class HierarchyHelper
    {
        public static IHtmlString OutputHierarchy(this HtmlHelper helper, Root<Favourite> item)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var ret = "<li><a href=\"" + urlHelper.Action("Details", new { level = item.Id }) + "\">" + item.Description + "</a> <a href=\"" + urlHelper.Action("AddLevel", new { parent = item.Id }) + "\">Add Level</a> <a href=\"" + urlHelper.Action("AddFavourite", new { owner = item.Id }) + "\">Add Favourite</a></li><ul>";

            foreach (var favourite in item.Favourites)
            {
                ret += "<li>" + favourite.Sedol + "</li>";
            }

            ret += "</ul><ul>";

            foreach (var child in item.Children)
            {
                ret += OutputHierarchy(helper, child);
            }

            ret += "</ul>";

            return new HtmlString(ret);
        }

        public static IHtmlString OutputHierarchyFromLeaf(this HtmlHelper helper, Root<Favourite> item)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var ret = "<li><a href=\"" + urlHelper.Action("Details", new { level = item.Id }) + "\">" + item.Description + "</a> <a href=\"" + urlHelper.Action("AddLevel", new { parent = item.Id }) + "\">Add Level</a> <a href=\"" + urlHelper.Action("AddFavourite", new { owner = item.Id }) + "\">Add Favourite</a></li><ul>";

            foreach (var favourite in item.Favourites)
            {
                ret += "<li>" + favourite.Sedol + "</li>";
            }

            ret += "</ul><ul>";

            if (item.Parent != null)
            {
                ret += OutputHierarchyFromLeaf(helper, item.Parent);
            }

            ret += "</ul>";

            return new HtmlString(ret);
        }
    }
}