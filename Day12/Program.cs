var watch = System.Diagnostics.Stopwatch.StartNew();

string input = File.ReadAllText("day12.txt");
string[] splitted = input.Split("\n\n");
int n = splitted.Length;

int[] shape_areas = [.. splitted[..(n - 1)].Select(s => s.Count(c => c == '#'))];

int part1 = 0;
foreach (string grid in splitted[n - 1].Trim().Split('\n'))
{
    int grid_area = int.Parse(grid[0..2]) * int.Parse(grid[3..5]);
    int min_area = 0;
    int max_area = 0;
    for (int i = 0; i < shape_areas.Length; i++)
    {
        int count = int.Parse(grid[(7 + 3 * i)..(9 + 3 * i)]);
        min_area += shape_areas[i] * count;
        max_area += 9 * count;
    }
    if (grid_area < min_area)
        continue;
    if (grid_area >= max_area)
    {
        part1++;
        continue;
    }
    Console.WriteLine("Too complicated...");
    part1 = 0;
    break;
}

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
