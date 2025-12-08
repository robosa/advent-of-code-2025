var watch = System.Diagnostics.Stopwatch.StartNew();

Box[] boxes = [.. File.ReadLines("day08.txt").Select(x => new Box(x))];
PriorityQueue<(int, int), double> dists = new();
Network network = new(boxes.Length);

int box1 = 0;
int box2 = 0;
for (box1 = 0; box1 < boxes.Length; box1++)
    for (box2 = box1 + 1; box2 < boxes.Length; box2++)
        dists.Enqueue((box1, box2), boxes[box1].Distance(boxes[box2]));

long part1 = 0;
long part2 = 0;
int iter = 1;
while (!network.FullyConnected)
{
    (box1, box2) = dists.Dequeue();
    network.Connect(box1, box2);
    if (++iter == 1000)
        part1 = network.Part1();
}
part2 = boxes[box1].X * boxes[box2].X;

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

class Box
{
    internal readonly long X, Y, Z;

    internal Box(string line)
    {
        string[] coords = line.Split(',');
        X = long.Parse(coords[0]);
        Y = long.Parse(coords[1]);
        Z = long.Parse(coords[2]);
    }

    internal double Distance(Box other)
    {
        long dx = X - other.X;
        long dy = Y - other.Y;
        long dz = Z - other.Z;
        return dx * dx + dy * dy + dz * dz;
    }
}

class Network(int size)
{
    private readonly List<HashSet<int>> _networks = [];

    internal bool FullyConnected => _networks.Count == 1 && _networks[0].Count == size;

    internal void Connect(int box1, int box2)
    {
        int net1 = -1;
        int net2 = -1;
        for (int k = 0; k < _networks.Count; k++)
        {
            if (_networks[k].Contains(box1))
                net1 = k;
            if (_networks[k].Contains(box2))
                net2 = k;
        }
        if (net1 == -1 && net2 == -1)
            _networks.Add(new HashSet<int>([box1, box2]));
        else if (net1 == -1)
            _networks[net2].Add(box1);
        else if (net2 == -1)
            _networks[net1].Add(box2);
        else if (net1 != net2)
        {
            _networks[net1].UnionWith(_networks[net2]);
            _networks.RemoveAt(net2);
        }
    }

    internal long Part1()
    {
        _networks.Sort((a, b) => b.Count.CompareTo(a.Count));
        return _networks[0].Count * _networks[1].Count * _networks[2].Count;
    }
}
