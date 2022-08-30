

//Day 1 - Measurements
//If result is 0 - it's something wrong with data or diff variable
//Console.WriteLine(Day1(1));
//Console.WriteLine(Day1(2));
using System.IO;

//Day 2 - Position
//Console.WriteLine(Day2(1));
//Console.WriteLine(Day2(2));

//Day 3 - Power consumption
Console.WriteLine(Day3(1));
//Console.WriteLine(Day3(2));


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

int Day2(int part)
{
    int withAim = 0;
    if (part == 2) withAim = 1; 
    // position [0] - horizontal position
    // position [1] - depth position
    // position [2] - aim (its  depth position in part 1)
    int[] position = new int[3];
    string fileName = "input2.txt";
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Data\", fileName));

    List<KeyValuePair<string, int>> commands = new List<KeyValuePair<string, int>>();
    
    using (StreamReader r = new StreamReader(path))
    {
        string line;
        while ((line = r.ReadLine()) != null)
        {
            commands.Add(new KeyValuePair<string, int>(line.Split(" ")[0], int.Parse(line.Split(" ")[1])));
        }
    }
    //foreach(var command in commands)
    //{
    //    Console.WriteLine(command.Value.GetType());
    //}
    foreach(var command in commands)
    {
        int distance = command.Value;
        switch (command.Key)
        {
            case "forward":
                position[0] += distance;
                // if its part 1 we don't need this variable so multiply by 0
                position[1] += distance * position[2] * withAim;
                break;
            case "down":
                position[2] += distance;
                break;
            case "up":
                position[2] -= distance;
                break;
        }
    }
    if(part == 2) return position[0] * position[1];
    else return position[0] * position[2];
}

int Day3(int part)
{
    string fileName = "input3.txt";
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Data\", fileName));
    string[] array = File.ReadLines(path).Where(line => !string.IsNullOrEmpty(line)).ToArray();

    string gamma = "";
    string epsilon = "";
    string[] oxygenGenerator = array;
    string[] co2Scrubber = array;
    int consumption = 0;
    for (int i = 0; i < array[0].Length; i++)
    {

        //if rate is higher than 0 - we have more zeros
        // rate is lower? - more ones
        // rate == 0? - equal number of zeros and ones
        if (part == 1)
        {
            int rate = 0;
            foreach (var line in array)
            {
                if (line[i] == '0') rate++;
                else rate--;
            }
            if (rate > 0)
            {
                gamma += '0';
                epsilon += '1';
            }
            else
            {
                gamma += '1';
                epsilon += '0';
            }
            if (i == array[0].Length - 1)
            {
                int decGamma = Convert.ToInt32(gamma.ToString(), 2);
                int decEpsilon = Convert.ToInt32(epsilon.ToString(), 2);
                consumption = decGamma * decEpsilon;
                Console.WriteLine("consumption ");
                return consumption;
            }
        }
        else
        {
            int rateO = 0;
            int rateC = 0;
            foreach (var line in oxygenGenerator)
            {
                if (line[i] == '0') rateO++;
                else rateO--;
                //Console.WriteLine(line);
            }
            foreach (var line in co2Scrubber)
            {
                if (line[i] == '0') rateC++;
                else rateC--;
                //Console.WriteLine(line);
            }
            if (oxygenGenerator.Length != 1) {
                if (rateO > 0) oxygenGenerator = oxygenGenerator.Where(val => val[i] == '0').ToArray();
                else oxygenGenerator = oxygenGenerator.Where(val => val[i] == '1').ToArray();
            }
            if (co2Scrubber.Length != 1) 
                if(rateC > 0) co2Scrubber = co2Scrubber.Where(val => val[i] == '1').ToArray();
                else co2Scrubber = co2Scrubber.Where(val => val[i] == '0').ToArray();
        }
        if (i == array[0].Length - 1)
        {
            int decOxygenGenerator = Convert.ToInt32(oxygenGenerator[0].ToString(), 2);
            int decCo2Scrubber = Convert.ToInt32(co2Scrubber[0].ToString(), 2);
            int lifeSupport = decOxygenGenerator * decCo2Scrubber;
            Console.WriteLine("lifeSupport ");
            return lifeSupport;
        }
    }

    return 5;
}