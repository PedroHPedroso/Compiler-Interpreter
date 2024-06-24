namespace Calc {
    public class SymbolTable {
        private List<Entry> _symbols;

        public SymbolTable() {
            _symbols = new List<Entry>();
        }

        public int AddSymbol(string varName) {
            int index = _symbols.FindIndex(e => e.VarName == varName);
            if (index == -1) {
                _symbols.Add(new Entry { VarName = varName, Value = null });
                index = _symbols.Count - 1;
            }
            return index;
        }

        public Entry GetSymbol(int index) {
            if (index >= 0 && index < _symbols.Count) {
                return _symbols[index];
            }
            throw new IndexOutOfRangeException("Symbol not found.");
        }

        public void UpdateSymbol(int index, Entry entry) {
            if (index >= 0 && index < _symbols.Count) {
                _symbols[index] = entry;
            } else {
                throw new IndexOutOfRangeException("Symbol not found.");
            }
        }
    }

    public struct Entry {
        public string VarName;
        public double? Value;
    }
}
