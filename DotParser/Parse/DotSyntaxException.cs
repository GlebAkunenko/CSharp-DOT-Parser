namespace DotParser.Parse;

public class DotSyntaxException : Exception
{
    public DotSyntaxException() { }

    public DotSyntaxException(string message) : base(message) { }
}
