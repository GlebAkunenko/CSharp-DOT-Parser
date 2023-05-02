using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class GraphBlock : StructBlock
{
    private bool _isDigraph;

    private GraphBlock(bool isDigraph, List<Block> body) : base(body)
    {
        _isDigraph = isDigraph;
    }

    private void MakeGraph()
    {
        throw new NotImplementedException();
    }

    public class Buider : BlockBuilder
    {
        private readonly Factory[] _factories = new Factory[] {

        };

        private int _bracketCount = 0;
        private bool _isDigraph;
        private BlockBuilder? _current;
        private List<Block> _blocks;

        private Buider()
        {
            _blocks = new List<Block>();
        }

        protected override void OnFirstElementAdded(Element element)
        {
            if (element.Lexeme is Word keyword) {
                if (keyword.Keyword == Keyword.Digraph)
                    _isDigraph = true;
                else if (keyword.Keyword == Keyword.Graph)
                    _isDigraph = false;
                else
                    throw new DotSyntaxException();
            }
        }

        protected override bool IsRelevant(Element element) => true;

        protected override bool IsFinishElement(Element element) => false;

        protected override void OnElementAdded(Element element)
        {
            if (element.Lexeme is Bracket bracket) {
                if (!bracket.IsAttribute) {
                    _bracketCount += bracket.IsOpen ? 1 : 0;
                    if (_bracketCount == 0)
                        IsComplited = true;
                }
            }
            if (_current is null) {
                foreach(Factory factory in _factories) {
                    if (factory.IsBeginElement(element)) {
                        _current = factory.GetBuilder();
                        _current.AddElement(element);
                    }
                }
            }
            else {
                _current.AddElement(element);
                if (_current.IsComplited)
                    _blocks.Add(_current.GetResult());
                _current = null;
            }
        }

        public override Block GetResult()
        {
            var result = new GraphBlock(_isDigraph, _blocks);
            result.MakeGraph();
            return result;
        }
    }
}
