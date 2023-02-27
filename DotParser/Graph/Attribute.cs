namespace DotParser.Graph;

public class Attribute
{
    public string Name { get; protected set; }
    public string Value { get; protected set; }

    public Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
}
