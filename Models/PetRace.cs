using System;

namespace Models
{
    public class PetRace
    {
        public PetRace() { }

        public PetRace(int petRaceID, string description, int? petTypeID)
        {
            PetRaceID = petRaceID;
            Description = description;
            PetTypeID = petTypeID;
        }

        public int PetRaceID { get; set; }
        public string Description { get; set; }
        public int? PetTypeID { get; set; }
    }
}











































