namespace DotParser.DOT;

public class Node
{
    public Node(string name, Attribute[] attributes = null)
    {
        Name = name;
        Attributes = attributes;
    }

    public string Name { init; get; }
    public Attribute[] Attributes { init; get; }
}
