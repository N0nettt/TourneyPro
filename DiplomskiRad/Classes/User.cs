using System;
using System.Text.RegularExpressions;

namespace DiplomskiRad.Classes
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public User(string username, string password, string email, Role role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }
        public bool ValidateUsername()
        {
            return !string.IsNullOrEmpty(Username) && Username.Length >= 3;
        }

        public bool ValidatePassword()
        {
            return !string.IsNullOrEmpty(Password) && Password.Length >= 6;
        }

        public bool ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(Email);
                return addr.Address == Email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValid()
        {
            return ValidateUsername() && ValidatePassword() && ValidateEmail();
        }

        public string GetValidationErrors()
        {
            string errors = string.Empty;

            if (!ValidateUsername())
            {
                errors += "Username must be at least 3 characters long.\n";
            }

            if (!ValidatePassword())
            {
                errors += "Password must be at least 6 characters long.\n";
            }

            if (!ValidateEmail())
            {
                errors += "Email format is invalid.\n";
            }

            return errors;
        }
    }
}
