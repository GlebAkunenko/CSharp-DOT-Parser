using DotParser.Graph;
using System.Data;

namespace DotParser.Parse.Keywords;

public class GraphKeywords : Keyword
{
    protected override bool CheckCodeBlock(string code)
    {
        return code.StartsWith("graph") || code.StartsWith("digraph");
    }

    private string WithdrawBracketsString(string source)
    {
        string result = "";
        int openBracketCount = 0;

        foreach(char c in source) {
            if (c == '{') 
                openBracketCount++;
            if (c == '}') {
                openBracketCount--;
                if (openBracketCount == 0)
                    return result;
            }
            result += c;
        }
        throw new DotSyntaxException("Brackets error");
    }

    protected override void WithdrawKeyword(string input, out string output, out string keywordAndParams)
    {
        string formInput = input.Trim().ToLower();

        string keyword = "";
        if (formInput.StartsWith("graph"))
            keyword = "graph";
        else if (formInput.StartsWith("digraph"))
            keyword = "digraph";
        else
            throw new WrondKeywordException();

        string parameter = formInput.Substring(keyword.Length).Trim();
        keywordAndParams = keyword + " " + WithdrawBracketsString(parameter);
        output = input.Remove(0, parameter.Length);
    }

    protected override void ApplyKeyword(IGraph graph, string keyword)
    {
        throw new NotImplementedException();
    }
}
