using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse;

public interface IAttributeParser
{
    bool HasAttributes(string str);

    Attribute[] FromString(string str);
}
