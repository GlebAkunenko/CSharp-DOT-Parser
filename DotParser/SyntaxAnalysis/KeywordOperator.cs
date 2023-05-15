using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class KeywordOperator : Operator
{
    public Keyword Keyword { get; init; }
    public List<Tuple<Word, Word>> Attributes { get; init; }

    public KeywordOperator(Element baseElement) : base(baseElement)
    {
        Word word = (Word)First.Lexeme; 
        if (word.Keyword == Keyword.Edge || word.Keyword == Keyword.Node)
            Keyword = word.Keyword;
        else
            throw new Exception();
        Attributes = ((AttrubuteOperator)Second).GetAttributes();
    }

    public class Factory : OperatorFactory
    {
        public override bool IsRelevant(Element element)
        {
            if (element.Left is null || element.Right is null)
                return false;
            if (element.Left.Lexeme is Word word)
                return
                    element.Lexeme is AlienSeparator &&
                    (word.Keyword == Keyword.Edge || word.Keyword == Keyword.Node) &&
                    element.Right is AttrubuteOperator;
            return false;
        }

        public override Operator CreateOperator(Element baseElement)
        {
            return new KeywordOperator(baseElement);
        }
    }
}