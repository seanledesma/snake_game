using System;

class Snake {
    /*
     * Here is the layout for the program:
     * - need a method to draw the border and fruit and snake
     * - need a method to get input
     * - need main game loop
     * - 
     */
    private static bool play = true;
    private const int second = 1000;
    private const int height = 16;
    private const int width = 36;
    private static int snakeLength;
    private static int snakeX = 5;
    private static int snakeY = 7;
    private static int fruitX = 12; // change this to random number
    private static int fruitY = 7; // change this to random number

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
                for (int k = 0; k < width; k++) // print right side
                {
                    if (i == fruitY && k == fruitX) // print fruit
                        Console.Write("F");
                    else if (i == snakeY && k == snakeX) // print snake head
                        Console.Write("O");
                    else if (k < width - 1)
                        Console.Write(" ");

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
    }


    static void input() {
        if (Console.KeyAvailable) {
            //ConsoleKeyInfo cki = Console.ReadKey();
            //Console.WriteLine(cki.KeyChar);

            char key = Console.ReadKey().KeyChar;
            //Console.WriteLine(key);
            switch (key) {
                case 'w':
                    snakeY--;
                    break;
                case 's':
                    snakeY++;
                    break;
                case 'a':
                    snakeX--;
                    break;
                case 'd':
                    snakeX++;
                    break;
                default:
                    break;
            }
        }
    }

    static void logic() {
        
    }


    static void Main(string[] args) {
        while (play) {
            //Console.Clear();
            draw();
            input();
            //logic();//TODO
            Thread.Sleep(second/30);
        }
    }
}