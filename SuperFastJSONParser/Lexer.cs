using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperFastJSONParser
{
    public class Lexer
    {
        public Lexer(string jsonString)
        {
            _JsonString = jsonString ?? throw new ArgumentNullException(nameof(jsonString));

            _position = -2;
            _currentChar = '\0';
            _nextChar = '\0';


            Next(); //to move so we start our lexer on a clean safe state...
        }

        private void Next()
        {
            _position++;
            _currentChar = _nextChar; //move on to the next char... 

            if(_position <= (_JsonString.Length - 2))
            {
                _nextChar = _JsonString[_position + 1];
            }
            else
            {
                _nextChar = '\0';
            }
        }

        private void ConsumeWhiteSpaces()
        {
            while(_currentChar != '\0' && char.IsWhiteSpace(_currentChar))
            {
                Next();
            }
        }

        public string _JsonString { get; }

        private int _position;
        private char _currentChar;
        private char _nextChar;


        public Token Lex()
        {
            Next();
            ConsumeWhiteSpaces();
            var currentChar = _currentChar;
            var charString = currentChar.ToString();

            if(currentChar == '\0')
            {
                return new Token(TokenKind.EOF, charString);
            }else if(currentChar == ':')
            {
                return new Token(TokenKind.Colon, charString);
            }else if(currentChar == '{')
            {
                return new Token(TokenKind.OpenCurlyBrace, charString);
            }else if(currentChar == '}')
            {
                return new Token(TokenKind.CloseCurlyBrace, charString);
            }else if(currentChar == '[')
            {
                return new Token(TokenKind.OpenSquareBrace, charString);
            }else if(currentChar == ']')
            {
                return new Token(TokenKind.CloseSquareBrace, charString);
            }
            else if (currentChar == ',')
            {
                return new Token(TokenKind.Comma, charString);
            }
            else if(currentChar == 't' || currentChar == 'f')
            {
                return MakeBooleanLiteral();
            }
            else if(currentChar == '"')
            {
                return MakeStringLiteral();
            }
            else if (char.IsDigit(currentChar))
            {
                return MakeNumericLiteral();
            }


            return new Token(TokenKind.EOF, '\0');
        }

        private Token MakeBooleanLiteral()
        {
            var currentPos = _position;
            while(_currentChar != '\0' && _currentChar != 'e') //true false end with 'e'
            {
                Next();
            }
            var boolValue = _JsonString.Substring(currentPos, _position - currentPos + 1);
            return new Token(TokenKind.BooleanLiteral, bool.Parse(boolValue.ToString()));
        }

        private Token MakeNumericLiteral()
        {
            var currentPos = _position;
            var dots = 0;
            while(_currentChar != '\0' && (char.IsDigit(_nextChar) || _nextChar == '.'))
            {
                if (_currentChar == '.') dots++;

                Next();
               
            }

            if (dots > 1) throw new FormatException("Invalid decimal number format"); //23.23.23
            if (dots == 0) return new Token(TokenKind.NumericLiteral, int.Parse(_JsonString.Substring(currentPos, _position - currentPos + 1)));
            return new Token(TokenKind.NumericLiteral, double.Parse(_JsonString.Substring(currentPos, _position - currentPos + 1)));
        }

        private Token MakeStringLiteral()
        {
            // " ... shift to the next char..
            Next();
            var currentPos = _position;

            while(_currentChar != '\0' && _currentChar != '"')
            {
                Next();
            }

            var literal = _JsonString.Substring(currentPos, _position - currentPos);
            return new Token(TokenKind.StringLiteral, literal);

        }
    }
}
