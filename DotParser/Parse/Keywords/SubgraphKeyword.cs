using DotParser.Graph;

namespace DotParser.Parse.Keywords;

public class SubgraphKeyword : Keyword
{
    protected override bool CheckCodeBlock(string code)
    {
        return code.StartsWith("subgraph");
    }

    protected override void ApplyKeyword(IGraph graph, string keyword)
    {
        throw new NotImplementedException();
    }

    protected override void WithdrawKeyword(string input, out string output, out string keywordAndParams)
    {
        throw new NotImplementedException();
    }
}