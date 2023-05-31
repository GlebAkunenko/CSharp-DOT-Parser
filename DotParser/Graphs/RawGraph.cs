using DotParser.DOT;

namespace DotParser.Graphs;

public class RawGraph
{
    public List<DOT.Edge> Edges { get; set; }
    public List<DOT.Node> Nodes { get; set; }

    public RawGraph()
    {
        Nodes = new List<DOT.Node>();
        Edges = new List<DOT.Edge>();
    }
}
