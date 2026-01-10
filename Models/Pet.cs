using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pet
    {

        public Pet()
        {

        }

        public Pet(int petID, string petName, string petAdress, string petPicture, int petType, int petBirthYear, int updateUserID, int isActive)
        {
            PetID = petID;
            PetName = petName;
            PetAdress = petAdress;
            PetPicture = petPicture;
            PetType = petType;
            PetBirthYear = petBirthYear;
            UpdateUserID = updateUserID;
            IsActive = isActive;
        }

        public int PetID { get; set; }
        public string PetName { get; set; }
        public string PetAdress { get; set; }
        public string PetPicture { get; set; }
        public int PetType { get; set; }
        public int PetBirthYear { get; set; }
        public int UpdateUserID { get; set; }
        public int IsActive { get; set; }
        public int PetRaceID { get; set; }
        public string TypeName { get; set; }
        public string RaceName { get; set; }
        public string UploaderFirstName { get; set; }
        public string UploaderLastName { get; set; }
        public string UploaderEmail { get; set; }
        public string UploaderPhone { get; set; }


    }
}
