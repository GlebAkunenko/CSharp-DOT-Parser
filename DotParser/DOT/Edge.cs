using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotParser.DOT;

public struct Edge : IEquatable<Edge>
{
    public Node Left { init; get; }
    public Node Right { init; get; }
    public Attribute[] Attributes { init; get; }

    public Edge(Node left, Node right, Attribute[] attributes = null)
    {
        Left = left;
        Right = right;
        Attributes = attributes;
    }

    public Edge(string firstNode, string secondNode, Attribute[] edgeAttributes = null)
    {
        Left = new Node(firstNode);
        Right = new Node(secondNode);
        Attributes = edgeAttributes;
    }

    public override bool Equals(object? obj)
    {
        return obj is Edge edge && Equals(edge);
    }

    public bool Equals(Edge other)
    {
        return Left.Equals(other.Left) &&
               Right.Equals(other.Right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Left, Right);
    }

    public static bool operator ==(Edge left, Edge right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Edge left, Edge right)
    {
        return !(left == right);
    }
}