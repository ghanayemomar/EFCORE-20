using C01.DBTransactions.Data;
using EFCORE_20.Entities;

namespace C01.DBTransactions.Helpers
{
    public static class DatabaseHelper
    {
        public static void RecreateCleanDatabase()
        {
            using var context = new AppDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void PopulateDatabase()
        {
            using (var context = new AppDbContext())
            {
                context.Add(
                    new BankAccount
                    {
                        AccountId = "1",
                        AccountHolder = "Ahmed Ali",
                        CurrentBalance = 10000m
                    });

                context.Add(
                    new BankAccount
                    {
                        AccountId = "2",
                        AccountHolder = "Reem Ali",
                        CurrentBalance = 15000
                    });

                context.SaveChanges();
            }
        }
    }
}
