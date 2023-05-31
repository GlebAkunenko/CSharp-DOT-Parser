using DotParser.Graphs;

namespace DotParser.SemanticAnalysis;

public abstract class Block
{
    public abstract void ApplyToGraph(RawGraph graph);
}
