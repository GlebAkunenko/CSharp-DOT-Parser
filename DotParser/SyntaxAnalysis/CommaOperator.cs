using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class CommaOperator : Operator
{
    public CommaOperator(Element baseElement) : base(baseElement)
    {
    }

    public class Factory : OperatorFactory
    {
        public override bool IsRelevant(Element element)
        {
            if (element.Left is null || element.Right is null)
                return false;
            return
                element.Lexeme is Comma &&
                (element.Left.Lexeme is Word || element.Left is CommaOperator) &&
                element.Right.Lexeme is Word;
        }

        public override Operator CreateOperator(Element baseElement)
        {
            return new CommaOperator(baseElement);
        }
    }
    
}
