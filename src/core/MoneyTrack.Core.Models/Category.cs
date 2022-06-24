namespace MoneyTrack.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsSystem { get; set; }
        public User? User { get; set; }

        public const string RealBalanceDiff = "Real balance difference";
        public const string AccChangeCom = "Account change commission";

        public override bool Equals(object? obj)
        {
            if(obj is Category ctg)
            {
                return Id.Equals(ctg.Id)
                    && Name is not null ? Name.Equals(ctg.Name) : Name == ctg.Name
                    && IsSystem.Equals(ctg.IsSystem)
                    && User is not null ? User.Equals(ctg.User) : User == ctg.User;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
                ^ (Name is not null ? Name.GetHashCode() : 0)
                ^ IsSystem.GetHashCode()
                ^ (User is not null ? User.GetHashCode() : 0);
        }
    }
}
