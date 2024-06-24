namespace Calc {
    public class Parser {
        private Lexer _lexer;
        private Token _lookahead;

        public Parser(Lexer lexer) {
            _lexer = lexer;            
            _lookahead = _lexer.NextToken();
        }

        public void Match(TokenType token) {
            if (_lookahead.Type == token) {
                _lookahead = _lexer.NextToken();
            } else {
                throw new Exception($"Syntax error: expected {token} but found {_lookahead.Type}");
            }
        }

        public string Cmd() {
        if (_lookahead == null) {
            throw new Exception("Unexpected end of input");
        }

        if (_lookahead.Type == TokenType.PRINT) {
            Match(TokenType.PRINT);
            var value = Expr();
            return value.ToString();
        } else if (_lookahead.Type == TokenType.VAR) {
            Atrib();
            return string.Empty;
        } else {
            throw new Exception($"Syntax error: unexpected command {_lookahead.Type}");
        }
        }

        public void Atrib() {
            int index = _lookahead.Attribute;
            Match(TokenType.VAR); // Match VAR
            Match(TokenType.EQ);  // Match '='
            double value = Expr();
            var entry = _lexer.Table.GetSymbol(index);
            entry.Value = value;
            _lexer.Table.UpdateSymbol(index, entry);
        }

        public double Expr() {
            double termValue = Term();
            return Rest(termValue);
        }

        public double Rest(double left) {
            if (_lookahead.Type == TokenType.SUM) {
                Match(TokenType.SUM);
                double right = Expr();
                return left + right;
            } else if (_lookahead.Type == TokenType.SUB) {
                Match(TokenType.SUB);
                double right = Expr();
                return left - right;
            } else {
                return left;
            }
        }

        public double Term() {
            if (_lookahead.Type == TokenType.NUM) {
                double value = _lookahead.Attribute;
                Match(TokenType.NUM);
                return value;
            } else if (_lookahead.Type == TokenType.VAR) {
                int index = _lookahead.Attribute;
                var entry = _lexer.Table.GetSymbol(index);
                Match(TokenType.VAR);
                return entry.Value ?? throw new Exception($"Variable {entry.VarName} is not initialized.");
            } else if (_lookahead.Type == TokenType.OPEN) {
                Match(TokenType.OPEN);
                double value = Expr();
                Match(TokenType.CLOSE);
                return value;
            } else {
                throw new Exception($"Syntax error: unexpected token {_lookahead.Type}");
            }
        }
    }
}
