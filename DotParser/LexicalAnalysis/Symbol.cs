namespace DotParser.LexicalAnalysis;

public class Symbol : Lexeme
{
    private Symbol(char symbol) : base(symbol.ToString()) { Value = symbol; }

    public new char Value { init; get; }

    public class Factory : LexemeFactory
    {
        public override bool CanParse(string code)
        {
            if (code.Length != 1)
                return false;
            return char.IsLetterOrDigit(code[0]);
        }

        public override Lexeme GetLexeme(string code)
        {
            return new Symbol(code[0]);
        }
    }
}
