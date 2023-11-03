using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpicyInvaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvaders.Tests
{
    [TestClass()]
    public class ObjectBaseTests
    {
        /// <summary>
        /// UnitTesting pour verifier si l'object il est mort
        /// </summary>
        [TestMethod()]
        public void _IsAliveTest()
        {
            //Arrange
            Player player = new Player(0, 0, ConsoleColor.Red, 1, 1);
            Player player1 = new Player(0, 0, ConsoleColor.Red, 1, 0);


            //Assert
            Assert.IsTrue(player._IsAlive() == true);
            Assert.IsTrue(player1._IsAlive() == false);
        }

        /// <summary>
        /// UnitTesting pour verifier si l'object est dans la console
        /// </summary>
        [TestMethod()]
        public void _OutLimitWindowYCheckTest()
        {
            //Arrange
            Bullet bullet = new Bullet(0, 0, ConsoleColor.Red, true, 0, 1);
            Bullet bullet1 = new Bullet(0, 0, ConsoleColor.Red, true, 0, 1);
            Bullet bullet2 = new Bullet(0, 0, ConsoleColor.Red, true, 0, 1);

            //Act
            bullet._y = -3;
            bullet1._y = 1000;
            bullet2._y = 10;

            //Assert
            Assert.IsTrue(bullet._OutLimitWindowYCheck(100) == true);
            Assert.IsTrue(bullet1._OutLimitWindowYCheck(100) == true);
            Assert.IsTrue(bullet2._OutLimitWindowYCheck(100) == false);
        }
    }
}