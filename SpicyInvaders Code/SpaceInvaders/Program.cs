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

//Limitar la consola OK
//Limitar el movimiento del jugador y corregir OK
//Crear enemigos OK
//Movimiento de enemigos OK
//Disparos OK
//Collisiones NO!
//Muerte enemigos y jugador OK
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


            //StartGame(player, enemies, bullets);



        }

        static void Title()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("SpicyInvader III");
            Console.WriteLine("---------------");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("1. Start");
            Console.WriteLine("2. Highscore");
            Console.WriteLine("3. Options");
            Console.WriteLine("4. Quitter");

            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            switch (keyPressed.Key)
            {
                case ConsoleKey.Enter:
                    StartGame();
                    break;
            }

            static void Language(int language)
            {
                int option = 0;
                bool validOption = false;
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
                } while (!validOption && !(option > 0 && option < 5));

            }

            static void StartGame()
            {
                Console.Clear();
                Player player = new Player();

                List<Alien> enemies = new List<Alien>();
                for (int i = 0; i < 10; i++)
                {
                    enemies.Add(new Alien(10 + (i * 13), 10, ConsoleColor.Red));

                }
                //enemies.Add(new Alien(10,10,ConsoleColor.Red));
                //enemies.Add(new Alien(23,10,ConsoleColor.Red));
                //enemies.Add(new Alien(36,10,ConsoleColor.Red));

                List<Bullet> bullets = new List<Bullet>();


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
                    


                    Console.CursorVisible = false;


                    for (int i = 0; i < enemies.Count; i++)
                    {
                        //Task limiter le mouvement enemies
                        if (i == 0 && enemies[i].x < limitMapLeft)
                        {
                            goDown = true;
                            changeDirection = false;
                        }
                        else if (i == enemies.Count - 1 && enemies[i].x > limitMapRight)
                        {
                            goDown = true;
                            changeDirection = true;
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
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].alive)
                        {
                            enemies[i].Draw();
                        }
                        else
                        {
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        //collisionsCheck(bullets[i], enemies);
                        if (bullets[i].alive)
                        {
                            bullets[i].Draw();
                        }
                        else
                        {
                            bullets.RemoveAt(i);
                        }
                        //if (collisionsCheck(bullets[i], enemies))
                        //{
                        //    bullets[i].alive = false;
                        //}
                    }

                    //BIEN PERO HAY QUE QUITAR QUE CORRIGA TODOS LAS BALAS

                    for (int i = 0; i < enemies.Count; i++)
                    {
                        for (int e = 0; e < bullets.Count; e++)
                        {

                            if (enemies[i].collisionX != true || enemies[i].collisionY != true)
                            {
                                for (int f = 0; f < 11; f++)
                                {

                                    if (enemies[i].x + f == bullets[e].x)
                                    {
                                        enemies[i].collisionX = true;
                                        f = 11;

                                    }

                                }

                                for (int d = 0; d < 5; d++)
                                {
                                    if (enemies[i].y + d == bullets[e].y)
                                    {
                                        enemies[i].collisionY = true;
                                        d = 5;
                                    }
                                }

                                //collision = true;
                                //enemies[i].alive = false;

                            }
                            else
                            {
                                Debug.Print("Enemy X: " + enemies[i].x + " BUlletX  " + bullets[e].x + enemies[i].collisionX);
                                Debug.Print("Enemy Y: " + enemies[i].y + " BUlletY  " + bullets[e].y + enemies[i].collisionY);
                                enemies.RemoveAt(i);
                                bullets.RemoveAt(e);
                            }
                        }

                    }

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Life: 1  Score: 000 ");

                    Thread.Sleep(50);
                }
            }
            static void DrawMapGame()
            {
                int longeur = 50;
                Console.WriteLine("\t╔═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");

                for (int i = 0; i <= longeur; i++)
                {
                    Console.WriteLine("\t║                                                                                                                     ║");
                }
                Console.WriteLine("\t╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            }

            //Task: methode pour le movement
            static void playerController(Player player)
            {

                int limitMapLeft = 10;
                int limitMapRight = Console.WindowWidth - 20;
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

                                player.Move(false, false);
                            }
                            else
                            {
                                player.Move(true, false);
                            }
                            break;
                    }

                } while (Console.KeyAvailable);

            }




            //static void collisionsCheck(Bullet bullet, List<Alien> aliens)
            //{
            //    //bool collision = false;
            //    //bool collisionX = false;
            //    //bool collisionY = false;

            //    for (int i = 0; i < aliens.Count; i++)
            //    {
            //        for (int e = 0; e < 11; e++)
            //        {

            //                if (aliens[i].x + e == bullet.x)
            //                {
            //                    aliens[i].collisionX = true;
            //                }                       
            //        }

            //        for (int d = 0; d <5;d++)
            //        {
            //            if (aliens[i].y + d == bullet.y)
            //            {
            //                aliens[i].collisionY = true;
            //            }
            //        }
            //        if (aliens[i].collisionX == true && aliens[i].collisionY == true)
            //        {
            //            //collision = true;
            //            aliens[i].alive = false;
            //        }

            //    }


            //}
            //static void collisionsCheck(Bullet bullet, Alien alien)
            //{
            //    //bool collision = false;
            //    //bool collisionX = false;
            //    //bool collisionY = false;


            //        for (int e = 0; e < 11; e++)
            //        {

            //            if (alien.x + e == bullet.x)
            //            {
            //                alien.collisionX = true;
            //            }
            //        }

            //        for (int d = 0; d < 5; d++)
            //        {
            //            if (alien.y + d == bullet.y)
            //            {
            //                alien.collisionY = true;
            //            }
            //        }
            //        if (alien.collisionX == true && alien.collisionY == true)
            //        {
            //            //collision = true;
            //            alien.alive = false;
            //        }

            //    }


            //}
        }
    }
}
