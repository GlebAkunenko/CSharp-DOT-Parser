using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class EdgeBlock : Block
{
    private EdgeOperator _edge;

    public List<Tuple<Word, Word>> Edges { get; init; }

    public EdgeBlock(EdgeOperator edge)
    {
        Edges = edge.GetEdges();
    }

    public class Builder : BlockBuilder
    {
        protected override bool IsRelevant(Element element) => element is EdgeOperator;

        protected override bool IsFinishElement(Element element) => true;

        public override Block GetResult()
        {
            return new EdgeBlock((EdgeOperator)elements[0]);
        }

        public class Factory : BlockBuilder.Factory
        {
            public override bool IsBeginElement(Element element) => element is EdgeOperator;

            public override BlockBuilder GetBuilder() => new EdgeBlock.Builder();
        }
    }
}