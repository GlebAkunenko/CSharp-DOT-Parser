using DotParser.SyntaxAnalysis;

namespace DotParser.SemanticAnalysis;

public class BlockCreater
{
    private readonly BlockFactory[] _factories = new BlockFactory[] {
        new NodesBlock.Factory(),
        new EdgeBlock.Factory(),
        new GraphBlock.Factory()
    };

    public List<Block> CreateBlocks(List<Element> code)
    {
        var result = new List<Block>();
        foreach(Element element in code) {
            foreach(BlockFactory factory in _factories) {
                if (factory.IsRelevant(element))
                    result.Add(factory.GetBlock(element));
            }
        }
        return result;
    }
}

