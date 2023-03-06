using DotParser.Graph;

namespace DotParser.Parse.Keywords;

public class EdgeKeyword : Keyword
{
    private IAttributeParser _attributeParser;

    public EdgeKeyword(IAttributeParser attributeParser)
    {
        _attributeParser = attributeParser;
    }

    protected override bool CheckCodeBlock(string code)
    {
        return code.StartsWith("edge");
    }

    protected override void WithdrawKeyword(string input, out string output, out string keywordAndParams)
    {
        input = input.Trim();
        throw new NotImplementedException();
    }

    protected override void ApplyKeyword(IGraph graph, string keyword)
    {
        throw new NotImplementedException();
    }
}