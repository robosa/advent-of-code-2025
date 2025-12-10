using Google.OrTools.LinearSolver;

var watch = System.Diagnostics.Stopwatch.StartNew();

int part1 = 0;
int part2 = 0;
foreach (string line in File.ReadLines("day10.txt"))
{
    Machine machine = new(line);
    part1 += machine.Part1();
    part2 += machine.Part2();
}

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

class Machine
{
    private readonly int Lights = 0;
    private readonly List<int> Joltages = [];
    private readonly List<int> Buttons = [];
    private readonly int NumButtons;

    internal Machine(string line)
    {
        foreach (string token in line.Split())
        {
            if (token[0] == '[')
            {
                for (int i = 1; i < token.Length - 1; i++)
                    if (token[i] == '#')
                        Lights |= 1 << (i - 1);
            }
            else if (token[0] == '{')
            {
                foreach (string n in token[1..(token.Length - 1)].Split(','))
                    Joltages.Add(int.Parse(n));
            }
            else
            {
                int button = 0;
                foreach (string n in token[1..(token.Length - 1)].Split(','))
                    button |= 1 << int.Parse(n);
                Buttons.Add(button);
            }
        }
        NumButtons = Buttons.Count;
    }

    internal int Part1() => Part1Rec(0, 0, 0);
    private int Part1Rec(int b, int p, int state)
    {
        if (state == Lights)
            return p;
        if (b == NumButtons)
            return int.MaxValue;
        return Math.Min(Part1Rec(b + 1, p + 1, state ^ Buttons[b]), Part1Rec(b + 1, p, state));
    }

    internal int Part2()
    {
        Solver solver = Solver.CreateSolver("SAT");

        Variable[] button_pushes = solver.MakeIntVarArray(NumButtons, 0, Joltages.Max());
        for (int i = 0; i < Joltages.Count; i++)
        {
            Constraint constraint = solver.MakeConstraint(Joltages[i], Joltages[i]);
            for (int j = 0; j < NumButtons; j++)
                constraint.SetCoefficient(button_pushes[j], (Buttons[j] >> i) & 1);
        }

        Objective total_pushes = solver.Objective();
        for (int j = 0; j < NumButtons; j++)
            total_pushes.SetCoefficient(button_pushes[j], 1);
        total_pushes.SetMinimization();

        solver.Solve();
        return (int)total_pushes.Value();
    }
}
