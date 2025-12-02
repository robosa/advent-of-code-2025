var watch = System.Diagnostics.Stopwatch.StartNew();

int dial = 50;
int password1 = 0;
int password2 = 0;
char direction = 'R';

foreach (string rotation in File.ReadAllLines("day01.txt"))
{
    if (rotation[0] != direction)
    {
        dial = (dial == 0) ? 0 : 100 - dial;
        direction = rotation[0];
    }
    password2 += Math.DivRem(dial + int.Parse(rotation[1..]), 100, out dial);
    password1 += (dial == 0) ? 1 : 0;
}

watch.Stop();
Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine($"Part 1: {password1}");
Console.WriteLine($"Part 2: {password2}");

