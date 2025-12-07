var watch = System.Diagnostics.Stopwatch.StartNew();

string[] lines = File.ReadAllLines("day07.txt");

long[] beams = new long[lines[0].Length];
beams[lines[0].IndexOf('S')] = 1;

long part1 = 0;
long part2 = 1;

for (int i = 1; i < lines.Length; i++)
    for (int j = 0; j < beams.Length; j++)
        if (beams[j] > 0 && lines[i][j] == '^')
        {
            part1++;
            part2 += beams[j];
            beams[j - 1] += beams[j];
            beams[j + 1] += beams[j];
            beams[j] = 0;
        }

watch.Stop();
Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
