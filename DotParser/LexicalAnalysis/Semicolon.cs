namespace DotParser.LexicalAnalysis;

public class Semicolon : Lexeme
{
    public Semicolon() : base(";") { }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code)
        {
            if (code.Length != 1)
                return false;
            return code[0] == ';';
        }

        public override Lexeme GetLexeme(string code)
        {
            return new Semicolon();
        }
    }
}