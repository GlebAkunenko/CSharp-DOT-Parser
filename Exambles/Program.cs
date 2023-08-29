using Exambles;

ExampleRunner runner = new Exambles.SimpleMathExample.Runner();

string path = "D:\\Projects\\DotNET\\DotParser\\Exambles\\SimpleMathGraph.txt";

string DOT = File.ReadAllText(path);

runner.Run(DOT);