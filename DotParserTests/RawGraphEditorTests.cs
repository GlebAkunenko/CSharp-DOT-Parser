using DotParser.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edge = DotParser.DOT.Edge;
using Node = DotParser.DOT.Node;
using Attribute = DotParser.DOT.Attribute;


namespace DotParserTests;

[TestClass]
public class RawGraphEditorTests
{
    [TestMethod]
    public void AddEdgeAttrsToRightNode()
    {
        RawGraph input = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a", "c", new Attribute[] {new("k2", "k2"), new("k3", "v3")}),
                new Edge("c", "a", new Attribute[] {new("k2", "v2"), new("k3", "v3")})
            },
            Nodes = new List<Node>() {
                new Node("a", new Attribute[] {new("k1", "v1")}),
                new Node("b", new Attribute[] {new("k1", "v1")}),
                new Node("c")
            }
        };

        RawGraphEditor editor = new RawGraphEditor(input);
        editor.AddEdgeAttrsToRightNode();

        RawGraph actual = editor.Graph;

        RawGraph expected = new RawGraph() {
            Edges = new List<Edge>() {
                new Edge("a", "c", new Attribute[] { new("k2", "v2"), new("k3", "v3") }),
                new Edge("c", "a", new Attribute[] { new("k2", "v2"), new("k3", "v3") })
            },
            Nodes = new List<Node>() {
                new Node("a", new Attribute[] { new("k1", "v1"), new("k2", "v2"), new("k3", "v3") }),
                new Node("b", new Attribute[] { new("k1", "v1")}),
                new Node("c", new Attribute[] { new("k2", "v2"), new("k3", "v3") })
            }
        };

        CollectionAssert.AreEquivalent(expected.Edges, actual.Edges);
        CollectionAssert.AreEquivalent(expected.Nodes, actual.Nodes);
        AttributesAssert.AreEqual(expected.Nodes, actual.Nodes);
        AttributesAssert.AreEqual(expected.Edges, actual.Edges);
    }
}
