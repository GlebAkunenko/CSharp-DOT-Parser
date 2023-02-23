using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotParser.DOT;

public class Edge
{
    public Edge(Node first, Node second, Attribute[] attributes = null)
    {
        First = first;
        Second = second;
        Attributes = attributes;
    }

    public Node First { init; get; }
    public Node Second { init; get; }
    public Attribute[] Attributes { init; get; }
}