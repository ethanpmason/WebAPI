using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.userInfo
{
    public class publicInfoDTO
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string service { get; set; }
        public string Description { get; set; }
    }
}
