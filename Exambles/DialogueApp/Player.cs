using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exambles;

public class DialogueAdd : ExampleRunner
{
    public override void Run(string graphCode)
    {
        
    }
}

/// <summary>
/// There is a property 'Charisma' deals with his set of available answers
/// There is a property 'Coins'
/// </summary>
public class Player
{
    public int Charisma { get; set; }
    public int Coins { get; set; }
}