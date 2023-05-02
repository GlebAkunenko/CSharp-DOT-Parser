using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class EqualOperator : Operator
{
    public EqualOperator(Element baseElement) : base(baseElement)
    {
    }

    public class Factory : OperatorFactory
    {
        public override bool IsRelevant(Element element)
        {
            if (element.Left is null || element.Right is null)
                return false;
            return
                element.Lexeme is Equal &&
                element.Left.Lexeme is Word &&
                element.Right.Lexeme is Word;
        }

        public override Operator CreateOperator(Element baseElement)
        {
            return new EqualOperator(baseElement);
        }
    }
}
