using System;
using DotParser.Parse.Dictionary;
using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse;

public class AttributesParser : IAttributeParser
{
    private int Count(string subject, char obj)
    {
        int result = 0;
        foreach (char c in subject)
            result += (obj == c) ? 1 : 0;
        return result;
    }

    public Attribute[] FromString(string s)
    {
        s = s.Trim().Trim('[', ']');

        DictionaryParser dictionary = new DictionaryParser();

        Dictionary<string, string> pairs = dictionary.ParseFromString(s);
        Attribute[] result = new Attribute[pairs.Count];
        int i = 0;
        foreach (var pair in pairs)
            result[i++] = new Attribute(pair.Key, pair.Value);
        return result;
    }

    public bool HasAttributes(string str)
    {
        int openBrackets = Count(str, '[');
        int closeBrackets = Count(str, ']');
        if (openBrackets != closeBrackets || openBrackets > 2)
            throw new DotSyntaxException("Brackets error");
        return openBrackets == 1;
    }
}
