using System;
using System.Collections.Generic;
using System.Linq;
using EfCoreV3Bugs.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreV3Bugs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Arrange:
            // remove DB, create DB, run migrations, add TestEntity
            DataContext.ConnectionString = "server=.;database=EfCoreV3Bugs;trusted_connection=true;";
            using (var context = new DataContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.Migrate();

                var entity = new TestEntity()
                {
                    VoCollection = new HashSet<ValueObject1>(new List<ValueObject1>() {new ValueObject1(false)})
                };
                context.TestEntities.Add(entity);
                context.SaveChanges();

                // Verify same scenario in memory
                if (!entity.VoCollection.Contains(new ValueObject1(false)))
                {
                    throw new Exception("Value object not found in memory in original object, but should be 1");
                }
            }
            
            // Act & Assert:
            using (var context = new DataContext())
            {
                var entity = context.TestEntities.Include(c=>c.VoCollection).First();

                if (entity.VoCollection.All(c => c != new ValueObject1(false)))
                {
                    throw new Exception("Value object not found using 'Any', but should be");
                }

                if (entity.VoCollection.Contains(new ValueObject1(false)) == false)
                {
                    // TODO: exception thrown only on this step
                    throw new Exception("Value object not found using 'Contains', but should be");
                }
            }
        }
    }
}
