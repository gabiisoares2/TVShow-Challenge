using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShow.Domain.Core
{
    public class Token
    {
        public Token(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}
