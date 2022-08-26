namespace MyProject2.Models
{
    public class User2
    {
        public int PersonID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public User2(int personid, string lastname, string firstname, string address, string city)
        { 
            PersonID = personid;
            LastName = lastname;
            FirstName = firstname;
            Address = address;
            City = city;
        }

        public User2()
        {

        }
    }
}
