using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Diagnostics;

namespace SpaceInvaders
{
    internal class Player
    {
        public int x;
        public int y;
        private ConsoleColor _color;
        private int speed = 1;
        private int life = 3;
        private int armor;

        public bool moveAllowRight =false;
        public bool moveAllowLeft = false;

       


        //private const string PLAYERDRAW = " X ";

        private string[] drawPlayer =
        {
            @"      ▄",
            @"█    ███    █",
            @"█ ▄███ ██▄  █",
            @"█▄██ ███ ██▄█",
            @"██▄█ ███ █▄██"
        };


        public Player()
        {
            x = 30;
            y = 55;
            _color = ConsoleColor.White;
            moveAllowLeft = true;
            moveAllowRight = true;
        }
        public bool isAlive()
        {
            return life > 0;
        }

        public void Draw()
        {
            Console.ForegroundColor = _color;

            for (int i=0; i<5; i++)
            {
                Console.SetCursorPosition(this.x, this.y + i);
                Console.WriteLine(drawPlayer[i]);
            }



            //Console.SetCursorPosition(x, y);
            //if (this.isAlive())
            //{
            //    Console.Write(PLAYERDRAW);
            //}
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.Write(PLAYERDRAW);

            //}
        }
        public void Move(bool moveLeft, bool moveRight)
        {
            if (moveLeft)
            {
                x=x-speed;
            }
            if (moveRight)
            {
                x = x+speed;
            }
        }

    }
}
