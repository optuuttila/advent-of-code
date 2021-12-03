Console.WriteLine("Day 3. Binary diagnostic");
var runInTestMode = true;
if (runInTestMode) {
    Console.WriteLine("Running with test input.");
    Console.WriteLine(" Excepted result 1: 198");
    Console.WriteLine(" Excepted result 2: 230 (oxygen: 23, co2: 10)");

    Console.WriteLine(" Add your real input to input.txt file and change the runInTestMode to false");
}

var data = GetTaskData(runInTestMode);

(var sum, var gamma, var epsilon) = CalculateGammaAndEpsilon(data);
Console.WriteLine($"Result 1: gamma {gamma} and epsilon {epsilon} have decimal sum of {sum}");

(var oxygen, var binary) = CalculateLifeSupport(data, true);
Console.WriteLine($"Result 2: oxygen {binary} has a decimal sum of {oxygen}");

(var co2, binary) = CalculateLifeSupport(data, false);
Console.WriteLine($"Result 2: c02 {binary} has a decimal sum of {co2}");

Console.WriteLine($"Result 2 final: {oxygen * co2}");

(int sum, string gamma, string epsilon) CalculateGammaAndEpsilon(List<string> inputs) {
    string gamma = "";
    string epsilon = "";
    for(int i=0; i < inputs[0].Length; i++) {
        (var zero, var one) = GetZeroOne(inputs, i);
 
        if (zero > one) {
            gamma += "0";
            epsilon += "1";
        } else {
            gamma += "1";
            epsilon += "0";
        }
    }

    var g = Convert.ToInt32(gamma,2);
    var e = Convert.ToInt32(epsilon,2);
    var sum = g*e;

    return (sum, gamma, epsilon);
}

(int zero, int one) GetZeroOne(List<string> inputs, int position) {
    int zero = 0;
    int one = 0;        
    foreach(var input in inputs) {
        var bit = input.Skip(position).Take(1).First();
        if (bit == '0') {
            zero++;
        } else {
            one++;
        }
    }

    return (zero, one);
}

(int d, string binary) CalculateLifeSupport(List<string> inputs, bool bitToUse) {
    //Create local copy so that the original input can be reused
    var copy = new List<string>(inputs);
        
    for(int i=0; i < inputs[0].Length; i++) {
        var workList = new List<string>();
        workList.AddRange(copy);
        (var zero, var one) = GetZeroOne(workList, i);

        foreach(var input in workList) {
            var current = input.Skip(i).Take(1).First();
            if (bitToUse)
            {
                if (one > zero && current == '0')
                {
                    copy.RemoveAll(x => x == input);
                }
                if (zero > one && current == '1')
                {
                    copy.RemoveAll(x => x == input);
                }
            }
            else
            {
                if (one > zero && current == '1')
                {
                    copy.RemoveAll(x => x == input);
                }
                if (zero > one && current == '0')
                {
                    copy.RemoveAll(x => x == input);
                }
            }
            if (zero == one && bitToUse && current == '0')
            {
                copy.RemoveAll(x => x == input);
            }
            if (zero == one && !bitToUse && current == '1')
            {
                copy.RemoveAll(x => x == input);
            }

        }
        //Exit if we only have one left as that is the one!
        if (copy.Count == 1) {
            break;
        }
    }
    var binary = copy.First();
    var d = Convert.ToInt32(binary, 2);
    return (d, binary);
}


List<string> GetTaskData(bool test) {
    return GetLines(test);
}

List<string> GetLines(bool test) {
    var input = test ? "input-test.txt" : "input.txt";
    //dotnet 6 is very friendly. We don't need to tell the full assembly System.IO.File
    return File.ReadAllLines(input).ToList();
}