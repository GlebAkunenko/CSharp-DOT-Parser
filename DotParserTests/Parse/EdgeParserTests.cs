using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attribute = DotParser.DOT.Attribute;
using System.Reflection;
using DotParser.DOT;

namespace DotParser.Parse.Tests;

[TestClass]
public class EdgeParserTests
{
    [TestMethod]
    public void ParseFromString_a2c2a2c2b_a2c1c2a1c2b()
    {
        var edgesParser = new EdgesParser(true);
        string line = "a -> c -> a->c -> b";

        Edge[] expected = new Edge[] {
            new Edge("a", "c"),
            new Edge("c", "a"),
            new Edge("c", "b")
        };

        Edge[] actual = edgesParser.ParseFromString(line);

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void ParseFromString_a2c2b_a2c1c2a1c2b1b2c()
    {
        var edgesParser = new EdgesParser(false);
        string line = "a -- c -- b";

        Edge[] expected = new Edge[] {
            new Edge("a", "c"),
            new Edge("c", "a"),
            new Edge("c", "b"),
            new Edge("b", "c")
        };

        Edge[] actual = edgesParser.ParseFromString(line);

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void ParseFromString_a2cb2d_a2c1a2b1c2d1b2d()
    {
        var edgesParser = new EdgesParser(true);
        string line = "a -> c,b->d";

        Edge[] actual = edgesParser.ParseFromString(line);

        Edge[] expected = new Edge[] {
            new Edge("a", "c"),
            new Edge("a", "b"),
            new Edge("b", "d"),
            new Edge("c", "d")
        };

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void ParseFromString_a2bcd_a2b1a2c1a2d()
    {
        var edgesParser = new EdgesParser(false);
        string line = "a -- b, c, d";

        Edge[] actual = edgesParser.ParseFromString(line);

        Edge[] expected = new Edge[] {
            new Edge("a", "b"),
            new Edge("a", "c"),
            new Edge("a", "d"),
            new Edge("b", "a"),
            new Edge("c", "a"),
            new Edge("d", "a")
        };

        CollectionAssert.AreEquivalent(expected, actual);
    }

    public void ParseFromString_crosslink()
    {
        var edgesParser = new EdgesParser(true);
        string line = "a1,a2,a3 -> b1, b2 -> c1, c2 , c3 ";

        Edge[] actual = edgesParser.ParseFromString(line);

        Edge[] expected = new Edge[] {
            new Edge("a1", "b1"),
            new Edge("a2", "b1"),
            new Edge("a3", "b1"),
            new Edge("a1", "b2"),
            new Edge("a2", "b2"),
            new Edge("a3", "b2"),
            new Edge("b1", "c1"),
            new Edge("b1", "c2"),
            new Edge("b1", "c3"),
            new Edge("b2", "c1"),
            new Edge("b2", "c2"),
            new Edge("b2", "c3")
        };

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void ParseFromString_wrongSep_zeroEdges()
    {
        var edgesParser = new EdgesParser(true);
        string line = "a -- c -- b";

        Edge[] actual = edgesParser.ParseFromString(line);
        Edge[] expected = new Edge[0];

        CollectionAssert.AreEqual(expected, actual);
    }
}