using System;
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

                context.TestEntities.Add(new TestEntity()
                {
                    VO1 = new ValueObject1(false),
                    VO2 = new ValueObject2(null)
                });
                context.SaveChanges();
            }

            // Approve it works for NonNullable:
            using (var context = new DataContext())
            {
                var entity = context.TestEntities.First();
                entity.VO1 = new ValueObject1(true);
                context.SaveChanges();

            }

            // Act & Assert:
            using (var context = new DataContext())
            {
                var entity = context.TestEntities.First();
                entity.VO2 = new ValueObject2("Non empty");
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    // TODO: It fails with:
                    /*
                     System.InvalidOperationException: The entity of type 'ValueObject2' is sharing the table 'TestEntities' with entities of type 'TestEntity', but there is no entity of this type with the same key value that has been marked as 'Added'. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the key values.
   at Microsoft.EntityFrameworkCore.Update.Internal.CommandBatchPreparer.Validate(Dictionary`2 sharedTablesCommandsMap)
   at Microsoft.EntityFrameworkCore.Update.Internal.CommandBatchPreparer.CreateModificationCommands(IList`1 entries, IUpdateAdapter updateAdapter, Func`1 generateParameterName)
   at Microsoft.EntityFrameworkCore.Update.Internal.CommandBatchPreparer.BatchCommands(IList`1 entries, IUpdateAdapter updateAdapter)+MoveNext()
   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.Execute(DbContext _, ValueTuple`2 parameters)
   at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.Execute[TState,TResult](TState state, Func`3 operation, Func`3 verifySucceeded)
   at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.Execute(IEnumerable`1 commandBatches, IRelationalConnection connection)
   at Microsoft.EntityFrameworkCore.Storage.RelationalDatabase.SaveChanges(IList`1 entries)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChanges(IList`1 entriesToSave)
   at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChanges(Boolean acceptAllChangesOnSuccess)
   at Microsoft.EntityFrameworkCore.DbContext.SaveChanges(Boolean acceptAllChangesOnSuccess)
   at Microsoft.EntityFrameworkCore.DbContext.SaveChanges()
   at EfCoreV3Bugs.Program.Main(String[] args) in C:\Projects\EfCore3VOBugs\EfCoreV3Bugs\Program.cs:line 45
                     */

                    // TODO: but should not
                    Console.WriteLine(e);
                }
            }
        }
    }
}
