using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJSONParser
{
    public enum TokenKind
    {
        OpenCurlyBrace,
        CloseCurlyBrace,

        Comma,
        Colon,

        OpenSquareBrace,
        CloseSquareBrace,

        StringLiteral,
        NumericLiteral,

        BooleanLiteral,

        EOF,
    }
}
