using System;

namespace Models
{
    public class AdoptionRequest
    {
        public int RequestID { get; set; }
        public int PetID { get; set; }
        public int AdopterID { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public string PetName { get; set; }
        public string AdopterFirstName { get; set; }
        public string AdopterPhone { get; set; }
    }
}