using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpicyInvaders;

namespace SpicyInvaders
{
    internal class Alien
    {
        public int x;
        public int y;
        public ConsoleColor colorAlien;
        public int speed = 5;
        public int life = 3;
        public bool alive = true;

        public bool moveLeft = false;
        public bool moveDown = false;

        public int assetLimitX = 10;
        public int assetLimitY = 5;

        private string[] drawAlien3 =
        {
            @"  ▀▄   ▄▀  ",
            @" ▄█▀███▀█▄ ",
            @"█▀███████▀█",
            @"█ █▀▀▀▀▀█ █",
            @"   ▀▀ ▀▀   ",
        };
        //11x5
        private string[] drawAlien2 =
        {
            @"  ▀▄   ▄▀  ",
            @" ▄█▀███▀█▄ ",
            @"█▀███████▀█",
            @"█ █▀▀      ",

        };
        // 11x4
        private string[] drawAlien1 =
        {
            @"  ▀▄   ▄▀  ",
            @" ▄█▀███▀█▄ ",
            @"█▀ ██ ██ ▀█",
        };
        //11 x 3
        public Alien(int originX, int originY, ConsoleColor colorSkin)
        {
            x = originX + 1;
            y = originY;
            colorAlien = colorSkin;
        }
        public void Draw()
        {
            if (alive)
            {
                Console.ForegroundColor = colorAlien;
                if (life == 3)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.SetCursorPosition(this.x, this.y + i);
                        Console.WriteLine(drawAlien3[i]);
                    }
                }
                else if (life == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.SetCursorPosition(this.x, this.y + i);
                        Console.WriteLine(drawAlien2[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.SetCursorPosition(this.x, this.y + i);
                        Console.WriteLine(drawAlien1[i]);
                    }
                }

            }

        }
        public void Move()
        {
            if (moveLeft)
            {
                x = x - speed;
            }
            else
            {
                x = x + speed;
            }
        }
    }
}
