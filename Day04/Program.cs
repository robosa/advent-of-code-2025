var watch = System.Diagnostics.Stopwatch.StartNew();

Grid grid = new();
int part1 = grid.Part1();
int part2 = grid.Part2();

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

class Grid
{
    readonly (int, int)[] directions = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];
    readonly char[][] grid;
    readonly int height;
    readonly int width;

    public Grid()
    {
        grid = [.. File.ReadAllLines("day04.txt").Select(x => x.ToCharArray())];
        height = grid.Length;
        width = grid[0].Length;
    }

    public int Part1(bool remove = false)
    {
        int reachable = 0;
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                if (grid[i][j] != '@')
                    continue;
                int adjacent = 0;
                foreach ((int di, int dj) in directions)
                {
                    (int ni, int nj) = (i + di, j + dj);
                    if (ni >= 0 && ni < height && nj >= 0 && nj < width && grid[ni][nj] == '@')
                    {
                        adjacent++;
                        if (adjacent >= 4)
                            break;
                    }
                }
                if (adjacent < 4)
                {
                    reachable++;
                    if (remove)
                        grid[i][j] = '.';
                }
            }
        return reachable;
    }

    public int Part2()
    {
        int total_removed = 0;
        while (true)
        {
            int removed = Part1(true);
            if (removed == 0)
                return total_removed;
            total_removed += removed;
        }
    }
}
