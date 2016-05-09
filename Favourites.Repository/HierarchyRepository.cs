using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Favourites.Domain;

namespace Favourites.Repository
{
    public class HierarchyRepository : IHierarchyRepository<Favourite>
    {
        public IList<Root<Favourite>> GetAll()
        {
            using (var conn = new SqlConnection(@"Server=.\SQLExpress;Database=Favourites;Trusted_Connection=True;"))
            {
                var ret = new List<Root<Favourite>>();

                conn.Open();

                var results = conn.Query<FavouriteQuery>(@"
                    SELECT h.Id AS Id, h.Description AS Description, h.ParentId AS ParentId, f.Id AS FavouriteId, f.Sedol AS FavouriteSedol
                    FROM Hierarchy AS h
                    LEFT JOIN Hierarchy AS ph ON h.ParentId = ph.Id
	                LEFT JOIN Favourites f ON h.Id = f.HierarchyId
                ")
                .ToList();

                var parents = results
                    .Where(x => x.ParentId == null)
                    .GroupBy(x => x.Id)
                    .ToDictionary(x => x.Key, x => x.ToList());

                var children = results
                    .Where(x => x.ParentId != null)
                    .GroupBy(x => x.ParentId.Value)
                    .ToDictionary(x => x.Key, x => x.ToList());

                foreach (var parent in parents)
                {
                    ret.Add(MapNormal(parent.Value, children));
                }

                return ret;
            }
        }

        public Root<Favourite> GetUser(Guid id)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLExpress;Database=Favourites;Trusted_Connection=True;"))
            {
                conn.Open();

                var results = conn.Query<FavouriteQuery>(@"
                    WITH nodeParents AS
                    (
                        SELECT h.Id, h.Description, h.ParentId, 0 as Generation
                            FROM Hierarchy h
		                    WHERE h.Id = @id
                        UNION ALL
                        SELECT sh.Id, sh.Description, sh.ParentId, np.Generation + 1
                            FROM Hierarchy sh 
		                    JOIN nodeParents AS np ON sh.Id = np.ParentId
                    ),
                    nodeFavourites AS
                    (
	                    SELECT h.Id AS Id, h.Description AS Description, h.ParentId AS Parent, hf.Id AS FavouriteId, hf.Sedol AS FavouriteSedol, 0 AS Generation
		                    FROM Hierarchy h
		                    INNER JOIN Favourites hf ON hf.HierarchyId = h.Id
		                    WHERE h.Id = @id
	                    UNION ALL
	                    SELECT np.Id AS Id, np.Description AS Description, np.ParentId AS Parent, f.Id AS FavouriteId, f.Sedol AS FavouriteSedol, np.Generation AS Generation
		                    FROM  nodeParents np
		                    INNER JOIN Favourites f ON f.HierarchyId = np.Id
		                    WHERE np.Id <> @id
                    )

                    SELECT nf.Id AS Id, nf.Description AS Description, nf.Parent AS ParentId, nf.FavouriteId AS FavouriteId, nf.FavouriteSedol AS FavouriteSedol, nf.Generation AS Generation
                    FROM nodeFavourites nf
                    ORDER BY nf.Generation ASC
                    OPTION(MAXRECURSION 32767)
                ", new {id})
                    .GroupBy(x => x.Id)
                    .ToDictionary(x => x.Key, x => x.ToList());

                var ret = Map(results[id], results);

                return ret;
            }
        }

        public IList<Favourite> GetForUser(Guid id)
        {
            using (var conn = new SqlConnection(@"Server=.\SQLExpress;Database=Favourites;Trusted_Connection=True;"))
            {
                conn.Open();

                return conn.Query<Favourite>(@"
                    WITH nodeParents AS
                    (
                        SELECT h.Id, h.Description, h.ParentId, 0 as Generation
                            FROM Hierarchy h
		                    WHERE h.Id = @id
                        UNION ALL
                        SELECT sh.Id, sh.Description, sh.ParentId, np.Generation + 1
                            FROM Hierarchy sh 
		                    JOIN nodeParents AS np ON sh.Id = np.ParentId
                    ),
                    nodeFavourites AS
                    (
	                    SELECT h.Id AS Id, h.Description AS Description, h.ParentId AS Parent, hf.Id AS FavouriteId, hf.Sedol AS FavouriteSedol, 0 AS Generation
		                    FROM Hierarchy h
		                    INNER JOIN Favourites hf ON hf.HierarchyId = h.Id
		                    WHERE h.Id = @id
	                    UNION ALL
	                    SELECT np.Id AS Id, np.Description AS Description, np.ParentId AS Parent, f.Id AS FavouriteId, f.Sedol AS FavouriteSedol, np.Generation AS Generation
		                    FROM  nodeParents np
		                    INNER JOIN Favourites f ON f.HierarchyId = np.Id
		                    WHERE np.Id <> @id
                    )

                    SELECT nf.FavouriteId AS Id, nf.FavouriteSedol AS Sedol
                    FROM nodeFavourites nf
                    WHERE Generation = (SELECT MIN(nf2.Generation) FROM nodeFavourites nf2)
                    OPTION(MAXRECURSION 32767)
                ", new { id })
                    .ToList();
            }
        }

        private static Root<Favourite> MapNormal(List<FavouriteQuery> favouriteQueries, Dictionary<Guid, List<FavouriteQuery>> records, Root<Favourite> parent = null)
        {
            var ret = new Root<Favourite>
            {
                Id = favouriteQueries[0].Id,
                Description = favouriteQueries[0].Description
            };

            foreach (var row in favouriteQueries)
            {
                ret.Favourites.Add(new Favourite
                {
                    Id = row.FavouriteId,
                    Sedol = row.FavouriteSedol
                });
            }

            if (parent != null)
                ret.Parent = parent;

            if (records.ContainsKey(ret.Id))
            {
                ret.Children.Add(MapNormal(records[ret.Id], records, ret));
            }

            return ret;
        }

        private static Root<Favourite> Map(List<FavouriteQuery> favouriteQueries, Dictionary<Guid, List<FavouriteQuery>> records, Root<Favourite> child = null)
        {
            var ret = new Root<Favourite>
            {
                Id = favouriteQueries[0].Id,
                Description = favouriteQueries[0].Description
            };

            foreach (var row in favouriteQueries)
            {
                ret.Favourites.Add(new Favourite
                {
                    Id = row.FavouriteId,
                    Sedol = row.FavouriteSedol
                });
            }

            if (child != null)
                ret.Children.Add(child);

            if (favouriteQueries[0].ParentId != null)
                ret.Parent = Map(records[favouriteQueries[0].ParentId.Value], records, ret);

            return ret;
        }
    }
}