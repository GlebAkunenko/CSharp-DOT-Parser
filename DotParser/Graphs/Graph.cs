namespace DotParser.Graphs;

public class Graph<N, E> 
    where N : Node, new()
    where E : Edge, new()
{
    private List<N> _nodes;
    private List<E> _edges;

    public Graph(RawGraph source)
    {
        _nodes = new List<N>();
        foreach (DOT.Node node in source.Nodes) {
            N add_node = new N();
            add_node.Init(node);
            _nodes.Add(add_node);
        }
        _edges = new List<E>();
        foreach (DOT.Edge edge in source.Edges) {
            E add_edge = new E();
            add_edge.Init(edge);
            _edges.Add(add_edge);
        }
    }

    public Dictionary<N, List<N>> GetAdjacencyList()
    {
        var result = new Dictionary<N, List<N>>();
        foreach (N node in _nodes)
            result[node] = new List<N>();
        foreach(E edge in _edges) {
            N? from = _nodes.Find((node) => node.Source == edge.Source.Left);
            N? to = _nodes.Find((node) => node.Source == edge.Source.Right);
            if (from is null || to is null) throw new Exception();
            result[from].Add(to);
        }
        return result;
    }

}