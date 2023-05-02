using DotParser.LexicalAnalysis;
using System.Collections.Generic;

namespace DotParser.SyntaxAnalysis;

public class EdgeOperator : Operator
{
    private bool _isArrow;
    private List<Word> _from;
    private List<Word> _to;

    private List<Word> from
    {
        get
        {
            if (_from == null) {
                _from = new List<Word>();
                FillBranch(First, _from);
            }
            return _from;
        }
    }

    private List<Word> to
    {
        get
        {
            if (_to == null) {
                _to = new List<Word>();
                FillBranch(Second, _to);
            }
            return _to;
        }
    }

    private EdgeOperator(Element baseElement) : base(baseElement)
    {
        _isArrow = ((Edge)baseElement.Lexeme).IsArrow;
    }

    /// <summary>
    /// This method goes down from the root and addes to container every word that has deal with the root
    /// </summary>
    private void FillBranch(Element root, List<Word> container)
    {
        if (root.Lexeme is Word word)
            container.Add(word);
        else if (root is CommaOperator comma) {
            FillBranch(comma.First, container);
            FillBranch(comma.Second, container);
        }
        else
            throw new Exception();
    }

    public List<Word> GetNodes(Element side)
    {
        if (side.Lexeme is Word word)
            return new List<Word>() { word };
        if (side is CommaOperator comma) {
            List<Word> result = new List<Word>();
            result.AddRange(GetNodes(comma.First));
            result.AddRange(GetNodes(comma.Second));
        }
        throw new DotSyntaxException();
    }

    public List<Tuple<Word, Word>> GetEdges()
    {
        var result = new List<Tuple<Word, Word>>();
        foreach(Word u in from) {
            foreach (Word v in to) {
                result.Add(new Tuple<Word, Word>(u, v));
                if (_isArrow)
                    result.Add(new Tuple<Word, Word>(v, u));
            }
        }
        return result;
    }

    public class Factory : OperatorFactory
    {
        public override bool IsRelevant(Element element)
        {
            if (element.Left is null || element.Right is null)
                return false;
            return
                (element.Left is CommaOperator || element.Left.Lexeme is Word) &&
                (element.Right is CommaOperator || element.Right.Lexeme is Word) &&
                element.Lexeme is LexicalAnalysis.Edge;
        }

        public override Operator CreateOperator(Element baseElement)
        {
            return new EdgeOperator(baseElement);
        }
    }
}