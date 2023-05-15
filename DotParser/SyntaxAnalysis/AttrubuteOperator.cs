using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class AttrubuteOperator : Operator
{
    public AttrubuteOperator(Element baseElement) : base(baseElement)
    {
    }

    private Tuple<Word, Word> GetAttributes(EqualOperator equal)
    {
        return new Tuple<Word, Word>((Word)equal.First.Lexeme, (Word)equal.Second.Lexeme);
    }

    private List<Tuple<Word, Word>> GetAttributes(EqualSeparator equal)
    {
        var result = new List<Tuple<Word, Word>>();

        if (equal.First is EqualSeparator leftSeparator)
            result.AddRange(GetAttributes(leftSeparator));
        else if (equal.First is EqualOperator leftOperator)
            result.Add(GetAttributes(leftOperator));
        else
            throw new Exception();

        if (equal.Second is EqualSeparator rightSeparator)
            result.AddRange(GetAttributes(rightSeparator));
        else if (equal.Second is EqualOperator rightOperator)
            result.Add(GetAttributes(rightOperator));
        else
            throw new Exception();

        return result;
    }

    public List<Tuple<Word, Word>> GetAttributes()
    {
        if (BaseElement is EqualOperator equal)
            return new List<Tuple<Word, Word>>() { GetAttributes(equal) };
        return GetAttributes((EqualSeparator)BaseElement);
    }

    public class Factory : OperatorFactory
    {
        public override bool AllowOperatorAsBase => true;

        private bool CheckBracket(Element target, string bracketType)
        {
            if (target.Lexeme is Bracket bracket)
                return bracket.Value == bracketType;
            return false;
        }

        public override bool IsRelevant(Element element)
        {
            if (element.Left is null || element.Right is null)
                return false;
            return
                element is EqualOperator or EqualSeparator &&
                CheckBracket(element.Left, "[") &&
                CheckBracket(element.Right, "]");
        }

        public override Operator CreateOperator(Element baseElement)
        {
            return new AttrubuteOperator(baseElement);
        }
    }
}
