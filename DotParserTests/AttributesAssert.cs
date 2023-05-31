using DotParser.DOT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;
using Edge = DotParser.DOT.Edge;

[assembly: InternalsVisibleTo("DotParser")]

namespace DotParserTests;

public static class AttributesAssert
{
    public static void AreEqual(Edge expected, Edge actual)
    {
        CollectionAssert.AreEquivalent(expected.Attributes, actual.Attributes);
    }

    public static void AreEqual(ICollection<Edge> expected, ICollection<Edge> actual)
    {
        if (expected.Count != actual.Count)
            Assert.Fail();

        for (int i = 0; i < expected.Count; i++)
            AreEqual(expected.ElementAt(i), actual.ElementAt(i));
    }

    public static void AreEqual(Node expected, Node actual)
    {
        CollectionAssert.AreEquivalent(expected.Attributes, actual.Attributes);
    }

    public static void AreEqual(ICollection<Node> expected, ICollection<Node> actual)
    {
        if (expected.Count != actual.Count)
            Assert.Fail();

        for (int i = 0; i < expected.Count; i++)
            AreEqual(expected.ElementAt(i), actual.ElementAt(i));
    }
}