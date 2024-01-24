namespace EFCORE_20.Entities
{
    public class GLTransaction
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public GLTransaction(decimal amount, string notes, DateTime createdAt)
        {

            Amount = amount;
            Notes = notes;
            CreatedAt = createdAt;
        }
    }
}