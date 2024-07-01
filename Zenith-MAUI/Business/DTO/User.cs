using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith_MAUI.Business.DTO
{
    public class User : TokenDTO
    {
        public string Username { get; set; }
        public int Id { get; set; }
    }
}
