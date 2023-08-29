using DotParser.Graphs;
using System.Net.Http.Headers;

namespace Exambles.SimpleMathExample;

public class MyNode : Node
{
    public int Value { get; init; }

    public MyNode(DotParser.DOT.Node source) : base(source)
    {
        Value = int.Parse(source.Name);
    }
}

public class MyNodeFactory : NodeFactory
{
    public override Node GetNode(DotParser.DOT.Node source)
    {
        return new MyNode(source);
    }
}

public class Runner : ExampleRunner
{
    public override void Run(string graphCode)
    {
        RawGraph rawGraph = RawGraphParser.Parse(graphCode);

        var graphBuilder = new GraphBuilder<MyNode, Edge>(new MyNodeFactory());

        Graph<MyNode, Edge> graph = graphBuilder.BuildGraph(rawGraph);

        Dictionary<MyNode, List<MyNode>> adjacencyList = graph.GetAdjacencyList();

        foreach (MyNode node in adjacencyList.Keys)
        {
            Console.Write(node.Value.ToString() + ": ");
            int sum = 0;
            foreach (MyNode child in adjacencyList[node])
            {
                Console.Write(child.Value.ToString() + " + ");
                sum += child.Value;
            }
            Console.WriteLine($"= {sum}");
        }
    }
}
