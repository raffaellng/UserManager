namespace UserManager.Domain.Validation
{
    public class DomainValidation : Exception
    {
        public DomainValidation(string error): base(error) { }
        
        public static void When(bool HasError, string error)
        {
            if (HasError) 
                throw new DomainValidation(error);
        }
    }
}
