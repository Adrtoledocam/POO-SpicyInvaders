using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Diagnostics;

namespace SpicyInvaders
{
    public class Player : ObjectBase
    {
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

        /// <summary>
        /// Initialisation de l'objet et enregistrement de ses valeurs
        /// </summary>
        /// <param name="originX"></param>
        /// <param name="originY"></param>
        /// <param name="color"></param>
        /// <param name="speed"></param>
        /// <param name="life"></param>
        public Player(int originX, int originY, ConsoleColor color, int speed, int life )
        {
            _x = originX;
            _y = originY;
            _color = color;
            _speed = speed;
            _life = life;

            moveAllowLeft = true;
            moveAllowRight = true;
        }

        /// <summary>
        /// La méthode Draw() affichera la forme de le player avec different couleur en fonction de sa quantité de vie.
        /// </summary>
        public void Draw()
        {
            if (_life == 3)
            {
                Console.ForegroundColor = _color;

            }
            else if (_life == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

            }
            else if (_life == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }

            if (_life != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(this._x, this._y + i);
                    Console.WriteLine(drawPlayer[i]);
                }
            }
            else
            {
                Console.WriteLine("      X");
            }

        }
        /// <summary>
        /// /// <summary>
        ///Méthode Move() exprimant le mouvement avec la vitesse sur l'axe des x et en funcition de le sens de mouvement
        /// </summary>
        /// </summary>
        /// <param name="moveLeft"></param>
        /// <param name="moveRight"></param>
        public void Move(bool moveLeft, bool moveRight)
        {
            if (moveLeft)
            {
                _x = _x - _speed;
            }
            if (moveRight)
            {
                _x = _x + _speed;
            }
        }

    }
}
