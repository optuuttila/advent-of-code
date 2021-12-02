Console.WriteLine("Day 2. Dive!");
var runInTestMode = true;
if (runInTestMode) {
    Console.WriteLine("Running with test input.");
    Console.WriteLine(" Excepted result 1: 150");
    Console.WriteLine(" Excepted result 2: 900");

    Console.WriteLine(" Add your real input to input.txt file and change the runInTestMode to false");
}

var moves = GetMoves(runInTestMode);

(var position, var depth) = CalculateCourse(moves, false);
Console.WriteLine($"Result 1: {position * depth}");

(position, depth) = CalculateCourse(moves, true);

Console.WriteLine($"Result 2: {position * depth}");

//Testing out the multiple return values
(int position, int depth) CalculateCourse(List<Movement> moves, bool useAim) {
    int aim = 0;
    int position = 0;
    int depth = 0;
    foreach(var move in moves) {
        //In task 1 (useAim = false) only rude position/speed calculation is done
        //In task 2 (useAim = true) also the aim is taken in consideration
        switch (move.Direction, useAim) {
            case ("forward", false):
                position += move.Speed;
                break;
            case ("up", false):
                depth -= move.Speed;
                break;
            case ("down", false):
                depth += move.Speed;
                break;
            case ("forward", true):
                position += move.Speed;
                depth += aim * move.Speed;
                break;
            case ("up", true):
                aim -= move.Speed; 
                break;
            case ("down", true):
                aim += move.Speed;
                break;
        }
    }
    return (position, depth);
}

List<Movement> GetMoves(bool test) {
    var results = new List<Movement>();
    foreach(var line in GetLines(test)) {
        //Every line is "direction speed" where direction is "forward/up/down" and speed is integer
        var movement = line.Split(' ');
        results.Add(new Movement{
            Direction = movement[0],
            Speed = int.Parse(movement[1])
        });
    }
    return results;
}

List<string> GetLines(bool test) {
    var input = test ? "input-test.txt" : "input.txt";
    //dotnet 6 is very friendly. We don't need to tell the full assembly System.IO.File
    return File.ReadAllLines(input).ToList();
}

record Movement {
    public string Direction {get;set;} = "forward";
    public int Speed {get;set;} = 0;
}