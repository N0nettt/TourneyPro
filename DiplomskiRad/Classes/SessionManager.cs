using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public static class SessionManager
    {
        // Static property to store the logged-in user object
        public static User? LoggedInUser { get; private set; }

        // Method to set the session when a user logs in
        public static void SetSession(User user)
        {
            LoggedInUser = user;
            // You can store additional session information from the user object if needed
        }

        // Method to clear the session when a user logs out
        public static void ClearSession()
        {
            LoggedInUser = null;
            // Clear other session variables if any
        }

        // Example method to check if a user is logged in
        public static bool IsUserLoggedIn()
        {
            return LoggedInUser != null;
        }
    }
}
