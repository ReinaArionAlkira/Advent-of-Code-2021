

//Day 1 - Measurements
//If result is 0 - it's something wrong with data or diff variable
Console.WriteLine(Day1(1));
Console.WriteLine(Day1(2));


int Day1(int part)
{
    int diff = 0;
    int amount = 0;
    if (part == 1) diff = 1;
    else if (part == 2) diff = 3;

    string fileName = "input.txt";
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Data\", fileName));
    //Console.WriteLine(path);

    int[] array = File.ReadLines(path).Where(line => !string.IsNullOrEmpty(line)).Select(line => int.Parse(line)).ToArray();
    for (int i = 0; i < array.Length; i++)
    {
        if (i == 0 || (diff == 3 && i < 3)) continue;
        else if (array[i] > array[i - diff]) amount++;
    }
    return amount;
}
