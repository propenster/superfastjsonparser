using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJSONParser
{
    public class Parser
    {

        public Lexer _Lexer { get; set; }
        private Token _CurrentToken;
        private Token _NextToken;

        public Parser(Lexer lexer)
        {
            _Lexer = lexer;
            _CurrentToken = new Token(TokenKind.EOF, "\0");
            _NextToken = new Token(TokenKind.EOF, "\0");

            Next();
        }
        /// <summary>
        /// advance to the next token
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void Next()
        {
            _CurrentToken = _NextToken;
            _NextToken = _Lexer.Lex();
        }

        public object Parse()
        {
            Next(); // {"akkskd"
            switch (_CurrentToken.kind)
            {
                case TokenKind.StringLiteral:
                case TokenKind.NumericLiteral:
                case TokenKind.BooleanLiteral:
                    return _CurrentToken.literal;

                case TokenKind.OpenCurlyBrace: //{
                    return MakeDictionary();

                case TokenKind.OpenSquareBrace: //[ 
                    return MakeList();

                default:
                    return (object)string.Empty;

            }
        }

        private object MakeList()
        {
            var outputList = new List<object>(); //]
            while (_CurrentToken.kind != TokenKind.CloseSquareBrace)
            {
                outputList.Add(Parse());
                Next();
            }
            return outputList;
        }

        private object MakeDictionary()
        {
            var outputDict = new Dictionary<object, object>();
            while (_CurrentToken.kind != TokenKind.CloseCurlyBrace)
            {
                var key = Parse(); //get key  "key"s
                if (_NextToken.kind != TokenKind.Colon) throw new Exception($"Expected a colon, found {_NextToken.literal}");

                Next();
                var value = Parse();
                outputDict.Add(key, value);
                Next();

            }

            return outputDict;
        }
    }
}
