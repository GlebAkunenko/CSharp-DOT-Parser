namespace DotParser.LexicalAnalysis;

public class Bracket : Lexeme
{
    private static readonly char[] s_openChars = new char[] { '[', '{' };
    private static readonly char[] s_closeChars = new char[] { ']', '}' };
    private static readonly char[] s_attributes = new char[] { '[', ']' };

    private static IEnumerable<char> suppportedChars => s_openChars.Concat(s_closeChars);

    public bool IsOpen { get; init; }
    public bool IsClose => !IsOpen;
    public bool IsAttribute { get; init; }

    private Bracket(char bracket) : base("")
    {
        if (!suppportedChars.Contains(bracket))
            throw new ArgumentException($"{bracket} is an unsupported char");
        Value = bracket.ToString();
        IsOpen = s_openChars.Contains(bracket);
        IsAttribute = s_attributes.Contains(bracket);
    }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code)
        {
            if (code.Length != 1)
                return false;
            return suppportedChars.Contains(code[0]);
        }

        public override Lexeme GetLexeme(string code)
        {
            return new Bracket(code[0]);
        }
    }
}
