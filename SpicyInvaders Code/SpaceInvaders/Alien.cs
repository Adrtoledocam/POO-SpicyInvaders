using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpicyInvaders;

namespace SpicyInvaders
{
    public class Alien : ObjectBase
    {
        /// <summary>
        /// Variables spécifiques  à Alien
        /// </summary>
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
     
        private string[] drawAlien2 =
        {
            @"  ▀▄   ▄▀  ",
            @" ▄█▀███▀█▄ ",
            @"█▀███████▀█",
            @"█ █▀▀      ",

        };
        
        private string[] drawAlien1 =
        {
            @"  ▀▄   ▄▀  ",
            @" ▄█▀███▀█▄ ",
            @"█▀ ██ ██ ▀█",
        };
        
        public Alien(int originX, int originY, ConsoleColor colorSkin, int speed, int life)
        {
            _x = originX + 1;
            _y = originY;
            _color = colorSkin;
            _speed = speed;
            _life = life;
        }
        /// <summary>
        /// La méthode Draw() affichera la forme de l'alien en fonction de sa quantité de vie.
        /// </summary>
        public void Draw()
        {
            if (this._IsAlive())
            {
                Console.ForegroundColor = _color;
                if (_life == 3)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.SetCursorPosition(this._x, this._y + i);
                        Console.WriteLine(drawAlien3[i]);
                    }
                }
                else if (_life == 2)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.SetCursorPosition(this._x, this._y + i);
                        Console.WriteLine(drawAlien2[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.SetCursorPosition(this._x, this._y + i);
                        Console.WriteLine(drawAlien1[i]);
                    }
                }

            }

        }
        /// <summary>
        ///Méthode Move() exprimant le mouvement avec la vitesse sur l'axe des x
        /// </summary>
        public void Move()
        {
            if (moveLeft)
            {
                _x = _x - _speed;
            }
            else
            {
                _x = _x + _speed;
            }
        }
    }
}
