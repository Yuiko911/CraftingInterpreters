abstract class Expr
{
	public abstract T Accept<T>(IVisitor<T> visitor);

	public interface IVisitor<T>
	{
		T VisitBinaryExpr (Binary expr);
		T VisitGroupingExpr (Grouping expr);
		T VisitLiteralExpr (Literal expr);
		T VisitUnaryExpr (Unary expr);
	}

	public class Binary(Expr left, Token op, Expr right) : Expr
	{
		public readonly Expr left = left;
		public readonly Token op = op;
		public readonly Expr right = right;

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.VisitBinaryExpr(this);
		}
	}

	public class Grouping(Expr expression) : Expr
	{
		public readonly Expr expression = expression;

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.VisitGroupingExpr(this);
		}
	}

	public class Literal(object? value) : Expr
	{
		public readonly object? value = value;

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.VisitLiteralExpr(this);
		}
	}

	public class Unary(Token op, Expr right) : Expr
	{
		public readonly Token op = op;
		public readonly Expr right = right;

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.VisitUnaryExpr(this);
		}
	}

}
