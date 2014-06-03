using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_MegaCasting.Class
{
    class User
    {
        #region Propriétés

        public Int64 Id { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Job { get; set; }
        public String Address { get; set; }
        public String CP { get; set; }
        public String City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Civility Civility { get; set; }


        public String FullName
        {
            get
            {
                return this.Civility.ShortLibel + " " + this.LastName + " " + this.FirstName;
            }
        }


        public Int64 IdCivility { get; set; }
        #endregion

        #region Constructeurs

        public User(String lastname, Civility civility)
        {
            if (civility != null)
            {
                if (lastname == "" || lastname == null)
                {
                    throw new Exception("You can't create contact with an empty name.");
                }
                this.LastName = lastname;
                this.Civility = civility;
                this.IdCivility = civility.Id;
            }
            else
            {
                new Exception("You can't create user without civility.");
            }
        }
        #endregion

    }
}
