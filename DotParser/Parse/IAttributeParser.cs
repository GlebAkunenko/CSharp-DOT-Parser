using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse;

public interface IAttributeParser
{
    Attribute[] FromString(string s);

    bool HasAttributes(string str);
}
