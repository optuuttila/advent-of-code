const fs = require('fs')

const input = 0 ? "input-test.txt" : "input.txt";

fs.readFile(input, function(err, data) {
    if(err) throw err;

    //Split into array
    const arr = data.toString().replace(/\r\n/g,'\n').split('\n');

    //https://stackoverflow.com/questions/10474992/split-a-javascript-string-into-fixed-length-pieces
    String.prototype.chunk = function(size) {
        return [].concat.apply([],
            this.split('').map(function(x,i){ return i%size ? [] : parseInt(this.slice(i,i+size).replace(' ', '')) }, this)
        )
    }

    const numbers = arr[0].split(',');
    console.log("Drawing bingo numbers: " + numbers);
    
    //Generate board 
    let boards = [];
    for(var i=2; i < arr.length; i+=6) {
        const rows = arr.slice(i,i+5);
        let board = [];
        for (let row of rows) {
            board.push(row.chunk(3));
        }
        boards.push(board);
    }
    console.log("We got " + boards.length + " Bingo boards in game");    

    console.log("Starting to draw numbers: ");
    let boardsWithBingo = [];
    for (let number of numbers ) {
        let b = undefined;
        console.log("   Shouting number " + number);
        for(let [index, board] of boards.entries()) {
            if (boardsWithBingo.find(b=>b.index === index)) continue;
            markNumberInBoard(number, board);
            let sum = bingo(number, board);
            if (sum) {
                b = index;
                boardsWithBingo.push({ index, sum, board, number, total: number*sum});
                console.log("       BINGO at board: " + index)
            }
        }
    }

    console.log("There were " + boards.length + " and " + boardsWithBingo.length + " had bingo")
    console.log("First bingo was at board " + JSON.stringify(boardsWithBingo[0]));
    console.log(boards[boardsWithBingo[0].index]);

    console.log("Last bingo was at board " + JSON.stringify(boardsWithBingo[boardsWithBingo.length-1]));
    console.log(boards[boardsWithBingo[boardsWithBingo.length-1].index]);

    function markNumberInBoard(number, board) {
        for(let row of board) {
            const index = row.findIndex(i => i == number);
            if (index > -1) {
                row[index] = 'X';
            }
        }        
    }

    function bingo(number, board) {
        function sumLine(arr) { 
            let sum = 0;
            for (let i = 0; i < arr.length; i++) 
                if (arr[i] !== 'X') {
                    sum += arr[i]; 
                }
       
            return sum; 
        } 

        //Get the original board sum for results in case we have bingo
        let sum = 0;
        for(let row of board) {
            sum += sumLine(row);
        }
        const copy = [...board];
        //Create new array lines for the vertical checks
        for(var i=0; i< 5; i++) {
            copy.push([board[0][i], board[1][i], board[2][i], board[3][i], board[4][i] ])
        }
        for(var i=0; i< 10; i++) {
            if (sumLine(copy[i]) === 0) {
                return sum;
            }
        }  
        return undefined;
    }
});