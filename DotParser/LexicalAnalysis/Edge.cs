namespace DotParser.LexicalAnalysis;

public class Edge : Lexeme
{
    private static readonly string[] s_supported = new[] { "--", "->" };

    public bool IsArrow { get; init; }

    private Edge(string edge) : base("")
    {
        if (!s_supported.Contains(edge))
            throw new ArgumentException($"{edge} is not supported");
        Value = edge;
        IsArrow = edge == "->";
    }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code)
        {
            return s_supported.Contains(code);
        }

        public override Lexeme GetLexeme(string code)
        {
            return new Edge(code);
        }
    }
}