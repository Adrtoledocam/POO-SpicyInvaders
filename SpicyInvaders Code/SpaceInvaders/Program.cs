using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Diagnostics;

//ETML
//Auteur : Adrian Toledo
//Date : 01.09.2023
//Description : Réalisation d'un jeux video Spicy Invaders

//Limitar la consola
//Limitar el movimiento del jugador y corregir
//Crear enemigos
//Movimiento de enemigos
//Disparos
//Collisiones
//Muerte enemigos y jugador
//



namespace SpaceInvaders
{
    

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Spicy Invaders II ";
            Title();

            Console.CursorVisible = false;

            Player player = new Player();

            List<Alien> enemies = new List<Alien>();
            for (int i = 0;i < 10;i++)
            {
                enemies.Add(new Alien(10+(i*13), 10, ConsoleColor.Red));

            }
            //enemies.Add(new Alien(10,10,ConsoleColor.Red));
            //enemies.Add(new Alien(23,10,ConsoleColor.Red));
            //enemies.Add(new Alien(36,10,ConsoleColor.Red));

            List<Bullet> bullets = new List<Bullet>();

            StartGame(player, enemies, bullets);

            

        }

        static void Title()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("SpicyInvader II");
            Console.WriteLine("---------------");           
        }
        static void Language(int language)
        {
            int option = 0;
            bool validOption =false;
            if (language == 0) //Français
            {
                Console.WriteLine("\n1.Jouer     ");
                Console.WriteLine("2.Options   ");
                Console.WriteLine("3.Résultats ");
                Console.WriteLine("4.Quitter   ");
            }
            else
            {
                Console.WriteLine("\n1.Play      ");
                Console.WriteLine("2.Options   ");
                Console.WriteLine("3.Scored    ");
                Console.WriteLine("4.Exit      ");
            }

            Console.WriteLine("\n Your answere : ");

            do
            {
                validOption = int.TryParse(Console.ReadLine(), out option);
                if (!validOption)
                {
                    if (language == 0)
                    {
                        Console.WriteLine("\nVotre valeur n'est pas valide! Merci de réessayer !");
                    }
                    else
                    {
                        Console.WriteLine("Your value is invalid! Please try again !");
                    }
                }
                else
                {
                    if (option == 1)
                    {
                        //StartGame();
                    }
                    else if (option == 2)
                    {

                    }
                    else if (option == 3)
                    {

                    }
                    else if (option == 4)
                    {

                    }
                    else
                    {

                    }
                }
            } while (!validOption&&!(option>0 && option <5));

        }

        static void StartGame(Player player, List<Alien> enemies, List<Bullet> bullets)
        {
            Console.Clear();
            //Title();
            //DrawMapGame();
            //player.Draw();

            Update(player, enemies, bullets);
            //Console.Clear();


            //Dibujar la tabla
            //Initialicer le player et l'enemmie 

        }

        static void Update(Player player, List<Alien> enemies, List<Bullet> bullets)
        {

            int limitMapLeft = 10;
            int limitMapRight = Console.WindowWidth - 20;
            bool changeDirection = false;
            bool goDown = false;

            while (player.isAlive())
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Task limiter le mouvement enemies
                    if (i ==0 && enemies[i].x<limitMapLeft)
                    {
                        goDown = true;
                        changeDirection = false;
                    }
                    else if (i == enemies.Count-1 && enemies[i].x>limitMapRight)
                    {
                        goDown = true;
                        changeDirection=true;
                    }
                }                
                //Task: methode pour le movement des enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (goDown)
                    {
                        enemies[i].y++;
                        if (i == enemies.Count - 1)
                        {
                            goDown = false;
                        }
                    }
                    enemies[i].Move(changeDirection);

                }

                //Task: methode pour le movement du player
                while (Console.KeyAvailable)
                {

                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                    switch (keyPressed.Key)
                        {
                        case ConsoleKey.RightArrow:
                            //Task: limiter le movement
                            if (player.x > limitMapRight)
                            {
                                player.Move(false, false);
                            }
                            else
                            {
                                player.Move(false, true);
                            }
                            break;

                        case ConsoleKey.LeftArrow:
                            if (player.x < limitMapLeft)
                            {

                                player.Move(false, false);
                            }
                            else
                            {
                                player.Move(true, false);
                            }
                            break;
                        case ConsoleKey.Spacebar:                            
                            bullets.Add(new Bullet(player.x + 6, player.y - 4, ConsoleColor.Yellow));
                            break;
                        default:
                            player.Move(false, false);
                            break;
                        }
                }


                //Task: trayectoir bullet
                if (bullets.Count != 0)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bullets[i].Move();

                        if (bullets[i].y < 4)
                        {
                            bullets[i].alive = false;
                        }
                    }
                }

                //DrawMapGame();

                Console.Clear();

                player.Draw();
                for (int i=0;i<enemies.Count;i++)
                {
                    if (enemies[i].alive)
                    {
                        enemies[i].Draw();

                    }
                    else
                    {
                        //enemies.RemoveAt(i);
                    }

                }
                for (int i=0;i<bullets.Count;i++)
                {

                    collisionsCheck(bullets[i], enemies);
                    if (bullets[i].alive)
                    {
                        bullets[i].Draw();
                    }
                    else
                    {
                        //bullets.RemoveAt(i);
                    }
                    //if (collisionsCheck(bullets[i], enemies))
                    //{
                    //    bullets[i].alive = false;

                    //}
                }
                
                Thread.Sleep(50);
            }
        }
        static void DrawMapGame ()
        {
            int longeur = 50;
            Console.WriteLine("\t╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            
            for (int i = 0; i<=longeur; i++)
            {
                Console.WriteLine("\t║                                                                                                                     ║");
            }
            Console.WriteLine("\t╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        //Task: methode pour le movement
        static void playerController(Player player)
        {

            int limitMapLeft = 10;
            int limitMapRight = Console.WindowWidth-20;
            do
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(false);
                //Task: keys for movement
                switch (keyPressed.Key)
                {
                    case ConsoleKey.RightArrow:
                        //Task: limiter le movement
                        if (player.x > limitMapRight)
                        {
                            player.Move(false, false);
                        }
                        else
                        {
                            player.Move(false, true);
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (player.x < limitMapLeft)
                        {

                            player.Move(false,false);
                        }
                        else
                        {
                            player.Move(true, false);
                        }
                        break;
                }
                
            } while (Console.KeyAvailable);

        }




        static void collisionsCheck(Bullet bullet, List<Alien> aliens)
        {
            bool collision = false;
            bool collisionX = false;
            bool collisionY = false;

            for (int i = 0; i < 10; i++)
            {
                for (int e = 0; e < 11; e++)
                {

                        if (aliens[i].x + e == bullet.x)
                        {
                            aliens[i].collisionX = true;
                        }                       
                }

                for (int d = 0; d <5;d++)
                {
                    if (aliens[i].y + d == bullet.y)
                    {
                        aliens[i].collisionY = true;
                    }
                }
                if (aliens[i].collisionX == true && aliens[i].collisionY == true)
                {
                    //collision = true;
                    aliens[i].alive = false;
                }

            }
            
            
        }
    }
    
}
