using Infotecs.MiniJournal.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.UsersApplicationService
{
    public class GetUserByNameResponse
    {
        public User User { get; set; }
    }
}
