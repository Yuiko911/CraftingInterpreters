class Utils
{
	static public bool IsCharDigit(char c)
	{
		return c >= '0' && c <= '9';
	}

	static public bool IsCharAlpha(char c) {
		// TODO: Support unicode
		return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == '_');
	}

	static public bool IsCharAlphanumeric(char c) {
		return IsCharAlpha(c) || IsCharDigit(c);
	}
}