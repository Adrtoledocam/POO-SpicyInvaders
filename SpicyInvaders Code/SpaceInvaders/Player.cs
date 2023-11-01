using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Diagnostics;

namespace SpicyInvaders
{
    internal class Player
    {
        public int x;
        public int y;
        public ConsoleColor _color;
        public int speed = 1;
        public int life = 3;
        public bool isAlive = true;
        public bool moveAllowRight = false;
        public bool moveAllowLeft = false;
        public bool canAttack = true;

        public string nickName;
        public int dimensionX = 13;
        public int dimensionY = 5;

        private string[] drawPlayer =
        {
            @"      ▄",
            @"█    ███    █",
            @"█ ▄███ ██▄  █",
            @"█▄██ ███ ██▄█",
            @" █▄█ ███ █▄█ "
        };

        public Player(int originX, int originY, ConsoleColor color)
        {
            x = originX;
            y = originY;
            _color = color;

            moveAllowLeft = true;
            moveAllowRight = true;
        }


        public void Draw()
        {
            if (life == 3)
            {
                Console.ForegroundColor = _color;

            }
            else if (life == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

            }
            else if (life == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }

            if (life != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(this.x, this.y + i);
                    Console.WriteLine(drawPlayer[i]);
                }
            }
            else
            {
                Console.WriteLine("      X");
            }

        }
        public void Move(bool moveLeft, bool moveRight)
        {
            if (moveLeft)
            {
                x = x - speed;
            }
            if (moveRight)
            {
                x = x + speed;
            }
        }

    }
}
