using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    internal class Bullet
    {
        public int x;
        public int y;
        public int speed = 3;
        public bool alive = true;
        public ConsoleColor colorBullet;

        public bool fromPlayer;
        public bool destroyed;

        private string simpleDrawBullet = "█";
        private string drawDeathBullet = "";

        public Bullet(int originX, int originY, ConsoleColor bulletColor, bool player)
        {
            x = originX;
            y = originY;
            colorBullet = bulletColor;

            fromPlayer = player;

            /*Heritage
            _x = originX; 
            _y = originY;
            _speed = speed;
            _life = oLife;
            _isAlive = true;
            _color = bulletColor;
            */
        }

        public void Draw()
        {
            Console.ForegroundColor = colorBullet;
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(simpleDrawBullet);
        }
        public void Move()
        {
            if (fromPlayer)
            {
                y = y - speed;
            }
            else
            {
                y = y + speed;
            }
        }

        public void Destroy()
        {
            alive = false;
        }
    }
}
