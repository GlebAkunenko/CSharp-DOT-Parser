using System.Globalization;

namespace DotParser.LexicalAnalysis;

public static class LexemeScanner
{
    private static List<Lexeme> GetRawLexemes(string code)
    {
        string current = "";
        List<Lexeme> result = new List<Lexeme>();

        Word inQuotes = null;

        for (int i = 0; i < code.Length; i++) { 

            if (inQuotes is not null) {
                if (new Quote.Factory().CanParse(code[i].ToString())) {
                    result.Add(inQuotes);
                    inQuotes = null;
                }
                else
                    inQuotes.AddSymbol(code[i]);
                continue;
            }

            if (new Quote.Factory().CanParse(code[i].ToString())) {
                inQuotes = new Word();
                continue;
            }

            current += code[i];
            if (LexemeCreater.TryGetLexeme(current, out Lexeme lexeme)) {
                current = "";
                result.Add(lexeme);
            }
        }

        return result;
    }

    public static Lexeme[] ScanLexemes(string code)
    {
        List<Lexeme> input = GetRawLexemes(code);
        List<Lexeme> output = new List<Lexeme>(); ;

        for (int i = 0; i < input.Count; i++)
        {
            if (i > 0){
                if (input[i] is WhiteSpace && input[i - 1] is WhiteSpace)
                    continue;
            }

            if (input[i] is Symbol)
            {
                Word word = new Word();
                int j = i;
                for (; j < input.Count; j++)
                {
                    if (input[j] is not Symbol)
                        break;
                    word.AddSymbol((Symbol)input[j]);
                }
                output.Add(word);
                i = j - 1;
            }
            else
                output.Add(input[i]);
        }

        return output.ToArray();
    }

    public static Lexeme[] RemoveUselessWhiteSpaces(Lexeme[] lexemes)
    {
        List<Lexeme> result = new List<Lexeme>();
        bool removing = true;
        foreach (Lexeme lexeme in lexemes) {
            if (lexeme is Bracket bracket) {
                if (bracket.IsAttribute)
                    removing = bracket.IsOpen;
            }
            if (removing) {
                if (lexeme is not WhiteSpace)
                    result.Add(lexeme);
            }
        }
        return result.ToArray();
    }
}