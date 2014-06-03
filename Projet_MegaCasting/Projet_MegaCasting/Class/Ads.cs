using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_MegaCasting.Class
{
    class Ads
    {
        #region Propriétés

        public Int64 Id { get; set; }
        public String Title { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String CP { get; set; }
        public String City { get; set; }
        public Int64 ReleaseDate { get; set; }
        public DateTime StartDate { get; set; }
        public String DescriptionJob { get; set; }
        public String DescriptionProfile { get; set; }
        public String DescriptionAgreement { get; set; }
        public String NameCompany { get; set; }
        public String Ref { get; set; }

        #region Constructeurs

        public Ads(String title)
        {
            if (title == "" || title == null)
            {
                throw new Exception("You can't create company with an empty name.");
            }
            this.Title = title;
        }
        #endregion
   }
}
