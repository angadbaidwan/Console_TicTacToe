# TicTacToe Console Application
![Console_TicTacToe](https://user-images.githubusercontent.com/25090977/210668474-e35471f9-ab35-438f-8c20-c5d07547071c.png)

## Description
This is a windows console application that allows you to play TicTacToe against an imperfect AI opponent.

## Functional Requires Specification

### Functional Requirements
1. ask user to pick a symbol
2. print tictactoe grid on screen with labeled rows (alphabetical) and columns (numerical)
3. prompt user for coordinate text input for where they want to play
4. play as the opposite symbol in a random open space on the board
5. print the updated game board
6. prompt user for coordinate text input for next move
7. repeat pompt, play, and print commands until someone wins or the game board is full
8. print victory, defeat or draw text
9. ask user to play again or exit

### Data
Gameboard
- userSymbol - must be 'o' or 'x'
- cpuSymbol - must be 'o' or 'x'

Coordinate
- x coordinate - range a-c
- y coordinate - range 1-3

### Exception Scenarios
Invalid user input
- print appropriate error message, repeat prompt


### Non-Functional Requirements
N/A
