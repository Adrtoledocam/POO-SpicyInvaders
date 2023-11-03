﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    public class ObjectBase
    {
        public int _x { get; set; }
        public int _y { get; set; }
        public ConsoleColor _color { get; set; }
        public int _speed { get; set; }
        public int _life { get; set; }
        public bool _isAlive { get; set; }


        public bool _IsAlive()
        {
            if (_life <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool _OutLimitWindowYCheck(int windowHeight)
        {
            if (this._y <= 0 || this._y >= windowHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
