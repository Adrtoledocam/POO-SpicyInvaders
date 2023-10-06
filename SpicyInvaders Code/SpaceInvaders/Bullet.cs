using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    internal class Bullet
    {
        public int x;
        public int y;
        public int speed = 4;
        public bool alive = true;
        public ConsoleColor colorBullet;

        public bool destroyed;

        private string[] drawBullet =
        {
            @"█",
            @"█",          
        };

        private string simpleDrawBullet = "█";
        private string drawDeathBullet = "";

        public Bullet(int originX, int originY, ConsoleColor bulletColor)
        {
            x= originX; 
            y= originY;
            colorBullet = bulletColor;
        }

        public void Draw()
        {
            Console.ForegroundColor = colorBullet;
            //for (int i =0; i<2;i++)
            //{
            //    Console.SetCursorPosition(this.x, this.y + i);
            //    Console.Write(drawBullet[i]);
            //}
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(simpleDrawBullet);
        }
        public void Move()
        {
            y = y - speed;
        }

        public void Destroy()
        {
            alive= false;
            Console.Write(drawDeathBullet);
        }
    }
}
