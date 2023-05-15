using System.Net.Http.Headers;
using System.Xml;
using System.Xml.Linq;

namespace DotParser.SyntaxAnalysis;

public class OperatorCreater
{
    private readonly OperatorFactory[] _factories = new OperatorFactory[] {
        new EqualOperator.Factory(),
        new EqualSeparator.Factory(),
        new AttrubuteOperator.Factory(),
        new KeywordOperator.Factory(),
        new CommaOperator.Factory(),
        new EdgeOperator.Factory(),
    };

    public List<Element> Elements { get; init; }

    public OperatorCreater(List<Element> elements)
    {
        Elements = elements;
    }

    private void CreateOperators(OperatorFactory factory)
    {
        for (int i = 0; i < Elements.Count; i++) {
            Element current = Elements[i];
            if (current is Operator && !factory.AllowOperatorAsBase)
                continue;
            if (factory.IsRelevant(current)) {
                Operator op = factory.CreateOperator(current);
                AddOperator(op);
                i -= 1;
            }
        }
    }

    private void AddOperator(Operator op)
    {
        int index = Elements.IndexOf(op.BaseElement) - 1;
        Elements.RemoveAt(index); // left
        Elements.RemoveAt(index); // target
        Elements.RemoveAt(index); // right

        Elements.Insert(index, op);
    }

    //private void RemoveElement(Element e)
    //{
    //    if (e.Left != null)
    //        e.Left.Right = e.Right;
    //    if (e.Right != null)
    //        e.Right.Left = e.Left;
    //    Elements.Remove(e);
    //}

    //private void AddElement(Element e, int index)
    //{
    //    Elements.Insert(index, e);
    //    if (index > 0) {
    //        Elements[index - 1].Right = e;
    //        Elements[index].Left = Elements[index - 1];
    //    }
    //    if (index < Elements.Count - 1) {
    //        Elements[index + 1].Left = e;
    //        Elements[index].Right = Elements[index + 1];
    //    }
    //}

    public void MakeOperators()
    {
        foreach (OperatorFactory factory in _factories)
            CreateOperators(factory);
    }
}