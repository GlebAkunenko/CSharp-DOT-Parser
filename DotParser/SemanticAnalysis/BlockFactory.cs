using DotParser.SyntaxAnalysis;

namespace DotParser.SemanticAnalysis;

public abstract class BlockFactory
{
    public abstract bool IsRelevant(Element element);

    public abstract Block GetBlock(Element element);
}