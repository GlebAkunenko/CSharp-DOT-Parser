using DotParser.Graph;

namespace DotParser.Parse.Keywords;

public class NodeKeyword : Keyword
{
    protected override bool CheckCodeBlock(string code)
    {
        return code.StartsWith("node");
    }

    protected override void WithdrawKeyword(string input, out string output, out string keywordAndParams)
    {
        throw new NotImplementedException();
    }

    protected override void ApplyKeyword(IGraph graph, string keyword)
    {
        throw new NotImplementedException();
    }
}