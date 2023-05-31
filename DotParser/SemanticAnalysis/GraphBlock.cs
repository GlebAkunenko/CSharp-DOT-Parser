using DotParser.Graphs;
using DotParser.LexicalAnalysis;
using DotParser.SyntaxAnalysis;

namespace DotParser.SemanticAnalysis;

public class GraphBlock : Block
{
    public GraphBlock(Sort type)
    {
        Type = type;
    }

    public Sort Type { get; init; }
    public bool IsDigraph => Type == Sort.Digraph;
    public bool IsNotIdgraph => Type == Sort.Graph;

    public enum Sort { Graph, Digraph }

    public override void ApplyToGraph(RawGraph graph)
    {
        throw new NotImplementedException();
    }

    public class Factory : BlockFactory
    {
        public override bool IsRelevant(Element element)
        {
            if (element.Lexeme is Word w) {
                if (w.Keyword == Keyword.Graph || w.Keyword == Keyword.Digraph) {
                    if (element.Right != null && element.Right.Right != null)
                        return element.Right.Lexeme is AlienSeparator && element.Right.Lexeme is Word;
                }
            }
            return false;
        }

        public override Block GetBlock(Element element)
        {
            Keyword keyword = ((Word)element.Lexeme).Keyword;
            Sort sort = keyword switch {
                Keyword.Graph => Sort.Graph,
                Keyword.Digraph => Sort.Digraph,
                _ => throw new Exception()
            };
            return new GraphBlock(sort);
        }
    }
}