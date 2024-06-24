namespace Calc {
    public class Lexer {
        public static char EOF = '\0';

        public string Input { get; set; }
        private int _ptr;
        public SymbolTable Table { get; private set; }

        public Lexer(string input, SymbolTable table) {
            Input = input;
            _ptr = 0;
            Table = table;
        }

        private char Scan() {
            if (_ptr == Input.Length)
                return EOF;
            return Input[_ptr++];
        }

        private int ParseInt(char c) {
            return c switch {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                _ => -1,
            };
        }

        public Token NextToken() {
            char c = Scan();
            while (c == ' ' || c == '\t') {
                c = Scan();
            }
            switch (c) {
                case '+': return new Token { Type = TokenType.SUM };
                case '-': return new Token { Type = TokenType.SUB };
                case '=': return new Token { Type = TokenType.EQ };
                case '(': return new Token { Type = TokenType.OPEN };
                case ')': return new Token { Type = TokenType.CLOSE };
                case '\0': return new Token { Type = TokenType.EOF };
                default:
                    if (char.IsLetter(c)) {
                        var varName = c.ToString();
                        int index = Table.AddSymbol(varName);
                        return new Token { Type = TokenType.VAR, Attribute = index };
                    } else if (char.IsDigit(c)) {
                        int value = ParseInt(c);
                        return new Token { Type = TokenType.NUM, Attribute = value };
                    } else {
                        return new Token { Type = TokenType.UNK };
                    }
            }
        }
    }

    public class Token {
        public TokenType Type { get; set; }
        public int Attribute { get; set; }
    }

    public enum TokenType {
        VAR, NUM, EQ, SUM, SUB, OPEN, CLOSE, PRINT, EOF, UNK
    }
}
