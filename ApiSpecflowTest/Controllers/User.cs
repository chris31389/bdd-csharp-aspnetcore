using System;

namespace ApiSpecflowTest.Controllers
{
    public class User
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        protected User()
        {
        }

        public User(string name)
        {
            Name = name;
        }
    }
}