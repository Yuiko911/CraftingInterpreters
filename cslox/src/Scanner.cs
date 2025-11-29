class Scanner(string Source)
{
	private static readonly Dictionary<string, TokenType> keywords = new() {
		{"and",    TokenType.AND},
		{"class",  TokenType.CLASS},
		{"else",   TokenType.ELSE},
		{"false",  TokenType.FALSE},
		{"for",    TokenType.FOR},
		{"fun",    TokenType.FUN},
		{"if",     TokenType.IF},
		{"nil",    TokenType.NIL},
		{"or",     TokenType.OR},
		{"print",  TokenType.PRINT},
		{"return", TokenType.RETURN},
		{"super",  TokenType.SUPER},
		{"this",   TokenType.THIS},
		{"true",   TokenType.TRUE},
		{"var",    TokenType.VAR},
		{"while",  TokenType.WHILE},
	};

	private readonly string source = Source;
	private List<Token> tokens = [];

	private int start = 0;
	private int current = 0;
	private int line = 1;

	public List<Token> ScanTokens()
	{
		while (!IsAtEnd())
		{
			char c = Advance();

			switch (c)
			{
				// Single character
				case '(': AddToken(TokenType.LEFT_PAREN); break;
				case ')': AddToken(TokenType.RIGHT_PAREN); break;
				case '{': AddToken(TokenType.LEFT_BRACE); break;
				case '}': AddToken(TokenType.RIGHT_BRACE); break;
				case ',': AddToken(TokenType.COMMA); break;
				case '.': AddToken(TokenType.DOT); break;
				case ';': AddToken(TokenType.SEMICOLON); break;
				case '-': AddToken(TokenType.MINUS); break;
				case '+': AddToken(TokenType.PLUS); break;
				case '*': AddToken(TokenType.STAR); break;
				case '/':
					if (DoesMatch('/'))
						while (Peek() != '\n' && !IsAtEnd()) Advance(); // Check for comments
					else if (DoesMatch('*'))
						ReadMultilineComment(); // Check for comments
					else
						AddToken(TokenType.SLASH);
					break;

				// Operators
				case '!': AddToken(DoesMatch('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
				case '=': AddToken(DoesMatch('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
				case '>': AddToken(DoesMatch('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
				case '<': AddToken(DoesMatch('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;

				// Strings
				case '"': ReadString(); break;

				// Whitespaces
				case ' ':
				case '\r':
				case '\t': break;
				case '\n': this.line++; break;

				default:
					// Numbers
					if (Utils.IsCharDigit(c))
					{
						ReadNumber();
					}
					else if (Utils.IsCharAlpha(c))
					{
						ReadIdentifier();
					}
					else
					{
						CSLox.Error(line, $"Unknown token '{c}'");
					}

					break;
			}

			this.start = this.current;
		}

		AddToken(TokenType.EOF);

		foreach (Token token in this.tokens)
			Console.WriteLine(token);

		return this.tokens;
	}

	private bool IsAtEnd()
	{
		return this.current >= this.source.Length;
	}

	private char Advance()
	{
		return this.source.ElementAt(this.current++);
	}

	private char Peek()
	{
		if (IsAtEnd()) return '\0';
		return this.source.ElementAt(current);
	}

	private char PeekNext()
	{
		if (this.current + 1 >= this.source.Length) return '\0';
		return this.source.ElementAt(current + 1);
	}

	private bool DoesMatch(char match)
	{
		if (IsAtEnd()) return false;
		if (Peek() != match) return false;

		this.current++;
		return true;
	}

	private void ReadString()
	{
		while (!Peek().Equals('"') && !IsAtEnd())
		{
			if (Peek() == '\n') this.line++;
			Advance();
		}

		if (IsAtEnd())
		{
			CSLox.Error(this.line, "Unterminated string");
			return;
		}

		Advance(); // Consume the closing "

		string text = this.source[(this.start + 1)..(this.current - 1)]; // Trim the "
		this.AddToken(TokenType.STRING, text);
	}

	private void ReadNumber()
	{
		while (Utils.IsCharDigit(Peek())) Advance();

		if (Peek().Equals('.') && Utils.IsCharDigit(PeekNext()))
		{
			Advance(); // Consume the .
			while (Utils.IsCharDigit(Peek())) Advance();
		}

		AddToken(TokenType.NUMBER, Double.Parse(this.source[this.start..this.current]));
	}

	private void ReadIdentifier()
	{
		while (Utils.IsCharAlphanumeric(Peek())) Advance();

		string text = this.source[this.start..this.current];
		if (!keywords.TryGetValue(text, out TokenType type)) type = TokenType.IDENTIFIER;

		AddToken(type);
	}

	private void ReadMultilineComment()
	{
		while (!(Peek().Equals('*') && PeekNext().Equals('/')) && !IsAtEnd())
		{
			if (Peek() == '\n') this.line++;
			Advance();
		}

		if (!IsAtEnd())
		{
			Advance();
			Advance();
		}
	}

	private void AddToken(TokenType type)
	{
		AddToken(type, null);
	}

	private void AddToken(TokenType type, object? obj)
	{
		string text = this.source[this.start..this.current];
		this.tokens.Add(new Token(type, text, obj, this.line));
	}

}
