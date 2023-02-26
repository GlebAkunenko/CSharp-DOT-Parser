using System;
using Attribute = DotParser.DOT.Attribute;

namespace DotParser.Parse;

public class AttributesParser : IAttributeParser
{
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
