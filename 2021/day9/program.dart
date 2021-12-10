import 'dart:io';
import 'dart:math';

void main() async {
  print('Day 9. Smoke basin.');
  var test = false;

  var file = File(test ? 'input-test.txt' : 'input.txt');

  var lines = file.readAsLinesSync();
  int rows = lines.length;
  int cols = lines[0].length;
  print("Map is $rows * $cols");
  var smokeMap = List.generate(
      rows, (i) => List.filled(cols, 0, growable: false),
      growable: false);

  int row = 0;
  for (var line in lines) {
    //print('$line: ${line.length} characters');
    int col = 0;
    for (var c in line.split("")) {
      smokeMap[row][col] = int.parse(c);
      col++;
    }
    row++;
  }

  int lowPoints = 0;
  int riskLevel = 0;
  for (row = 0; row < rows; row++) {
    for (var col = 0; col < cols; col++) {
      //Check value west
      if (col == 0 || smokeMap[row][col] < smokeMap[row][col - 1]) {
        //Check value east
        if (col == cols - 1 || smokeMap[row][col] < smokeMap[row][col + 1]) {
          //Check value north
          if (row == 0 || smokeMap[row][col] < smokeMap[row - 1][col]) {
            //Check value south
            if (row == rows - 1 ||
                smokeMap[row][col] < smokeMap[row + 1][col]) {
              lowPoints++;
              riskLevel += smokeMap[row][col] + 1;
            }
          }
        }
      }
    }
  }
  print("Count of low points $lowPoints and their risk-level is $riskLevel");

  List<int> basins = [];
  for (row = 0; row < rows; row++) {
    for (var col = 0; col < cols; col++) {
      if (smokeMap[row][col] != 9) {
        int basin = 0;
        basin = basinSum(smokeMap, row, col);
        if (basin > 0) {
          basins.add(basin);
        }
      }
    }
  }
  int max1 = basins.reduce(max);
  basins.remove(max1);
  int max2 = basins.reduce(max);
  basins.remove(max2);
  int max3 = basins.reduce(max);
  int total = max1 * max2 * max3;
  print("Three biggest basins $max1, $max2, $max3 = $total ");
}

int basinSum(List<List<int>> map, int x, int y) {
  int maxX = map.length;
  int maxY = map[0].length;
  //Check off-grid
  if (x < 0 || y < 0 || x >= maxX || y >= maxY) {
    return 0;
  }
  //Check high-peak
  if (map[x][y] == 9) {
    return 0;
  }
  map[x][y] = 9; //Mark handled
  return 1 +
      basinSum(map, x - 1, y) +
      basinSum(map, x + 1, y) +
      basinSum(map, x, y - 1) +
      basinSum(map, x, y + 1);
}
