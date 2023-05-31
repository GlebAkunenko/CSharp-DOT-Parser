namespace DotParser.Graphs;

public class Graph 
{
    private List<Node> _nodes;
    private List<Edge> _edges;

    public Graph(List<Node> nodes, List<Edge> edges)
    {
        _nodes = nodes;
        _edges = edges;
    }

    public Dictionary<Node, List<Node>> GetAdjacencyList()
    {
        var result = new Dictionary<Node, List<Node>>();
        foreach (Node node in _nodes)
            result[node] = new List<Node>();
        foreach(Edge edge in _edges) {
            Node? from = _nodes.Find((node) => node.Source == edge.Source.Left);
            Node? to = _nodes.Find((node) => node.Source == edge.Source.Right);
            if (from is null || to is null) throw new Exception();
            result[from].Add(to);
        }
        return result;
    }

}
