using System;

class Snake {
    /*
     * Here is the layout for the program:
     * - need a method to draw the border and fruit and snake
     * - need a method to get input
     * - need main game loop
     * - 
     */
    private static bool gameOver = false;
    private const int second = 1000;
    private const int height = 15;
    private const int width = 40;
    private static int snakeLength;
    private static int headX = 5;
    private static int headY = 7;
    private static int fruitX = 12; // change this to random number
    private static int fruitY = 7; // change this to random number
    private static int trunkCount = 0;
    private static int[,] trunk = new int[20,2];


    static bool drawAssist(int Y, int X) {      //fixes the problem we were having with the spaces and right wall
        
        for (int m = 0; m <= trunkCount; m++) {
            if (trunk[m, 0] == Y && trunk[m, 1] == X) {
                return true;
            }

            if (trunk[m, 0] == headY && trunk[m, 1] == headX)
                gameOver = true;
        }
        return false;
    }
    
    static void draw() {
        Console.Clear();
        for (int i = 0; i < height; i++) // main for loop to go down height
        {
            if (i == 0) {
                for (int j = 0; j < width; j++) // print top bar
                {
                    Console.Write("#");
                }
            }

            Console.Write("#"); // print left side

            if (i > 0 && i < height - 1) {
                for (int k = 0; k < width; k++) { // print right side
                    
                    if (i == headY && k == headX) // print snake head
                        Console.Write("O");
                    else if (drawAssist(i, k) == true)
                            Console.Write("o");
                    else if (i == fruitY && k == fruitX) // print fruit
                        Console.Write("F");
                    else if (k < width - 1) {
                        Console.Write(" ");
                    }
                    
                    if (k == width - 1)
                        Console.Write("#");
                }
            }

            if (i == height - 1) // print bottom
            {
                for (int l = 0; l < width; l++) {
                    Console.Write("#");
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine("trunkCount size: " + trunkCount);
        Console.WriteLine("headY: " + headY);
        Console.WriteLine("headX: " + headX);
        Console.WriteLine(trunk[0, 0]);
        Console.WriteLine(trunk[0, 1]);
        Console.WriteLine(trunk[1, 0]);
        Console.WriteLine(trunk[1, 1]);
        Console.WriteLine(trunk[2, 0]);
        Console.WriteLine(trunk[2, 1]);
    }
    
    
    static void slither(int y, int x) {
        //TODO: get the snake to slither once a second independent of game loop
        //but for right now just get body to follow head
        if (trunkCount > 0) {
            trunk[0, 0] = y;
            trunk[0, 1] = x;
            for (int i = trunkCount; i > 0; i--) {
                trunk[i, 0] = trunk[i - 1, 0];
                trunk[i, 1] = trunk[i - 1, 1];
            }
        }
    }
    
    static void input() {
        if (Console.KeyAvailable) {
            char key = Console.ReadKey().KeyChar;
            switch (key) {
                case 'w':
                    slither(headY, headX);
                    headY--;
                    break;
                case 's':
                    slither(headY, headX);
                    headY++;
                    break;
                case 'a':
                    slither(headY, headX);
                    headX--;
                    break;
                case 'd':
                    slither(headY, headX);
                    headX++;
                    break;
                default:
                    break;
            }
        }
    }
    
    /*
     * Logic to do list:
     * - need to make sure body continues in direction at certain speed (even if player does not touch anything)
     * - collision
     * - score board
     */
    static void logic() {
        if (headX == fruitX && headY == fruitY) {
            Random random = new Random();
            fruitX = random.Next(1, width - 1);
            fruitY = random.Next(1, height - 1);
            trunkCount++;
        }

        if (headX == -1 || headX == width-1)
            gameOver = true;
        if (headY == 0 || headY == height - 1)
            gameOver = true;
        //body collision added in drawAssist() to avoid extra for loops

    }


    static void Main(string[] args) {
        while (gameOver == false) {
            draw();
            input();
            logic();

            Thread.Sleep(second/30);
        }
        Console.WriteLine();
        Console.WriteLine("Game Over!");
    }
}