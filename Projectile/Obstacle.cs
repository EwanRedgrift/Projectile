using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projectile
{
    internal class Obstacle
    {
        public Rectangle hitbox;

        public Obstacle(int x, int y, int width, int height)
        {
            hitbox = new Rectangle(x, y, width, height);
        }

        public bool CheckCollision(int projX, int projY, int projSize)
        {
            Rectangle projRect = new Rectangle(projX, projY, projSize, projSize);
            return hitbox.IntersectsWith(projRect);
        }
    }
}
