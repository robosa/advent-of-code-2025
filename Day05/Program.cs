var watch = System.Diagnostics.Stopwatch.StartNew();

List<Interval> intervals = [];
bool intervals_parsed = false;
long part1 = 0;
foreach (string line in File.ReadLines("day05.txt"))
{
    if (line == string.Empty)
    {
        intervals.Sort();
        intervals_parsed = true;
        continue;
    }
    if (!intervals_parsed)
    {
        long[] bounds = [.. line.Split('-', count: 2).Select(long.Parse)];
        intervals.Add(new Interval(bounds[0], bounds[1]));
        continue;
    }
    long value = long.Parse(line);
    if (intervals.Any(x => x.Contains(value)))
        part1++;
}

long part2 = 0;
long current = -1;
foreach (Interval interval in intervals)
{
    if (interval.Low > current)
    {
        part2 += interval.Size;
        current = interval.High;
    }
    else if (interval.High > current)
    {
        part2 += interval.High - current;
        current = interval.High;
    }
}

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

record Interval(long Low, long High) : IComparable<Interval>
{
    internal long Size => High - Low + 1;
    internal bool Contains(long value) => Low <= value && value <= High;

    public int CompareTo(Interval? other)
    {
        if (other == null)
            return 1;
        return (Low, High).CompareTo((other.Low, other.High));
    }
}
