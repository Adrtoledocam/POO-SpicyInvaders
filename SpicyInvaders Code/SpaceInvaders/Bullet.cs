using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    public class Bullet :ObjectBase
    {

        public bool fromPlayer;
        public bool destroyed;

        private string simpleDrawBullet = "█";
        private string drawDeathBullet = "";

        public Bullet(int originX, int originY, ConsoleColor bulletColor, bool player, int speed, int life)
        {
            _x = originX;
            _y = originY;
            _color = bulletColor;
            _speed = speed;
            _life = life;

            fromPlayer = player;
        }

        public void Draw()
        {
            Console.ForegroundColor = _color;
            if (!_OutLimitWindowYCheck(Console.BufferHeight)) 
            {
                Console.SetCursorPosition(_x, _y);
            }
            Console.Write(simpleDrawBullet);
        }
        public void Move()
        {
            if (fromPlayer)
            {
                _y = _y - _speed;
            }
            else
            {
                _y = _y + _speed;
            }
        }

        
    }
}
