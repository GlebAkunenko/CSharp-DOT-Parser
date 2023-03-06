namespace DotParser.Parse.Keywords;

public class WrondKeywordException : Exception
{
    public WrondKeywordException() { }

    public WrondKeywordException(string message) : base(message) { }
}