namespace DotParser.Parse;

public class SentenceParser
{
    private IAttributeParser _attributeParser;
    private IEdgesParser _edgesParser;

    public SentenceParser(IAttributeParser attributeParser, IEdgesParser edgesParser)
    {
        _attributeParser = attributeParser;
        _edgesParser = edgesParser;
    }

}
