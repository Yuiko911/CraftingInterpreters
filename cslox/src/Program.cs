namespace CSLox
{
	class Program
	{
		static int Main(string[] args)
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
				return 64;
			}

			return 0;
		}

		static void RunLoxScript(string path)
		{	
			byte[] bytes = File.ReadAllBytes(path);
			Run(System.Text.Encoding.UTF8.GetString(bytes));
		}

		static void RunLoxREPL()
		{
			while (true) {
				Console.Write("> ");
				string? input = Console.ReadLine();
				
				if (input == null || input.Equals("")) continue;
				if (input.Equals("exit")) break;

				Run(input);
			}
		}

		static void Run(string input) {
			Console.WriteLine(input);
		}
	}
}