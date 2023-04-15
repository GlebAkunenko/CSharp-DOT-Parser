namespace DotParser.LexicalAnalysis;

public class WhiteSpace : Lexeme
{
    private WhiteSpace() : base("") { }

	public class Factory : LexemeFactory
	{
        public override bool CanParse(string code)
        {
            if (code.Length != 1)
                return false;
            return char.IsWhiteSpace(code[0]);
        }

        public override Lexeme GetLexeme(string code)
        {
            return new WhiteSpace();
        }
    }
}
