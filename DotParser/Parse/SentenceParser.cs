using DotParser.DOT;

namespace DotParser.Parse;

public class SentenceParser : ISentenceParser
{
    private IAttributeParser _attributeParser;
    private IEdgesParser _edgesParser;

    public SentenceParser(IAttributeParser attributeParser, IEdgesParser edgesParser)
    {
        _attributeParser = attributeParser;
        _edgesParser = edgesParser;
    }

    public Sentence[] FromString(string line)
    {
        throw new NotImplementedException();
    }

}
