using DotParser.LexicalAnalysis;

namespace DotParser.SyntaxAnalysis;

public class LexemesEditor
{
    public void Edit(List<Lexeme> lexemes)
    {
        for(int i = 0; i < lexemes.Count - 1; i++) {
            if (lexemes[i] is Word && lexemes[i + 1] is Word)
                lexemes.Insert(i + 1, new AlienSeparator());
        }
    }
}
