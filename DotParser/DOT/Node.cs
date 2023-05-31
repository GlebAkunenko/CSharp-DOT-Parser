using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace DotParser.DOT;

public struct Node : IEquatable<Node>
{
    public Node(string name, Attribute[] attributes = null)
    {
        Name = name;
        Attributes = attributes;
    }

    public string Name { init; get; }
    public Attribute[] Attributes { init; get; }

    public override bool Equals(object? obj)
    {
        return obj is Node node && Equals(node);
    }

    public bool Equals(Node other)
    {
        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }

    public static bool operator ==(Node left, Node right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Node left, Node right)
    {
        return !(left == right);
    }
}