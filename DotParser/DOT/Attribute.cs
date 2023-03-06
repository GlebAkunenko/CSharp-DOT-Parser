namespace DotParser.DOT;

public struct Attribute
{
    public string Name { init; get; }
    public string Value { init; get; }

    public Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
}
