namespace DentalClinic.UI.Authorization
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, string.Empty, string.Empty, null)
        { }
    }
}
