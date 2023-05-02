using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class ElementsCreater
{
    public List<Element> Create(List<Lexeme> code)
    {
        var editor = new LexemesEditor();
        editor.Edit(code);
        var result = new List<Element>(code.Count);
        for (int i = 0; i < code.Count; i++)
            result.Add(new Element(i > 0 ? result[i - 1] : null, code[i]));
        return result;
    }
}
