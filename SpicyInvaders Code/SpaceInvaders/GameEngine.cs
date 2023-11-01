using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders
{
    internal class GameEngine
    {
        public GameEngine()
        {
            Title();
        }

        static void Title()
        {
            Console.CursorVisible = false;
            Console.Clear();


            Console.WriteLine("------------");
            Console.WriteLine("SpicyInvader");
            Console.WriteLine("------------\n");

            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Highscore");
            Console.WriteLine("3. Controls Game");
            Console.WriteLine("4. Exit");

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
            }
        }

        public static void StartGame()
        {
            Console.WriteLine("\nWrite your Nickname: ");
            string nickName = Console.ReadLine();

            Console.Clear();

            int totalEnemies = 10;

            int originLimitX = 10;
            int originLimitY = 5;

            int spaceBetweenEnemies = 13;

            Player player = new Player(originLimitX, Console.WindowHeight - 6, ConsoleColor.Green);
            player.nickName = nickName;
            List<Alien> enemies = new List<Alien>();
            List<Bullet> bullets = new List<Bullet>();
            Score score = new Score(0);

            for (int i = 0; i < totalEnemies; i++)
            {
                enemies.Add(new Alien(originLimitX + (i * spaceBetweenEnemies), originLimitY, ConsoleColor.Red));
            }

            Update(player, enemies, bullets, score);
        }

        static void Update(Player player, List<Alien> enemies, List<Bullet> bullets ,Score score)
        {

            int limitMapLeft = 10;
            int limitMapRight = Console.WindowWidth - 20;
            int respawnBulletTime = 5;

            while (player.isAlive && enemies.Count != 0)
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

            EndGame(player, enemies ,score);
        }

        static void EndGame(Player player, List<Alien> enemie, Score score)
        {
            Console.Clear();
            DataBaseConnect databaseConnection = new DataBaseConnect();
            databaseConnection.SavePoints(player.nickName, score.points);

            HudGameDraw(player, score);

            if (!player.isAlive)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Your Lose");
                Console.WriteLine("Score : " + score.points);
                Console.WriteLine("\n1 - PlayAgain");
                Console.WriteLine("2 - Menu");
                Console.WriteLine("3 - Exit");
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
                }
                Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Your Win");
                Console.WriteLine("Score : " + score.points);
                Console.WriteLine("\n1 - PlayAgain");
                Console.WriteLine("2 - Menu");
                Console.WriteLine("3 - Exit");
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
                }
                Console.Clear();
            }

        }

        static void PlayerControll(Player player, int limitMapRight, int limitMapLeft, List<Bullet> bullets)
        {
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
                        if (player.canAttack)
                        {
                            bullets.Add(new Bullet(player.x + 6, player.y, ConsoleColor.Yellow, true));
                            player.canAttack = false;
                        }
                        break;
                    default:
                        player.Move(false, false);
                        break;
                }
            }
        }

        static void EnemiesMovement(List<Alien> enemies, int limitMapLeft, int limitMapRight)
        {
            if (enemies[0].x < limitMapLeft)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].moveLeft = false;
                    enemies[i].moveDown = true;
                }
            }

            if (enemies[enemies.Count - 1].x > limitMapRight)
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
                    enemies[i].y++;
                    enemies[i].moveDown = false;
                }
            }
        }

        static void BulletMovement(List<Bullet> bullets)
        {
            if (bullets.Count != 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Move();

                    if (bullets[i].y < 4 || bullets[i].y > Console.WindowHeight - 2)
                    {
                        bullets[i].alive = false;
                    }
                }
            }
        }

        static void EnemiesAtack(List<Alien> enemies, List<Bullet> bullets)
        {
            Random rnd = new Random();
            int rndEnemey = rnd.Next(0, enemies.Count);
            if (enemies.Count != 0) { bullets.Add(new Bullet(enemies[rndEnemey].x + enemies[rndEnemey].assetLimitX / 2, enemies[rndEnemey].y + enemies[rndEnemey].assetLimitY, ConsoleColor.DarkRed, false)); }
        }

        static void CollisionSystem(List<Alien> enemies, List<Bullet> bullets, Player player, Score score)
        {

            for (int e = 0; e < bullets.Count; e++)
            {

                if (bullets[e].fromPlayer)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if ((bullets[e].y >= enemies[i].y && bullets[e].y <= enemies[i].y + 5 && bullets[e].x >= enemies[i].x && bullets[e].x <= enemies[i].x + 10) && bullets.Count != 0 && enemies.Count != 0)
                        {
                            score.points += score.pointsForEnemy;
                            if (enemies[i].life != 1)
                            {

                                enemies[i].life--;
                            }
                            else
                            {
                                enemies.RemoveAt(i);
                            }

                            bullets.RemoveAt(e);

                        }
                    }
                }
                else
                {
                    if (bullets[e].y >= player.y && bullets[e].y <= player.dimensionY + player.y && bullets[e].x >= player.x && bullets[e].x <= player.x + player.dimensionX)
                    {
                        score.points -= score.pointsForDmg;

                        if (player.life != 1)
                        {
                            player.life--;
                        }
                        else
                        {
                            player.isAlive = false;
                        }
                        bullets.RemoveAt(e);
                    }
                }
            }
        }


        static void EnemiesDraw(List<Alien> enemies)
        {
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
        }
        static void BulletsDraw(List<Bullet> bullets)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].alive)
                {
                    bullets[i].Draw();
                }
                else
                {
                    bullets.RemoveAt(i);
                }
            }
        }
        static void HudGameDraw(Player player, Score score)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Player : " + player.nickName);
            Console.WriteLine("{0} LIVES : " + player.life);
            Console.WriteLine("SCORE : " + score.points);
        }

        static void ControlMenu()
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

        static void HighScoreMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            DataBaseConnect databaseConnection = new DataBaseConnect();

            Console.WriteLine("------------------------");
            Console.WriteLine("SpicyInvader - Highscore");
            Console.WriteLine("------------------------\n");
            databaseConnection.Highscore();
            Console.Write("1# - NickName: {0} Points: {1}", databaseConnection.playersName[0], databaseConnection.playersScore[0]);
            Console.Write("\n2# - NickName: {0} Points: {1}", databaseConnection.playersName[1], databaseConnection.playersScore[1]);
            Console.Write("\n3# - NickName: {0} Points: {1}", databaseConnection.playersName[2], databaseConnection.playersScore[2]);
            Console.Write("\n4# - NickName: {0} Points: {1}", databaseConnection.playersName[3], databaseConnection.playersScore[3]);
            Console.Write("\n5# - NickName: {0} Points: {1}", databaseConnection.playersName[4], databaseConnection.playersScore[4]);

            Console.WriteLine("\n\nPress any key to back to menu");

            Console.ReadKey();
            Console.Clear();
            Title();

        }
    }
}
