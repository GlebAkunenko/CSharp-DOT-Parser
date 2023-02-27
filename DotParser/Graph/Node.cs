using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotParser.Graph;

public class Node
{
    public string Name { get; init; }
    public List<Node> Children { get; init; }

    public Node(string name)
    {
        Name = name;
        Children = new List<Node>();
    }

    public Node(DOT.Node node)
    {
        Name = node.Name;
    }
}
