namespace DotParser.Graphs;

public class GraphBuilder
{
    private NodeFactory _nodeFactory;
    private EdgeFactory _edgeFactory;

    public GraphBuilder() : this(new NodeFactory(), new EdgeFactory()) { }

    public GraphBuilder(NodeFactory nodeFactory) : this(nodeFactory, new EdgeFactory()) { }

    public GraphBuilder(EdgeFactory edgeFactory) : this(new NodeFactory(), edgeFactory) { }

    public GraphBuilder(NodeFactory nodeFactory, EdgeFactory edgeFactory)
    {
        _nodeFactory = nodeFactory;
        _edgeFactory = edgeFactory;
    }

    public Graph BuildGraph(RawGraph rawGraph)
    {
        List<Node> nodes = new List<Node>();
        List<Edge> edges = new List<Edge>();
        foreach (DOT.Node node in rawGraph.Nodes)
            nodes.Add(_nodeFactory.GetNode(node));
        foreach (DOT.Edge edge in rawGraph.Edges)
            edges.Add(_edgeFactory.GetEdge(edge));
        return new Graph(nodes, edges);
    }
}