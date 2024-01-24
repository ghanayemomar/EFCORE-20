namespace EFCORE_20.Entities
{
    public class BankAccount
    {
        public string AccountId { get; set; }
        public string AccountHolder { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<GLTransaction> GLTransactions { get; set; } = new();

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                CurrentBalance += amount;
                GLTransactions.Add(new GLTransaction(amount, "DEPOSIT", DateTime.Now));
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 0 && CurrentBalance >= amount)
            {
                CurrentBalance -= amount;
                GLTransactions.Add(new GLTransaction(amount * -1, "WITHDRAW", DateTime.Now));
            }
        }
    }

}