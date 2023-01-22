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
    private static int headX = 5;
    private static int headY = 7;
    private static int fruitX = 12; // change this to random number
    private static int fruitY = 7; // change this to random number
    private static int trunkCount = 0;
    private static int[,] trunk = new int[20,2];
    

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
                    else {
                        for (int m = 0; m <= trunkCount; m++) {
                            if (trunk[m, 0] == i && trunk[m, 1] == k) {
                                //i == trunk[0, 0] && k == trunk[0, 1
                                Console.Write("o");
                            }
                                
                                
                        }
                    }
                    
                    if (i == fruitY && k == fruitX) // print fruit
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
    }
    
    
    static void slither(int y, int x) {
        //TODO: get the snake to slither once a second independent of game loop
        //but for right now just get body to follow head
        if (trunkCount > 0) {
            
            for (int i = trunkCount; i > 0; i--) {
                trunk[i, 0] = trunk[i - 1, 0];
                trunk[i, 1] = trunk[i - 1, 1];
            }
            trunk[0, 0] = y;
            trunk[0, 1] = x;
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
     * - need to establish rule for body to follow head correctly
     * - need to add one body part per fruit consumed
     * - need to set bounds to play area (may be in different function though)
     * - also need to make sure body continues in direction at certain speed (even if player does not touch anything)
     * - once fruit is eaten, generate new random number for fruit pos
     */
    static void logic() {
        if (headX == fruitX && headY == fruitY) {
            Random random = new Random();
            fruitX = random.Next(1, width - 1);
            fruitY = random.Next(1, height - 1);
            trunkCount++;//don't think I need this
            
            
        }
    }


    static void Main(string[] args) {
        while (play) {
            draw();
            input();
            logic();
            //slither();

            Thread.Sleep(second/30);
        }
    }
}