namespace DotParser.Graphs;

public class Edge
{
    public DOT.Edge Source { get; protected set; }

    public Edge(DOT.Edge source) => Source = source;

    public override bool Equals(object? obj)
    {
        return obj is Edge edge &&
               Source.Equals(edge.Source);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Source);
    }
}
