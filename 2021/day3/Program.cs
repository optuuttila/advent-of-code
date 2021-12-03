Console.WriteLine("Day 3.");
var runInTestMode = true;
if (runInTestMode) {
    Console.WriteLine("Running with test input.");
    Console.WriteLine(" Excepted result 1: 150");
    //Console.WriteLine(" Excepted result 2: 900");

    Console.WriteLine(" Add your real input to input.txt file and change the runInTestMode to false");
}

var data = GetTaskData(runInTestMode);

(var sum, var gamma, var epsilon, var co2, var oxygen) = CalculateX(data);
Console.WriteLine($"Result 1: gamma {gamma} and epsilon {epsilon} have decimal sum of {sum}");

(var d, var binary) = CalculateTwo(data, true);
Console.WriteLine($"Result 2: oxygen {binary} have decimal sum of {d}");
(d, binary) = CalculateTwo(data, false);
Console.WriteLine($"Result 2: c02 {binary} have decimal sum of {d}");

//(gammaRate, depth) = CalculateX(data, true);

//Console.WriteLine($"Result 2: {position * depth}");

//Testing out the multiple return values
(int sum, string gamma, string epsilon, List<string> co2, List<string> oxygen) CalculateX(List<string> inputs) {
    string gamma = "";
    string epsilon = "";
    var oxygen = new List<string>();
    var co2 = new List<string>();
    for(int i=0; i < inputs[0].Length; i++) {
        int zero = 0;
        int one = 1;        
        foreach(var input in inputs) {
            var bit = input.Skip(i).Take(1).First();
            if (bit == '0') {
                zero++;
            } else {
                one++;
            }
        }
        //Console.WriteLine(zero + " " + one);
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

    return (sum, gamma, epsilon, co2, oxygen);
}


(int zero, int one) GetZeroOne(List<string> inputs, int position) {
    int zero = 0;
    int one = 1;        
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

(int d, string binary) CalculateTwo(List<string> inputs, bool bitToUse) {
    string gamma = "";
    string epsilon = "";
    for(int i=0; i < 5; i++) {
        var workList = new List<string>();
        workList.AddRange(inputs);
        (var zero, var one) = GetZeroOne(workList, i);
        Console.WriteLine($"zero: {zero}, one:  {one}");
        foreach(var input in workList) {
            if (bitToUse && one >= zero) {
                inputs.RemoveAll(x=>x==input);
            } 
            if (!bitToUse && zero >= one) {
                inputs.RemoveAll(x=>x==input);
            }
        }
        Console.WriteLine(inputs.Count);
        if (inputs.Count == 1) {
            break;
        }
    }
    Console.WriteLine(inputs.Count + " " + inputs.First());

    return (0, inputs.First());
}


List<string> GetTaskData(bool test) {
    return GetLines(test);
}

List<string> GetLines(bool test) {
    var input = test ? "input-test.txt" : "input.txt";
    //dotnet 6 is very friendly. We don't need to tell the full assembly System.IO.File
    return File.ReadAllLines(input).ToList();
}