namespace Models
{
    public class Customer
    {
        public int UserID { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Customer() { }
        public Customer(int userId, bool isAdmin,string firstName,string lastName,string email,string phone,string password)
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