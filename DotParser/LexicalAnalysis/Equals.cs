namespace DotParser.LexicalAnalysis;

public class Equals : Lexeme
{
    private Equals() : base("=") { }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code) => code == "=";

        public override Lexeme GetLexeme(string code) => new Equals();
    }
}