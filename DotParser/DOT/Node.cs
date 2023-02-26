using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace DotParser.DOT;

public struct Node
{
    public Node(string name, Attribute[] attributes = null)
    {
        Name = name;
        Attributes = attributes;
    }

    public string Name { init; get; }
    public Attribute[] Attributes { init; get; }
}
