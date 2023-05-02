namespace DotParser.SyntaxAnalysis;

public abstract class OperatorFactory
{
    public abstract bool IsRelevant(Element element);

    public abstract Operator CreateOperator(Element baseElement);
}
