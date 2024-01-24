using C01.DBTransactions.Data;
using C01.DBTransactions.Helpers;
using EFCORE_20.Entities;

namespace C01.DBTransactions
{
    class Program
    {
        private static Random _random = new();
        public static void Main(string[] args)
        {
            RunChangeWithMultipleSaveChangeInDataBseBestPracticeSavePoints();
        }

        public static void RunInitialTransferWalkThrough()
        {
            var account1 = new BankAccount
            {
                AccountId = "1",
                AccountHolder = "Omer Ghanayem",
                CurrentBalance = 1000,
            };
            var account2 = new BankAccount
            {
                AccountId = "2",
                AccountHolder = "Ghoshwa Ghanayem",
                CurrentBalance = 400,
            };
            var amountToTransfer = 100;
            account1.Withdraw(amountToTransfer);
            account2.Deposit(amountToTransfer);
        }
        public static void RunChangeWithMultipeSaveChange()
        {
            DatabaseHelper.RecreateCleanDatabase();
            DatabaseHelper.PopulateDatabase();
            using (var context = new AppDbContext())
            {
                var account1 = context.BankAccounts.First(x => x.AccountId == "1");
                var account2 = context.BankAccounts.First(x => x.AccountId == "2");

                var amountToTransfer = 100;

                account1.Withdraw(amountToTransfer);
                context.SaveChanges();
                if (_random.Next(0, 3) == 0)
                {
                    throw new Exception();
                }
                account2.Deposit(amountToTransfer);
            }
        }

        public static void RunChangeWithSingleSaveChange()
        {
            DatabaseHelper.RecreateCleanDatabase();
            DatabaseHelper.PopulateDatabase();
            using (var context = new AppDbContext())
            {
                var account1 = context.BankAccounts.First(x => x.AccountId == "1");
                var account2 = context.BankAccounts.First(x => x.AccountId == "2");

                var amountToTransfer = 100;

                account1.Withdraw(amountToTransfer);
                if (_random.Next(0, 1) == 0)
                {
                    throw new Exception();
                }
                account2.Deposit(amountToTransfer);
                context.SaveChanges();

            }
        }
        public static void RunChangeWithMultipleSaveChangeInDataBse()
        {
            DatabaseHelper.RecreateCleanDatabase();
            DatabaseHelper.PopulateDatabase();
            using (var context = new AppDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var account1 = context.BankAccounts.First(x => x.AccountId == "1");
                    var account2 = context.BankAccounts.First(x => x.AccountId == "2");
                    var amountToTransfer = 100;
                    account1.Withdraw(amountToTransfer);
                    context.SaveChanges();
                    account2.Deposit(amountToTransfer);
                    context.SaveChanges();
                    transaction.Commit();
                }

            }
        }

        public static void RunChangeWithMultipleSaveChangeInDataBseBestPractice()
        {
            DatabaseHelper.RecreateCleanDatabase();
            DatabaseHelper.PopulateDatabase();
            using (var context = new AppDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var account1 = context.BankAccounts.First(x => x.AccountId == "1");
                        var account2 = context.BankAccounts.First(x => x.AccountId == "2");
                        var amountToTransfer = 100;
                        //
                        account1.Withdraw(amountToTransfer);
                        context.SaveChanges();
                        //
                        account2.Deposit(amountToTransfer);
                        context.SaveChanges();
                        //
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        transaction.Rollback();
                    }
                }
            }
        }

        public static void RunChangeWithMultipleSaveChangeInDataBseBestPracticeSavePoints()
        {
            DatabaseHelper.RecreateCleanDatabase();
            DatabaseHelper.PopulateDatabase();
            using (var context = new AppDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var account1 = context.BankAccounts.First(x => x.AccountId == "1");
                        var account2 = context.BankAccounts.First(x => x.AccountId == "2");
                        transaction.CreateSavepoint("read_accounts");
                        var amountToTransfer = 100;
                        //
                        account1.Withdraw(amountToTransfer);
                        context.SaveChanges();
                        //
                        transaction.CreateSavepoint("withdraw_done");
                        //
                        account2.Deposit(amountToTransfer);
                        context.SaveChanges();
                        transaction.CreateSavepoint("deposit_done");

                        //
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        transaction.RollbackToSavepoint("withdraw_done");
                    }

                }
            }
        }

    }
}