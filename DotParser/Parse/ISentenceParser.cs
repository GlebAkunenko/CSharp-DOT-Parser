using DotParser.DOT;

namespace DotParser.Parse;

public interface ISentenceParser
{
    Sentence[] FromString(string line);
}