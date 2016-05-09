using System;
using Favourites.Repository;

namespace Favourites.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new HierarchyRepository();

            var favAtUserLevel = service.GetForUser(new Guid("9AAB5D59-E96D-42E5-911D-B8EDC9CE39F3"));

            service.GetUser(new Guid("9AAB5D59-E96D-42E5-911D-B8EDC9CE39F3"));
            service.GetAll();

            System.Console.Out.WriteLine("Favourites at User Level");

            foreach (var favourite in favAtUserLevel)
            {
                System.Console.Out.WriteLine(favourite.Sedol);
            }

            var favAtSubCompanyLevel = service.GetForUser(new Guid("FD149E82-6E93-49A5-8573-41FC0B1B5957"));

            System.Console.Out.WriteLine("Favourites at Sub Company Level");

            foreach (var favourite in favAtSubCompanyLevel)
            {
                System.Console.Out.WriteLine(favourite.Sedol);
            }

            var favAtCompanyLevel = service.GetForUser(new Guid("0905C1EE-4F76-43B5-954A-DBD062C3B1CC"));

            System.Console.Out.WriteLine("Favourites at Company Level");

            foreach (var favourite in favAtCompanyLevel)
            {
                System.Console.Out.WriteLine(favourite.Sedol);
            }

            System.Console.ReadLine();
        }
    }
}
