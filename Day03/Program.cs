var watch = System.Diagnostics.Stopwatch.StartNew();

long[] result = [0, 0];

foreach (string line in File.ReadAllLines("day03.txt"))
{
    List<long> batteries = [.. line.ToCharArray()
        .Select(x => (long)char.GetNumericValue(x))];
    foreach ((int idx, int size) in new[] { (0, 2), (1, 12) })
    {
        List<long> djoltage = batteries[..size];
        for (int i = 1; i < batteries.Count + 1 - size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (batteries[i + j] > djoltage[j])
                {
                    djoltage.RemoveRange(j, size - j);
                    djoltage.AddRange(batteries[(i + j)..(i + size)]);
                    break;
                }
            }
        }
        result[idx] += djoltage.Aggregate((x, y) => x * 10 + y);
    }
}

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {result[0]}");
Console.WriteLine($"Part 2: {result[1]}");
