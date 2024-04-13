using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            var userCreditService = new UserCreditService();
            DateTime now = DateTime.Now;
            int age =  now.Year - dateOfBirth.Year;
            bool canBeAdded = false;


            age += (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) ? -1 : 0;

            canBeAdded = (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)) && (!email.Contains("@") && !email.Contains(".")) && (age < 21);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            user.HasCreditLimit = (client.Type == "VeryImportantClient") ? false : true;

            using (userCreditService)
                user.CreditLimit = (client.Type == "ImportantClient") ?   userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2 : userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth); ;
            

            canBeAdded = (user.HasCreditLimit && user.CreditLimit < 500) ? true : false;

            if (canBeAdded == true) UserDataAccess.AddUser(user);
            else return false;

            return true;
        }
    }
}
