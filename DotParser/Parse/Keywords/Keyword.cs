using DotParser.Graph;

namespace DotParser.Parse.Keywords;

public abstract class Keyword
{
    protected abstract bool CheckCodeBlock(string code);

    protected abstract void WithdrawKeyword(string input, out string output, out string keywordAndParams);

    /// <param name="keyword">This is a code with only keyword syntax and parameters</param>
    protected abstract void ApplyKeyword(IGraph graph, string keyword);

    /// <param name="graph"></param>
    /// <param name="code"></param>
    /// <returns>Code without keyword</returns>
    /// <exception cref="WrondKeywordException"></exception>
    public string Apply(IGraph graph, string code)
    { 
        if (!CheckCodeBlock(code))
            throw new WrondKeywordException();

        WithdrawKeyword(code, out string result, out string keyword);
        ApplyKeyword(graph, keyword);
        return result;
    }

    public bool IsMatch(string word) => CheckCodeBlock(word.Trim().ToLower());
}
