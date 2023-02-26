using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotParser.DOT;

public struct Edge
{
    public Node First { init; get; }
    public Node Second { init; get; }
    public Attribute[] Attributes { init; get; }

    public Edge(Node first, Node second, Attribute[] attributes = null)
    {
        First = first;
        Second = second;
        Attributes = attributes;
    }

    public Edge(string firstNode, string secondNode, Attribute[] edgeAttributes = null)
    {
        First = new Node(firstNode);
        Second = new Node(secondNode);
        Attributes = edgeAttributes;
    }
}