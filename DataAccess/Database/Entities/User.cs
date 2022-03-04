using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        [JsonConstructor]
        public User(int userid, string name, string address, string contact)
        {
            this.UserId = userid;
            this.Name = name;
            this.Address = address;
            this.Contact = contact;
        }
    }
}
