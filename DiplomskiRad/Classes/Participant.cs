using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiRad.Classes
{
    public class Participant
    {
        public int ParticipantID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        

        public string GetName()
        {
            return this.Name;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public Participant(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        public Participant(int ParticipantID, string Name, string Email)
        {
            this.Name = Name;
            this.Email = Email;
            this.ParticipantID = ParticipantID;
        }

        public string GetEmail()
        {
            return Email;
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }



    }
}
