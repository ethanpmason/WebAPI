using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Feedback
{
    public class feedBackDTO
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string Email { get; set; }
        public string comments { get; set; }
    }
}
