class Token(TokenType type, string lexeme, object? literal, int line)
{
	readonly private TokenType type = type;
	readonly private string lexeme = lexeme;
	readonly private object? literal = literal;
	readonly private int line = line;

	override public string ToString()
	{
		return $"{this.type} {this.lexeme} {this.literal} ({this.line})";
	}
}