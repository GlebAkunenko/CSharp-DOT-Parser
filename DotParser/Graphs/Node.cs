namespace DotParser.Graphs;

public class Node
{
    public DOT.Node Source { get; protected set; }

    public Node(DOT.Node source) => Source = source;

    public override bool Equals(object? obj)
    {
        return obj is Node node &&
               Source.Equals(node.Source);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Source);
    }
}
