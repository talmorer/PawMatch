using System;

namespace Models
{
    public class PetType
    {
        public PetType() { }

        public PetType(int typeID, string description)
        {
            TypeID = typeID;
            Description = description;
        }

        public int TypeID { get; set; }
        public string Description { get; set; }
    }
}
