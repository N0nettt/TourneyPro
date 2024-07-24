using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        // Constructor
        public Role(int roleId, string roleName)
        {
            RoleID = roleId;
            RoleName = roleName;
        }

        // Default constructor for ORM frameworks
        public Role() { }

        // Additional methods if needed
        // For example, a method to display role information
        public override string ToString()
        {
            return $"RoleID: {RoleID}, RoleName: {RoleName}";
        }
    }
}
