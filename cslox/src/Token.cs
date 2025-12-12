class Token(TokenType type, string lexeme, object? literal, int line)
{
	readonly public TokenType type = type;
	readonly public string lexeme = lexeme;
	readonly public object? literal = literal;
	readonly public int line = line;

	override public string ToString()
	{
		return $"{this.type} {this.lexeme} {this.literal} ({this.line})";
	}
}