import 'dart:io';
import 'dart:convert';
import 'dart:async';

void main() async {
  print('Day 7. The treachery of whales.');
  var test = true;

  var file = File(test ? 'input-test.txt' : 'input.txt');

  Stream<String> lines = file
      .openRead()
      .transform(utf8.decoder) // Decode bytes to UTF-8.
      .transform(LineSplitter()); // Convert stream to individual lines.
  try {
    await for (var line in lines) {
      var data = line.split(',');
      print('$line: ${line.length} characters');
      //var crabs = Map<int, int>();
      int max = 0;

      //Over-engineered the shit out of this for task 1 as I was expecting a different kind of task2
      //My brain froze due to this and it took a ridicilous amount to solve the task2 plus switching out of that thinking was surprisingly hard
      for (final d in data) {
        var position = int.parse(d);
        if (position > max) {
          max = position;
        }
        /*
        // This ended up being totally useless in the end. Leaving it here as a reminder on not to over-engineer!
        // I was thinking that I create a map with crab position and then number of the craps there.
        // Writing this now it would have worked also, I just would have needed to add the positiions with no crabs at all to that map as well
        if (crabs.containsKey(position)) {
          crabs.update(position, (int) => crabs[position]! + 1);
        } else {
          crabs[position] = 1;
        }*/
      }

      int? smallestMoves = null;
      int? smallestMovesTwo = null;

      var crabsAll = line.split(',').map(int.parse);

      //Need to loop all possible entries, not just the ones with crabs in them. This took a long time to figure out as I desperately was trying to use my map of crabs.
      for (int i = 0; i <= max; i++) {
        var moves = 0, moves2 = 0;
        for (var current in crabsAll) {
          var diff = (i - current).abs();
          moves += diff;
          int sum = countSum(diff);
          moves2 += sum;
        }

        if (smallestMoves == null || moves < smallestMoves) {
          smallestMoves = moves;
        }
        if (smallestMovesTwo == null || moves2 < smallestMovesTwo) {
          smallestMovesTwo = moves2;
        }
      }
      print("Smallest amount of moves needed are $smallestMoves");
      print("Smallest amount of moves needed for part2 $smallestMovesTwo");
    }
  } catch (e) {
    print('Error: $e');
  }
}

countSum(int max) {
  int sum = 0;
  for (int i = 0; i <= max; i++) {
    sum += i;
  }
  return sum;
}
