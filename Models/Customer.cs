namespace Models
{
    public class Customer
    {
        public int UserID { get; set; }
        public int IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }
        public Customer() { }
        public Customer(int userId, int isAdmin,string firstName,string lastName,string email,int phone,string password)
        {
            UserID = userId;
            IsAdmin = isAdmin;
            FirstName = firstName;
            LastName=lastName;
            Email = email;
            Phone = phone;
            Password = password;
        }
    }

}