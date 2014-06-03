using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_MegaCasting.Class
{
    class Company
    {
        #region Propriétés

        public Int64 Id { get; set; }
        public String Name { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String CP { get; set; }
        public String City { get; set; }
        public String Sector { get; set; }

        #region Constructeurs

        public Company(String name)
        {
            if (name == "" || name == null)
            {
                throw new Exception("You can't create company with an empty name.");
            }
            this.Name = name;
        }
        #endregion
    }
}
