var watch = System.Diagnostics.Stopwatch.StartNew();

List<Point> red_tiles = [];

foreach (string line in File.ReadAllLines("day09.txt"))
{
    string[] coords = line.Split(',');
    red_tiles.Add(new Point(long.Parse(coords[0]), long.Parse(coords[1])));
}

int length = red_tiles.Count;
int half_length = length / 2;

long part1 = 0;
long part2 = 0;
for (int i = 0; i < length; i++)
{
    for (int j = i + 1; j < length; j++)
        part1 = Math.Max(part1, red_tiles[i].AreaWith(red_tiles[j]));

    /* Part 2: The shape is roughly a circle with the two points in the middle
     * crossing the shape. So the largest rectangle is necesseraly in one of the
     * two halves, and one of its corner is one of the half points.
     * Also, because of this particular shape, checking that no other red tile
     * is strictly inside a rectangle is enough. */
    if (i == half_length || i == half_length + 1)
        continue;
    (Point other, int low, int high) = (i < half_length) ?
        (red_tiles[half_length], 0, half_length) :
        (red_tiles[half_length + 1], half_length + 2, length);
    (long minX, long maxX) = red_tiles[i].MinMaxX(other);
    (long minY, long maxY) = red_tiles[i].MinMaxY(other);
    bool is_ok = true;
    for (int j = low; j < high; j++)
        if (red_tiles[j].IsInside(minX, maxX, minY, maxY))
        {
            is_ok = false;
            break;
        }
    if (is_ok)
        part2 = Math.Max(part2, red_tiles[i].AreaWith(other));
}

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

record Point(long X, long Y)
{
    internal (long, long) MinMaxX(Point other) => (X > other.X) ? (other.X, X) : (X, other.X);
    internal (long, long) MinMaxY(Point other) => (Y > other.Y) ? (other.Y, Y) : (Y, other.Y);
    internal long AreaWith(Point other)
    {
        long dx = Math.Abs(X - other.X) + 1;
        long dy = Math.Abs(Y - other.Y) + 1;
        return dx * dy;
    }
    internal bool IsInside(long minX, long maxX, long minY, long maxY)
    {
        return (X > minX && X <= maxX && Y > minY && Y <= maxY);
    }
}
