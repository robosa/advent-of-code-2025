var watch = System.Diagnostics.Stopwatch.StartNew();

HashSet<long> part1_set = [];
HashSet<long> part2_set = [];

string input = File.ReadAllText("day02.txt");

static long BuildInvalidId(long prefix, int repeats)
{
    long tens = (long)Math.Pow(10, Math.Floor(Math.Log10(prefix) + 1));
    long res = 0;
    for (int i = 0; i < repeats; i++)
        res = res * tens + prefix;
    return res;
}

foreach (var range in input.Split(','))
{
    string[] bounds = range.Split('-', count: 2);
    long left = long.Parse(bounds[0]);
    long right = long.Parse(bounds[1]);
    for (int i = 2; i <= bounds[1].Length; i++)
    {
        string start_str = bounds[0][..(bounds[0].Length / i)];
        long start = (start_str.Length > 0) ? long.Parse(start_str) : 1;
        HashSet<long> new_ids = [.. Enumerable.InfiniteSequence(start, 1)
            .Select(x => BuildInvalidId(x, i))
            .Where(x => x >= left)
            .TakeWhile(x => x <= right)];
        if (i == 2)
            part1_set.UnionWith(new_ids);
        part2_set.UnionWith(new_ids);
    }
}

long part1 = part1_set.Sum();
long part2 = part2_set.Sum();

watch.Stop();
Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");
