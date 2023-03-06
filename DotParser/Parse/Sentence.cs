using DotParser.Graph;

namespace DotParser.Parse;

public abstract class Sentence
{
    protected string Line { init; get; }

    public Sentence(string line)
    {
        Line = line;
    }

    public abstract void Apply(IGraph graph);
}


