namespace Calc {
    class CalcInt {

        public static void Main(string[] args) {

            var interpreter = new Interpreter();
            Console.WriteLine("Calc Interpreter");
            string command;
            do {
                Console.Write(">");
                command = Console.ReadLine();
                if (!string.IsNullOrEmpty(command)) {
                    string output = interpreter.Exec(command);
                    Console.WriteLine(output);
                }
            } while (!string.IsNullOrEmpty(command));
        }
    }
}
