using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projectile
{
    internal class Player
    {
        public int Health = 100;

        public int width = 25;
        public int height = 100;

        public int xCoords;
        public int yCoords;

        public int slideVelocity;
        public bool isSliding;
    }
}
