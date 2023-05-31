using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotParser.Graphs;

public class NodeFactory
{
    public virtual Node GetNode(DOT.Node source)
    {
        return new Node(source);
    }
}
