﻿using System;

class Snake {
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
    private static int speed = 250;
    private static int direction = 0;
    private static int[,] trunk = new int[40,2];//Probably should make array bigger or do something else, to avoid index out of bounds
    private static System.Timers.Timer timer = new System.Timers.Timer();
    


    static bool drawAssist(int Y, int X) {      //fixes the problem we were having with the spaces and right wall
        
        for (int m = 0; m <= trunkCount; m++) {
            if (trunk[m, 0] == Y && trunk[m, 1] == X) {
                return true;
            }
            //body collision
            if (trunk[m, 0] == headY && trunk[m, 1] == headX)
                gameOver = true;
        }
        return false;
    }
    
    static void draw() {
        Console.Clear();
        Console.WriteLine("Score: " + trunkCount);
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
                    //leaving the following method calls and such incase don't want auto slither
                    //slither(headY, headX);
                    //headY--;
                    direction = 1;
                    break;
                case 's':
                    //slither(headY, headX);
                    //headY++;
                    direction = 2;
                    break;
                case 'a':
                    //slither(headY, headX);
                    //headX--;
                    direction = 3;
                    break;
                case 'd':
                    //slither(headY, headX);
                    //headX++;
                    direction = 4;
                    break;
                default:
                    break;
            }
        }
    }
    
    /*
     * Logic to do list:
     * OPTIONAL
     * - score board
     * - perhaps all time score board as well
     * - variable speed of slither??
     */
    static void logic() {
        //second counter and slither here
        if (headX == fruitX && headY == fruitY) {
            Random random = new Random();
            fruitX = random.Next(1, width - 1);
            fruitY = random.Next(1, height - 1);
            trunkCount++;
        }
        //wall collision
        if (headX == -1 || headX == width - 1)
            gameOver = true;
        if (headY == 0 || headY == height - 1)
            gameOver = true;
        //body collision added in drawAssist() to avoid extra for loops

    }

    private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
        switch (direction) {
            case 0: 
                slither(headY, headX);
                headX++;
                break;
            case 1:
                slither(headY, headX);
                headY--;
                break;
            case 2:
                slither(headY, headX);
                headY++;
                break;
            case 3:
                slither(headY, headX);
                headX--;
                break;
            case 4:
                slither(headY, headX);
                headX++;
                break;
            default:
                break;
        }
    }


    static void Main(string[] args) {
        timer.Interval = speed;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
        while (gameOver == false) {
            draw();
            input();
            logic();
            
            Thread.Sleep(second/60);
        }
        Console.WriteLine();
        Console.WriteLine("Game Over!     Score: " + trunkCount);
        timer.Stop();
    }
}