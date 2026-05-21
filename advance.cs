using System;
namespace Hasan
{
    public static Main()
    {
        int[] numbers = new int[] { 1, 2, 3, 4, 5 };
        foreach(var number in numbers){
            Console.Write(number);

            var numbers = new int[20];

        
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1;
        }

        foreach (var number in numbers)
        {
            Console.Write($" {number}");
        }
        }

        string[,] chessBoard = new string[8, 8];

        // Creating the starting positions of a Chessboard
        chessBoard[0, 0] = "Rook";
        chessBoard[0, 1] = "Knight";
        chessBoard[0, 2] = "Bishop";
        chessBoard[0, 3] = "Queen";
        chessBoard[0, 4] = "King";
        chessBoard[0, 5] = "Bishop";
        chessBoard[0, 6] = "Knight";
        chessBoard[0, 7] = "Rook";

        chessBoard[1, 0] = "Pawn";
        chessBoard[1, 1] = "Pawn";
        chessBoard[1, 2] = "Pawn";
        chessBoard[1, 3] = "Pawn";
        chessBoard[1, 4] = "Pawn";
        chessBoard[1, 5] = "Pawn";
        chessBoard[1, 6] = "Pawn";
        chessBoard[1, 7] = "Pawn";

        chessBoard[6, 0] = "Pawn";
        chessBoard[6, 1] = "Pawn";
        chessBoard[6, 2] = "Pawn";
        chessBoard[6, 3] = "Pawn";
        chessBoard[6, 4] = "Pawn";
        chessBoard[6, 5] = "Pawn";
        chessBoard[6, 6] = "Pawn";
        chessBoard[6, 7] = "Pawn";

        chessBoard[7, 0] = "Rook";
        chessBoard[7, 1] = "Knight";
        chessBoard[7, 2] = "Bishop";
        chessBoard[7, 3] = "Queen";
        chessBoard[7, 4] = "King";
        chessBoard[7, 5] = "Bishop";
        chessBoard[7, 6] = "Knight";
        chessBoard[7, 7] = "Rook";


        // Print the chessboard
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                string piece = chessBoard[row, col] ?? "Empty";
                Console.Write($"{piece}\t");
            }
            Console.WriteLine();
    } 

    int[][] daysInMonths = new int[12][];

// Initialize each month with its corresponding number of days
daysInMonths[0] = new int[31]; 
daysInMonths[1] = new int[28]; 
daysInMonths[2] = new int[31]; 
daysInMonths[3] = new int[30]; 
daysInMonths[4] = new int[31]; 
daysInMonths[5] = new int[30];
daysInMonths[6] = new int[31]; 
daysInMonths[7] = new int[31]; 
daysInMonths[8] = new int[30]; 
daysInMonths[9] = new int[31]; 
daysInMonths[10] = new int[30];
daysInMonths[11] = new int[31]; 

// Print the number of days in each month
for (int month = 0; month < daysInMonths.Length; month++)
{
    Console.WriteLine($"Month {month + 1}: {daysInMonths[month].Length} days");
}
    }  
}