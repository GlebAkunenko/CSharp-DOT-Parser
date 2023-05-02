using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class AttributeBlock : Block
{
    private Element _body;

    public List<Tuple<Word, Word>> Attributes { get; init; }

    public AttributeBlock(Element body)
    {
        _body = body;
        Attributes = new List<Tuple<Word, Word>>();
    }

    private void AddAttributes(Element element)
    {
        if (element is EqualOperator equal)
            Attributes.Add(new Tuple<Word, Word>((Word)equal.First.Lexeme, (Word)equal.Second.Lexeme));
        else if (element is EqualSeparator separator) {
            AddAttributes(separator.First);
            AddAttributes(separator.Second);
        }
        else
            throw new DotSyntaxException();
    }

    public void MakeAttributes()
    {
        AddAttributes(_body);
    }

    public class Builder : BlockBuilder
    {
        protected override bool IsRelevant(Element element)
        {
            if (element is EqualOperator or EqualSeparator)
                return true;
            if (element.Lexeme is Bracket bracket)
                return bracket.IsAttribute;
            return false;
        }

        protected override bool IsFinishElement(Element element)
        {
            if (element.Lexeme is Bracket close)
                return close.Value == "]";
            return false;
        }

        public override Block GetResult()
        {
            if (elements.Count != 3)
                throw new DotSyntaxException();
            var result = new AttributeBlock(elements[1]);
            result.MakeAttributes();
            return result;
        }

        public class Factory : BlockBuilder.Factory
        {
            public override bool IsBeginElement(Element element)
            {
                if (element.Lexeme is Bracket open)
                    return open.Value == "[";
                return false;
            }

            public override BlockBuilder GetBuilder()
            {
                return new AttributeBlock.Builder();
            }
        }
    }
}
