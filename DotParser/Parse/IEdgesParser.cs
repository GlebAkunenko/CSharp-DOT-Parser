using DotParser.DOT;

namespace DotParser.Parse;

public interface IEdgesParser
{
    Edge[] ParseFromString(string input);
}
