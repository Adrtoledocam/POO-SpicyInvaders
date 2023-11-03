using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    public class GameEngine
    {
        public GameEngine()
        {
            Title();
        }

        /// <summary>
        /// Menu principal du jeu 
        /// </summary>
        public static void Title()
        {
            Console.CursorVisible = false;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------");
            Console.WriteLine("SpicyInvader");
            Console.WriteLine("------------\n");

            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Highscore");
            Console.WriteLine("3. Controls Game");
            Console.WriteLine("4. Exit");

            Console.WriteLine("\n\nPress your number option");


            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                    StartGame();
                    break;
                case ConsoleKey.D2:
                    HighScoreMenu();
                    break;
                case ConsoleKey.D3:
                    ControlMenu();
                    break;
                case ConsoleKey.D4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Title();
                    break;
            }
        }
        /// <summary>
        /// Initialisation du jeu avec les objets et l'interface
        /// </summary>
        public static void StartGame()
        {
            Console.WriteLine("\nWrite your Nickname: ");
            string nickName = Console.ReadLine();

            Console.Clear();

            int totalEnemies = 10;

            int originLimitX = 10;
            int originLimitY = 5;

            int spaceBetweenEnemies = 13;

            Player player = new Player(originLimitX, Console.WindowHeight - 6, ConsoleColor.Green,4,3);
            player.nickName = nickName;
            List<Alien> enemies = new List<Alien>();
            List<Bullet> bullets = new List<Bullet>();
            Score score = new Score(0);

            for (int i = 0; i < totalEnemies; i++)
            {
                enemies.Add(new Alien(originLimitX + (i * spaceBetweenEnemies), originLimitY, ConsoleColor.Red,2,3));
            }

            Update(player, enemies, bullets, score);
        }

        /// <summary>
        /// Methodo qui met constamment à jour le jeu pour son gameplay.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemies"></param>
        /// <param name="bullets"></param>
        /// <param name="score"></param>
        public static void Update(Player player, List<Alien> enemies, List<Bullet> bullets ,Score score)
        {

            int limitMapLeft = 10;
            int limitMapRight = Console.BufferWidth - 20;
            int respawnBulletTime = 6;

            while (player._IsAlive() && enemies.Count != 0)
            {
                EnemiesMovement(enemies, limitMapLeft, limitMapRight);
                PlayerControll(player, limitMapRight, limitMapLeft, bullets);
                BulletMovement(bullets);
                CollisionSystem(enemies, bullets, player ,score);

                if (respawnBulletTime == 0)
                {
                    EnemiesAtack(enemies, bullets);
                    player.canAttack = true;
                    respawnBulletTime = 5;
                }
                else
                {
                    respawnBulletTime--;
                }

                Console.Clear();

                EnemiesDraw(enemies);
                player.Draw();
                BulletsDraw(bullets);
                HudGameDraw(player, score);

                Thread.Sleep(100);
            }

            EndGame(player ,score, true);
        }
        /// <summary>
        /// Methodo qui affiche le menu de fin de partie avec les données de la partie.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="score"></param>
        /// <param name="savePoints"></param>
        public static void EndGame(Player player, Score score, bool savePoints)
        {
            if(savePoints)
            {
                try
                {
                    Console.Clear();
                    DataBaseConnect databaseConnection = new DataBaseConnect();
                    databaseConnection.SavePoints(player.nickName, score.points);
                }
                catch
                {
                }
            }            

            if (!player._IsAlive())
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Your Lose " + player.nickName);
                Console.WriteLine("Score : " + score.points);
                Console.WriteLine("\n1 - PlayAgain");
                Console.WriteLine("2 - Menu");
                Console.WriteLine("3 - Exit");
                Console.WriteLine("\nPress your number option");
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        StartGame();

                        break;
                    case ConsoleKey.D2:
                        Title();
                        break;
                    case ConsoleKey.D3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        EndGame(player, score, false);
                        break;
                }
                Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Your Win "+player.nickName);
                Console.WriteLine("Score : " + score.points);
                Console.WriteLine("\n1 - PlayAgain");
                Console.WriteLine("2 - Menu");
                Console.WriteLine("3 - Exit");
                Console.WriteLine("\n\nPress your number option");

                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                        StartGame();
                        break;
                    case ConsoleKey.D2:
                        Title();
                        break;
                    case ConsoleKey.D3:
                        Environment.Exit(0);
                        Console.Clear();
                        EndGame(player, score, false);
                        break;
                    default:
                        Console.Clear();
                        EndGame(player, score, false);
                        break;
                }
                Console.Clear();
            }

        }
        /// <summary>
        /// Methodo qui gère le contrôle du joueur avec son navire en fonction des touches.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="limitMapRight"></param>
        /// <param name="limitMapLeft"></param>
        /// <param name="bullets"></param>
        public static void PlayerControll(Player player, int limitMapRight, int limitMapLeft, List<Bullet> bullets)
        {
            while (Console.KeyAvailable)
            {

                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                switch (keyPressed.Key)
                {
                    case ConsoleKey.RightArrow:
                        //Task: limiter le movement
                        if (player._x > limitMapRight)
                        {
                            player.Move(false, false);
                        }
                        else
                        {
                            player.Move(false, true);
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (player._x < limitMapLeft)
                        {

                            player.Move(false, false);
                        }
                        else
                        {
                            player.Move(true, false);
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        if (player.canAttack)
                        {
                            bullets.Add(new Bullet(player._x + 6, player._y, ConsoleColor.Yellow, true, 3, 1));
                            player.canAttack = false;
                        }
                        break;
                    default:
                        player.Move(false, false);
                        break;
                }
            }
        }
        /// <summary>
        /// Methodo qui gère le mouvement de tous les ennemis à l'écran.        
        /// /// </summary>
        /// <param name="enemies"></param>
        /// <param name="limitMapLeft"></param>
        /// <param name="limitMapRight"></param>
        public static void EnemiesMovement(List<Alien> enemies, int limitMapLeft, int limitMapRight)
        {
            if (enemies[0]._x < limitMapLeft)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].moveLeft = false;
                    enemies[i].moveDown = true;
                }
            }

            if (enemies[enemies.Count - 1]._x > limitMapRight)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].moveLeft = true;
                    enemies[i].moveDown = true;
                }
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Move();

                if (enemies[i].moveDown)
                {
                    enemies[i]._y++;
                    enemies[i].moveDown = false;
                }
            }
        }
        /// <summary>
        /// Methodo qui gère le mouvement de tous les bullets à l'écran.        
        /// </summary>
        /// <param name="bullets"></param>
        public static void BulletMovement(List<Bullet> bullets)
        {
            if (bullets.Count != 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Move();

                    if (bullets[i]._y < 4 || bullets[i]._y > Console.BufferHeight - 2)
                    {
                        bullets[i]._life=0;
                        bullets.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Methodo qui gère l'ataque de tous les enemies avec des bullets
        /// </summary>
        /// <param name="bullets"></param>
        public static void EnemiesAtack(List<Alien> enemies, List<Bullet> bullets)
        {
            Random rnd = new Random();
            int rndEnemey = rnd.Next(0, enemies.Count);
            if (enemies.Count != 0) { bullets.Add(new Bullet(enemies[rndEnemey]._x + enemies[rndEnemey].assetLimitX / 2, enemies[rndEnemey]._y + enemies[rndEnemey].assetLimitY, ConsoleColor.DarkRed, false, 1,1)); }
        }

        /// <summary>
        /// Methodo qui contrôle les collisions du jeu et sa réponse.
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="bullets"></param>
        /// <param name="player"></param>
        /// <param name="score"></param>
        public static void CollisionSystem(List<Alien> enemies, List<Bullet> bullets, Player player, Score score)
        {

            for (int e = 0; e < bullets.Count; e++)
            {

                if (bullets[e].fromPlayer)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if ((bullets[e]._y >= enemies[i]._y && bullets[e]._y <= enemies[i]._y + 5 && bullets[e]._x >= enemies[i]._x && bullets[e]._x <= enemies[i]._x + 10) && bullets.Count != 0 && enemies.Count != 0)
                        {
                            score.points += score.pointsForEnemy;

                            enemies[i]._life--;

                            if (!enemies[i]._IsAlive())
                            {
                                enemies.RemoveAt(i);

                            }

                            bullets.RemoveAt(e);

                        }
                    }
                }
                else
                {
                    if (bullets[e]._y >= player._y && bullets[e]._y <= player.dimensionY + player._y && bullets[e]._x >= player._x && bullets[e]._x <= player._x + player.dimensionX)
                    {
                        score.points -= score.pointsForDmg;

                        
                            player._life--;
                        
                        bullets.RemoveAt(e);
                    }
                }
            }
        }

        /// <summary>
        /// Methodo pour afficher tous les ennemis dans le jeu
        /// </summary>
        /// <param name="enemies"></param>
        public static void EnemiesDraw(List<Alien> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i]._IsAlive())
                {
                    enemies[i].Draw();
                }
                else
                {
                    enemies.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Methodo pour afficher tous les bullets dans le jeu
        /// </summary>
        /// <param name="bullets"></param>
        public static void BulletsDraw(List<Bullet> bullets)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i]._IsAlive())
                {
                    bullets[i].Draw();
                }
                else
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Methodo pour afficher l'interface du jeu 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="score"></param>
        public static void HudGameDraw(Player player, Score score)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Player : " + player.nickName);
            Console.WriteLine("LIVES : " + player._life);
            Console.WriteLine("SCORE : " + score.points);
        }

        /// <summary>
        /// Methodo pour afficher le menu de controlles du jeu
        /// </summary>
        public static void ControlMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("-----------------------");
            Console.WriteLine("SpicyInvader - Controls");
            Console.WriteLine("-----------------------\n");

            Console.WriteLine("Right Arrow : move to right");
            Console.WriteLine("Left Arrow : move to left");
            Console.WriteLine("Space bar : shoot");
            Console.WriteLine();
            Console.WriteLine("Press any key to back to menu");

            Console.ReadKey();
            Console.Clear();
            Title();
        }
        /// <summary>
        ///Methodo pour afficher le menu de Highscore du jeu
        /// </summary>
        public static void HighScoreMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            DataBaseConnect databaseConnection = new DataBaseConnect();

            Console.WriteLine("------------------------");
            Console.WriteLine("SpicyInvader - Highscore");
            Console.WriteLine("------------------------\n");
            databaseConnection.Highscore();
            try
            {
                Console.Write("1# - NickName: {0} Points: {1}", databaseConnection.playersName[0], databaseConnection.playersScore[0]);
                Console.Write("\n2# - NickName: {0} Points: {1}", databaseConnection.playersName[1], databaseConnection.playersScore[1]);
                Console.Write("\n3# - NickName: {0} Points: {1}", databaseConnection.playersName[2], databaseConnection.playersScore[2]);
                Console.Write("\n4# - NickName: {0} Points: {1}", databaseConnection.playersName[3], databaseConnection.playersScore[3]);
                Console.Write("\n5# - NickName: {0} Points: {1}", databaseConnection.playersName[4], databaseConnection.playersScore[4]);
            }
            catch
            {
                Console.WriteLine("Error in DataBase");
            }
            

            Console.WriteLine("\n\nPress any key to back to menu");

            Console.ReadKey();
            Console.Clear();
            Title();

        }
    }
}
