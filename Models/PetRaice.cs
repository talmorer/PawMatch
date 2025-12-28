using System;

namespace Models
{
    public class PetRaice
    {
        public PetRaice() { }

        public PetRaice(int petRaiceID, string description, int? petTypeID)
        {
            PetRaiceID = petRaiceID;
            Description = description;
            PetTypeID = petTypeID;
        }

        public int PetRaiceID { get; set; }
        public string Description { get; set; }
        public int? PetTypeID { get; set; }
    }
}











































