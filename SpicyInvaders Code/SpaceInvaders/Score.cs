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

        /// <summary>
        /// Méthode de score qui demande d'enregistrer le nombre initial de points
        /// </summary>
        /// <param name="scoreO"></param>
        public Score(int scoreO)
        {

            points = scoreO;
        }
        
    }
}
