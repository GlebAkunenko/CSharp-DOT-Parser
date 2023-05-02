using DotParser.LexicalAnalysis;
using System.Text.Json.Serialization;

namespace DotParser.SyntaxAnalysis;

public class EqualSeparator : Operator
{
    private EqualSeparator(Element baseElement) : base(baseElement)
    {
    }

    public class Factory : OperatorFactory
    {
        public override bool IsRelevant(Element element)
        {
            if (element.Left is null || element.Right is null)
                return false;
            return
                element.Lexeme is Comma or Semicolon or AlienSeparator &&
                element.Left is EqualOperator or EqualSeparator &&
                element.Right is EqualOperator;
        }

        public override Operator CreateOperator(Element baseElement)
        {
            return new EqualSeparator(baseElement);
        }
    }
}
