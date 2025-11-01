class Program {
	static void Main() {
		Console.WriteLine("Hello...");

		int i = 1;
		do
		{
			Console.WriteLine(i);
			i *= 2;
		} while (i < 100);

		Console.WriteLine("...World !");
	}
}