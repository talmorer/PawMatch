using System;

namespace Models
{
    public class AdoptionRequest
    {
        public int PetID { get; set; }
        public int UserRequstingID { get; set; }
        public string RequestDate { get; set; }
        public int IsAdopted { get; set; }

        public string PetName { get; set; }
        public string AdopterFirstName { get; set; }
        public string AdopterPhone { get; set; }

        public string StatusText
        {
            get
            {
                if (IsAdopted == 1) return "Approved";
                if (IsAdopted == 2) return "Rejected";
                return "Pending";
            }
        }
    }
}