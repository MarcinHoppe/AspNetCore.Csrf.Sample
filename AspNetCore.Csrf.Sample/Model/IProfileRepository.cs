namespace AspNetCore.Csrf.Sample.Model
{
    public interface IProfileRepository
    {
        Profile GetProfileForUser(string login);
        void UpdateProfileForUser(string login, Profile newProfile);
    }
}