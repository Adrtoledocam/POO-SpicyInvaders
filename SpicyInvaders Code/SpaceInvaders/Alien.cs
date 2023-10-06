using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders;

namespace SpaceInvaders
{
    //Task: creer alien
    internal class Alien
    {
        //Task: créer attributs
        public int x;
        public int y;
        public ConsoleColor colorAline;
        public int speed = 5;
        public int life = 1;
        public ConsoleColor colorBline;
        public bool alive = true;

        public bool moveRight = true;
        public bool moveLeft = false;
        public bool moveDown = false;

        public bool collisionX = false;
        public bool collisionY = false;


        private string[] drawAlien =
        {
            @"  ▀▄   ▄▀  ",
            @" ▄█▀███▀█▄ ",
            @"█▀███████▀█",
            @"█ █▀▀▀▀▀█ █",
            @"   ▀▀ ▀▀   ",
        };
        //11 x 5
        public Alien(int originX, int originY, ConsoleColor colorSkin )
        {
            x = originX;
            y = originY;
            colorAline = colorSkin;
        }

        //public bool isAlive()
        //{
        //    return life> 0;
        //}
        public void alienPositions()
        {
            List<int> positionsX = new List<int>();
            List<int> positionsY = new List<int>();


        }
        public void Draw()
        {
            if (alive)
            {
                Console.ForegroundColor = colorAline;
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(this.x, this.y + i);
                    Console.WriteLine(drawAlien[i]);
                }
            }
            else
            {
                Console.WriteLine("");

            }

        }
        //Task: Creer methode alien
        public void Move(bool moveRightBool)
        {
            int limitLeft = 10;
            int limitRight = Console.WindowWidth-20;
            
            if (!moveRightBool)
            {
                x = x+speed;
            }
            else
            {
                x = x-speed;
            }
        }
    }
}
