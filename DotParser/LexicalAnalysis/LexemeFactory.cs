namespace DotParser.LexicalAnalysis;

public abstract class LexemeFactory
{
    public abstract bool CanParse(string code);

    public abstract Lexeme GetLexeme(string code);
}
