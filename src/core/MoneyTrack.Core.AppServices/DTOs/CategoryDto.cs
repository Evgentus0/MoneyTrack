namespace MoneyTrack.Core.AppServices.DTOs
{
    public class CategoryDto: BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }

        public override string GetErrorString()
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(Name))
            {
                error += "Name can not be empty";
            }
            return error;
        }
    }
}