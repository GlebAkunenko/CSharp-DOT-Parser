namespace DotParser.LexicalAnalysis;

public class Word : Lexeme
{
    public Keyword Keyword { private set; get; }
    public bool IsKeyword => Keyword != Keyword.Incorrect;

    private void CheckKeyword()
    {
        if (Enum.TryParse(typeof(Keyword), Value, true, out object result))
            Keyword = (Keyword)result;
        else
            Keyword = Keyword.Incorrect;
    }

    public Word(string word = "") : base(word)
    {
        foreach(char c in word) {
            if (!char.IsLetterOrDigit(c))
                throw new ArgumentException($"char {c} is unsupported");
        }
        CheckKeyword();
    }

    public void AddSymbol(char symbol)
    {
        if (!char.IsLetterOrDigit(symbol))
            throw new ArgumentException($"char {symbol} is unsupported");
        Value += symbol;
        CheckKeyword();
    }

    public void AddSymbol(Symbol symbol) => AddSymbol(symbol.Value);
}

public enum Keyword
{
    Incorrect,
    Digraph,
    Graph,
    Node,
    Edge
}