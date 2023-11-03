//ETML
//Auteur : Adrian Toledo
//Date : 01.09.2023
//Description : Réalisation d'un jeux video Spicy Invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Diagnostics;


namespace SpicyInvaders
{
    public class MainCode
    {
        public static void Main(string[] args)
        {            
            Console.Title = "Spicy Invaders";
            Console.SetWindowSize(200, 50);
            Console.SetBufferSize(200, 50);
            
            GameEngine engine = new GameEngine();
        }
    }
}
