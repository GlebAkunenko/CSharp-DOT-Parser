namespace DotParser.Graphs;

public class EdgeFactory
{
    public virtual Edge GetEdge(DOT.Edge source)
    {
        return new Edge(source);
    }
}
