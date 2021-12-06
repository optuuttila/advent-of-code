package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
	"time"
)

//Reading files requires checking most calls for errors. This helper will streamline our error checks below.
//https://gobyexample.com/reading-files
func check(e error) {
	if e != nil {
		panic(e)
	}
}
func main() {
	const test = true
	const days = 256

	var file = "input.txt"
	if test {
		file = "input-test.txt"
	}
	dat, err := os.ReadFile(file)
	check(err)

	var bruteForceTimer = time.Now()
	//split into str array and convert to int array
	//feels really awkward but couldn't find a better way to do this 
	var str = strings.Split(string(dat), ",")
	var fishes []int
	for _, digit := range str {
		i, err := strconv.Atoi(digit)
		check(err)
		fishes = append(fishes, i)
	}

	//Only run Brute force with small days amount. It gets VERY SLOW with higher day count!!
	if test && days < 100 {
		for day := 1; day <= days; day++ {
			fmt.Printf("%v = %v", fishes, len(fishes))

			var newFish = 0
			for index := range fishes {
				fishes[index]--
				if fishes[index] == -1 {
					fishes[index] = 6
					newFish++
				}
			}
			fmt.Printf(" new fishes added: %v\n", newFish)
			if newFish > 0 {
				for i := 0; i < newFish; i++ {
					fishes = append(fishes, 8)
				}
			}
		}
	}

	fmt.Printf("\n(BRUTE FORCE) Total fishes after %v days are %v\n\n", days, len(fishes))
	var bruteForceElapsed = time.Since(bruteForceTimer)

	var betterAlgoTimer = time.Now()
	str = strings.Split(string(dat), ",")
	fishes = []int{0, 0, 0, 0, 0, 0, 0, 0, 0}
	for _, digit := range str {
		i, err := strconv.Atoi(digit)
		check(err)
		fishes[i]++
	}

	for day := 1; day <= days; day++ {
		var fishCount = sum(fishes)
		fmt.Printf("%v is sum of %v", fishes, fishCount)

		var newFish = fishes[0]
		for i := 0; i < 8; i++ {
			fishes[i] = fishes[i+1]
		}
		fishes[8] = 0;
		fmt.Printf(" new fishes added: %v\n", newFish)

		if newFish > 0 {
			fishes[6] += newFish
			fishes[8] += newFish
		}
	}

	var fishCount = sum(fishes)
	fmt.Printf("\n(BETTER ALGO) Total fishes after %v days are %v", days, fishCount)
	var betterAlgoElapsed = time.Since(betterAlgoTimer)

	fmt.Printf("\n\nBrute force execution time: %v\n", bruteForceElapsed)
	fmt.Printf("Better algo execution time: %v\n", betterAlgoElapsed)
}

func sum(array []int) int {
	result := 0
	for _, v := range array {
		result += v
	}
	return result
}
