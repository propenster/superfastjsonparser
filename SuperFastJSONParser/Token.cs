using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJSONParser
{
    public record Token(TokenKind kind, object literal);
    
}
