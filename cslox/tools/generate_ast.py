#!/usr/bin/python3
import sys


def defineAST(output_dir: str, base_name: str, types: list[tuple[str, str]]) -> None:
	assert output_dir != ""
	
	file_path = f"{output_dir}/{base_name}.cs"
	with open(file_path, 'w+') as file:
		file.write(f"abstract class {base_name}\n")
		file.write("{\n")

		file.write("\tpublic abstract T Accept<T>(IVisitor<T> visitor);\n")
		file.write("\n")

		defineVisitor(file, base_name, types)

		for _type in types:
			class_name = _type[0].strip()
			fields = _type[1].strip()
			defineType(file, base_name, class_name, fields)
		
		file.write("}\n")

def defineVisitor(file: 'TextIOWrapper', base_name: str, types: str) -> None:
	file.write("\tpublic interface IVisitor<T>\n")
	file.write("\t{\n")

	for _type in types:
		class_name = _type[0].strip()
		file.write(f"\t\tT Visit{class_name}{base_name} ({class_name} {base_name.lower()});\n")
	
	file.write("\t}\n\n")
	...

def defineType(file: 'TextIOWrapper', base_name: str, class_name: str, fields: str) -> None:
	file.write(f"\tpublic class {class_name}({fields}) : {base_name}\n")
	file.write("\t{\n")

	for field in fields.split(','):
		field = field.strip()
		field_type, field_name = field.split(' ')

		file.write(f"\t\tpublic readonly {field_type} {field_name} = {field_name};\n")

	file.write("\n")
	file.write("\t\tpublic override T Accept<T>(IVisitor<T> visitor)\n")
	file.write("\t\t{\n")
	file.write(f"\t\t\treturn visitor.Visit{class_name}{base_name}(this);\n")
	file.write("\t\t}\n")

	file.write("\t}\n\n")
	file

def main() -> None:
	if len(sys.argv) < 2:
		print("Usage: ./generate_ast.py <output directory>", file=sys.stderr)
		sys.exit(64)
		
	output_dir: str = sys.argv[1]
	defineAST(output_dir, "Expr", [
		("Binary", "Expr left, Token op, Expr right"),
		("Grouping", "Expr expression"),
		("Literal", "object? value"),
      	("Unary", "Token op, Expr right")
	])
	sys.exit(0)

if __name__=="__main__":
	main()