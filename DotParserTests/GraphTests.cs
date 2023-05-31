using DotParser.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edge = DotParser.DOT.Edge;
using Node = DotParser.DOT.Node;
using GNode = DotParser.Graphs.Node;
using GEdge = DotParser.Graphs.Edge;
using Attribute = DotParser.DOT.Attribute;
using System.Net.Http.Headers;

namespace DotParserTests;

[TestClass]
public class GraphTests
{
    [TestMethod]
    public void GetAdjacencyList()
    {
        // a -> b, c u -> v b -> a, c n m
        RawGraph input = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a", "b"),
                new Edge("a", "c"),
                new Edge("u", "v"),
                new Edge("b", "a"),
                new Edge("b", "c"),
            },
            Nodes = new List<Node>() {
                new Node("a"),
                new Node("b"),
                new Node("c"),
                new Node("u"),
                new Node("v"),
                new Node("n"),
                new Node("m")
            }
        };

        Graph graph = new GraphBuilder().BuildGraph(input);

        Dictionary<GNode, List<GNode>> actual = graph.GetAdjacencyList();

        GNode GN(string name) { return new GNode(new Node(name)); }

        List<GNode> expected_a = new List<GNode>() { GN("b"), GN("c") };
        List<GNode> expected_u = new List<GNode>() { GN("v") };
        List<GNode> expected_b = new List<GNode>() { GN("a"), GN("c") };
        List<GNode> empty = new List<GNode>();

        CollectionAssert.AreEquivalent(expected_a, actual[GN("a")]);
        CollectionAssert.AreEquivalent(expected_u, actual[GN("u")]);
        CollectionAssert.AreEquivalent(expected_b, actual[GN("b")]);
        CollectionAssert.AreEquivalent(empty, actual[GN("n")]);
        CollectionAssert.AreEquivalent(empty, actual[GN("m")]);
    }
}