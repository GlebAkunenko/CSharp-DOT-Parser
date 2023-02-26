using System;
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

    public bool HasAttributes(string str)
    {
        int openBrackets = Count(str, '[');
        int closeBrackets = Count(str, ']');
        if (openBrackets != closeBrackets || openBrackets > 2)
            throw new DotSyntaxException("Brackets error");
        return openBrackets == 1;
    }

    public Attribute[] FromString(string s)
    {
        s = s.Trim().Trim('[', ']');
        string[] input = s.Split(',');
        Attribute[] output = new Attribute[input.Length];

        for(int i = 0; i < input.Length; i++) {
            string[] name_value = input[i].Split('=');
            if (name_value.Length != 2)
                throw new DotSyntaxException("Wrong attribute format");
            output[i] = new Attribute(
                name:  name_value[0].Trim(),
                value: name_value[1].Trim()
                );
        }

        return output;
    }
}
