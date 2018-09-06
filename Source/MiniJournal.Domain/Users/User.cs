using System;

namespace Infotecs.MiniJournal.Domain.Users
{
    public class User
    {
        public User(string name)
        {
            this.Name = name;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}
