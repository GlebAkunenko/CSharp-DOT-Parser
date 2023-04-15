namespace DotParser.LexicalAnalysis;

public static class LexemeCreater
{
    private static readonly LexemeFactory[] s_facories = new LexemeFactory[] {
        new WhiteSpace.Factory(),
        new Edge.Factory(),
        new Comma.Factory(),
        new Semicolon.Factory(),
        new Equals.Factory(),
        new Bracket.Factory(),
        new Symbol.Factory()
    };

    public static bool TryGetLexeme(string code, out Lexeme lexeme)
    {
        foreach(LexemeFactory factory in s_facories) {
            if (factory.CanParse(code)) {
                lexeme = factory.GetLexeme(code);
                return true;
            }
        }
        lexeme = null;
        return false;
    }
}