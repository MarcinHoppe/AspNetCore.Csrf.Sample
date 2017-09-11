using System.Collections.Generic;

namespace AspNetCore.Csrf.Sample.Model
{
    public class InMemoryProfileRepository : IProfileRepository
    {
        private readonly Dictionary<string, Profile> profiles;

        public InMemoryProfileRepository()
        {
            profiles = new Dictionary<string, Profile>
            {
                {
                    "marcin",
                    new Profile
                    {
                        FirstName = "Marcin",
                        LastName = "Hoppe",
                        Email = "marcin.hoppe@acm.org",
                        BankAccount = "111222333444"
                    }
                },
                {
                    "janek",
                    new Profile
                    {
                        FirstName = "Janek",
                        LastName = "Kowalski",
                        Email = "janek.kowalski@yahoo.com",
                        BankAccount = "222333444555"
                    }
                },
                {
                    "franek",
                    new Profile
                    {
                        FirstName = "Franek",
                        LastName = "Wasilewski",
                        Email = "franek.wasilewski@hotmail.com",
                        BankAccount = "333444555666"
                    }
                }
            };
        }

        public Profile GetProfileForUser(string login)
        {
            return profiles[login];
        }

        public void UpdateProfileForUser(string login, Profile newProfile)
        {
            var profile = profiles[login];

            if (!string.IsNullOrWhiteSpace(newProfile.FirstName))
            {
                profile.FirstName = newProfile.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(newProfile.LastName))
            {
                profile.LastName = newProfile.LastName;
            }
            if (!string.IsNullOrWhiteSpace(newProfile.Email))
            {
                profile.Email = newProfile.Email;
            }
            if (!string.IsNullOrWhiteSpace(newProfile.BankAccount))
            {
                profile.BankAccount = newProfile.BankAccount;
            }
        }
    }
}