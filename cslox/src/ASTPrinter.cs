class ASTPrinter : Expr.IVisitor<string>
{
	public string Print(Expr expr)
	{
		return expr.Accept(this);
	}

	public string VisitBinaryExpr(Expr.Binary expr)
	{
		return this.Parenthesize(expr.op.lexeme, expr.left, expr.right);
	}

	public string VisitGroupingExpr(Expr.Grouping expr)
	{
		return this.Parenthesize("group", expr.expression);
	}

	public string VisitLiteralExpr(Expr.Literal expr)
	{
		if (expr.value is null) return "null";
		string? s = expr.value.ToString(); 
		return s is null ? "null" : s;
	}

	public string VisitUnaryExpr(Expr.Unary expr)
	{
		return this.Parenthesize(expr.op.lexeme, expr.right);
	}

	private string Parenthesize(string name, params Expr[] exprs) {
		string s = "";
		s += '(';
		s += name;

		foreach (var expr in exprs)
		{
			s += " " + expr.Accept(this);
		}

		s += ')';
		return s;
	}

}