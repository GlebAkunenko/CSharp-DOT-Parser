namespace DotParser.SyntaxAnalysis;

public class StructBlock : Block
{
    public List<Block> Body { get; init; }

    protected StructBlock(List<Block> body)
    {
        Body = body;
    }
}
