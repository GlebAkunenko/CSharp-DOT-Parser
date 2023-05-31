namespace DotParser.Graphs;

public class Graph<N, E>
    where N: Node
    where E: Edge
{
    private List<Node> _nodes;
    private List<Edge> _edges;

    public Graph(List<Node> nodes, List<Edge> edges)
    {
        _nodes = nodes;
        _edges = edges;
    }

    public Dictionary<N, List<N>> GetAdjacencyList()
    {
        var result = new Dictionary<N, List<N>>();
        foreach (Node node in _nodes)
            result[(N)node] = new List<N>();
        foreach(Edge edge in _edges) {
            Node? from = _nodes.Find((node) => node.Source == edge.Source.Left);
            Node? to = _nodes.Find((node) => node.Source == edge.Source.Right);
            if (from is null || to is null) throw new Exception();
            result[(N)from].Add((N)to);
        }
        return result;
    }

}
