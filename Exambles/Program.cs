using Exambles;

ExampleRunner runner = new Exambles.SimpleMathExaple.Runner();

string path = "D:\\Projects\\DotNET\\DotParser\\Exambles\\SimpleMathGraph.txt";

string DOT = File.ReadAllText(path);

runner.Run(DOT);