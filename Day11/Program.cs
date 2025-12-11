var watch = System.Diagnostics.Stopwatch.StartNew();

Network network = new("day11.txt");

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

long part1 = network.Part1();
long part2 = network.Part2();

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

class Network
{
    private readonly Dictionary<string, string[]> Adjacencies = [];
    private readonly Dictionary<(string, int), long> DfsCache = [];

    internal Network(string filename)
    {
        foreach (string line in File.ReadLines(filename))
        {
            string[] words = line.Split();
            Adjacencies.Add(words[0][..3], words[1..]);
        }
    }

    internal long Part1() { DfsCache.Clear(); return RecDfs("you", 0, false); }
    internal long Part2() { DfsCache.Clear(); return RecDfs("svr", 0, true); }

    private long RecDfs(string input, int flag, bool part2)
    {
        switch (input)
        {
            case "out": return (!part2 || flag == 0b11) ? 1 : 0;
            case "fft": flag |= 0b01; break;
            case "dac": flag |= 0b10; break;
        }
        if (DfsCache.TryGetValue((input, flag), out long cached))
            return cached;
        long result = Adjacencies[input].Sum(x => RecDfs(x, flag, part2));
        DfsCache.Add((input, flag), result);
        return result;
    }
}
