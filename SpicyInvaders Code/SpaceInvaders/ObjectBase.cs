using System;
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
        public ConsoleColor _color { get; set;}
        public int _speed { get; set; }
        public int _life { get; set; }
        public bool _isAlice { get; set; }
    }
}
