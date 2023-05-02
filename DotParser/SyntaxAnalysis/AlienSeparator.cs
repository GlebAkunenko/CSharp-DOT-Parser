using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class AlienSeparator : Lexeme
{
    public AlienSeparator() : base(",")
    {
    }
}