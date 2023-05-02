namespace DotParser.LexicalAnalysis;

public class Equal : Lexeme
{
    private Equal() : base("=") { }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code) => code == "=";

        public override Lexeme GetLexeme(string code) => new Equal();
    }
}