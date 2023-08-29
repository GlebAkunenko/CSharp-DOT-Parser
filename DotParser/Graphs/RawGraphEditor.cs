using DotParser.DOT;
using System.Net.Http.Headers;

namespace DotParser.Graphs;

using Attribute = DotParser.DOT.Attribute;

public class RawGraphEditor
{
    public RawGraphEditor(RawGraph graph)
    {
        Graph = graph;
    }

    public RawGraph Graph { get; init; }

    private void AddAttrsToNode(List<DOT.Node> nodes, DOT.Node obj, Attribute[] additional)
    {
        DOT.Node actual = nodes.Find((x) => x == obj);
        nodes.Remove(actual);
        Attribute[] actualAttrs = actual.Attributes;
        actualAttrs ??= Array.Empty<Attribute>();
        List<Attribute> newAttrs = new List<Attribute>();
        newAttrs.AddRange(actualAttrs);
        newAttrs.AddRange(additional);
        DOT.Node newNode = new DOT.Node(actual.Name, newAttrs.ToArray());
        nodes.Add(newNode);
    }

    public void AddEdgeAttrsToRightNode()
    {
        List<DOT.Node> nodes = Graph.Nodes;
        foreach(DOT.Edge edge in Graph.Edges) {
            DOT.Node add = edge.Right;
            DOT.Attribute[] attrs = edge.Attributes;
            if (attrs != null && attrs.Length > 0)
                AddAttrsToNode(nodes, add, attrs);
        }
        Graph.Nodes = nodes;
    }
}