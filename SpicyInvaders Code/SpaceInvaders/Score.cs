using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    public class Score
    {
        public int points;
        public int pointsForEnemy = 200;
        public int pointsForDmg = 50;
        public Score(int scoreO)
        {

            points = scoreO;
        }

    }
}
