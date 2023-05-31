using DotParser.DOT;
using DotParser.Graphs;
using DotParser.SyntaxAnalysis;
using Attribute = DotParser.DOT.Attribute;
using Edge = DotParser.DOT.Edge;
using Node = DotParser.DOT.Node;

namespace DotParser.SemanticAnalysis;

public class EdgeBlock : Block
{
    private EdgeOperator _edge;
    private AttrubuteOperator? _attrubutes;

    public EdgeBlock(EdgeOperator edge, AttrubuteOperator? attrubutes = null)
    {
        _edge = edge;
        _attrubutes = attrubutes;
    }

    private bool hasAttributes() => _attrubutes is not null;

    private Attribute[] GetAttributes()
    {
        if (_attrubutes == null)
            return Array.Empty<Attribute>();
        List<Attribute> result = new List<Attribute>();
        foreach (var attribute in _attrubutes.GetAttributes())
            result.Add(new Attribute(attribute.Item1.Value, attribute.Item2.Value));
        return result.ToArray();
    }

    public Edge[] GetEdges()
    {
        List<Edge> result = new List<Edge>();
        foreach(var edge in _edge.GetEdges()) {
            Attribute[]? attributes = null;
            if (hasAttributes())
                attributes = GetAttributes();
            result.Add(new Edge(edge.Item1.Value, edge.Item2.Value, attributes));
        }
        return result.ToArray();
    }

    public override void ApplyToGraph(RawGraph graph)
    {
        Edge[] edges = GetEdges();
        foreach(Edge edge in edges) {
            Node u = edge.Left, v = edge.Right;
            if (!graph.Nodes.Contains(u))
                graph.Nodes.Add(u);
            if (!graph.Nodes.Contains(v))
                graph.Nodes.Add(v);
        }
        graph.Edges.AddRange(edges);
    }

    public class Factory : BlockFactory
    {
        public override bool IsRelevant(Element element) => element is EdgeOperator;

        public override Block GetBlock(Element element)
        {
            if (element.Right is AttrubuteOperator attrubutes)
                return new EdgeBlock((EdgeOperator)element, attrubutes);

            return new EdgeBlock((EdgeOperator)element);
        }
    }
}