namespace CSLox
{
	class Program
	{
        static bool hadError;

        static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				RunLoxREPL();
			}
			else if (args.Length == 1)
			{
				RunLoxScript(args[0]);
			}
			else
			{
				Console.WriteLine("Usage: cslox [script]");
                System.Environment.Exit(64);
            }
		}

		private static void RunLoxScript(string path)
		{	
			byte[] bytes = File.ReadAllBytes(path);
			Run(System.Text.Encoding.UTF8.GetString(bytes));

            if (hadError) System.Environment.Exit(65);
        }

		private static void RunLoxREPL()
		{
			while (true) {
				Console.Write("> ");
				string? input = Console.ReadLine();
				
				if (input == null || input.Equals("exit")) break;
				if (input.Equals("")) continue;

				Run(input);
                hadError = false;
            }
		}

		private static void Run(string input) {
            List<string> tokens = [.. input.Split(' ')];

			foreach (var token in tokens)
			{
                Console.WriteLine(token);
            }
        }

		private static void Error(int line, string message) {
            Report(line, "", message);
        }

		private static void Report(int line, string where, string message) {
			// TODO: Better error messages
            Console.WriteLine($"[line {line}] Error {where}: {message}");
            hadError = true;
        }
	}
}