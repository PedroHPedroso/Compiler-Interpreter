namespace Calc {
    public class Interpreter {
        private SymbolTable _table;

        public Interpreter() {
            _table = new SymbolTable();
        }

        public string Exec(string? command) {
            if (string.IsNullOrWhiteSpace(command)) return "";

            var lexer = new Lexer(command, _table);
            var parser = new Parser(lexer);
            return parser.Cmd();
        }
    }
    }