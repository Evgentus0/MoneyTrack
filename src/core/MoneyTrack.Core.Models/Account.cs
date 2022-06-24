namespace MoneyTrack.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Transaction? LastTransaction { get; set; }
        public User? User { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is Account ac)
            {
                return Id.Equals(ac.Id)
                    && Name is not null ? Name.Equals(ac.Name) : Name == ac.Name
                    && LastTransaction is not null 
                        ? LastTransaction.Equals(ac.LastTransaction) 
                        : LastTransaction == ac.LastTransaction
                    && User is not null ? User.Equals(ac.User) : User == ac.User;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id
                ^ (Name is not null ? Name.GetHashCode() : 0)
                ^ (LastTransaction is not null ? LastTransaction.GetHashCode() : 0)
                ^ (User is not null ? User.GetHashCode() : 0);
        }
    }
}
