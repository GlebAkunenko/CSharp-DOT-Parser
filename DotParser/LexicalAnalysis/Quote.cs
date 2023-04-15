using System.Runtime.InteropServices;

namespace DotParser.LexicalAnalysis;

public class Quote : Lexeme
{
    public Quote() : base("\"") { }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code) => code == "\"" || code == "'";

        public override Lexeme GetLexeme(string code) => new Quote();
    }
}
