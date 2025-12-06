var watch = System.Diagnostics.Stopwatch.StartNew();

string[] lines = File.ReadAllLines("day06.txt");
int nb_lines = lines.Length - 1;
int line_length = lines[0].Length;

string[] ops = SplitLine(lines[nb_lines]);
long[][] values = [.. lines[..nb_lines].Select(x => SplitLine(x).Select(long.Parse).ToArray())];

long part1 = 0;
long part2 = 0;

int cursor = 0;
for (int i = 0; i < ops.Length; i++)
{
    bool is_add = ops[i] == "+";

    // Part1
    long res1 = is_add ? 0 : 1;
    for (int j = 0; j < nb_lines; j++)
        res1 = is_add ? res1 + values[j][i] : res1 * values[j][i];
    part1 += res1;

    // Part2
    long res2 = is_add ? 0 : 1;
    while (cursor < line_length)
    {
        long next_val = 0;
        bool empty = true;
        for (int j = 0; j < nb_lines; j++)
        {
            char next_char = lines[j][cursor];
            if (next_char == ' ')
                continue;
            next_val = next_val * 10 + (long)char.GetNumericValue(next_char);
            empty = false;
        }
        cursor++;
        if (empty)
            break;
        res2 = is_add ? res2 + next_val : res2 * next_val;
    }
    part2 += res2;
}
watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

static string[] SplitLine(string line)
{
    return line.Split(null as char[], StringSplitOptions.RemoveEmptyEntries);
}
