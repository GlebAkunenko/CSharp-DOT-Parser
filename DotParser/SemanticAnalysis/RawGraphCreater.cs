using DotParser.Graphs;
using DotParser.SyntaxAnalysis;

namespace DotParser.SemanticAnalysis;

public class RawGraphCreater
{
    public RawGraph Create(List<Block> blocks)
    {
        if (blocks.Count == 0)
            throw new GraphCreatingException();

        RawGraph result = new RawGraph();
        var enumerator = blocks.AsEnumerable();
        if (blocks[0] is GraphBlock)
            enumerator = enumerator.Skip(1);

        foreach(Block block in enumerator) 
            block.ApplyToGraph(result);
        return result;
    }
}
