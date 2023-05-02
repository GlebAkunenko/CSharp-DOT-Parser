using DotParser.LexicalAnalysis;
using System.Reflection.Metadata;

namespace DotParser.SyntaxAnalysis;

public abstract class Operator : Element
{
    public Element BaseElement { init; get; }
    public Element First { get; init; }
    public Element Second { get; init; }

    protected Operator(Element baseElement) : base(baseElement.Left, baseElement.Lexeme)
    {
        if (baseElement.Left is null || baseElement.Right is null)
            throw new Exception();
        BaseElement = baseElement;
        First = BaseElement.Left;
        Second = BaseElement.Right;
        Left = BaseElement.Left.Left;
        Right = BaseElement.Right.Right;
        if (Left != null)
            Left.Right = this;
        if (Right != null)
            Right.Left = this;
    }
}
