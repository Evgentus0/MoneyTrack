namespace MoneyTrack.Core.AppServices.DTOs
{
    public abstract class BaseDto
    {
        public abstract string GetErrorString();
        public virtual string GetErrorIncludeInner()
        {
            return GetErrorString();
        }
    }
}
