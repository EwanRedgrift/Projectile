using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projectile
{
    internal class projectile
    {
        public string ownership;
        public int xCoords;
        public int yCoords;

        public int xVelocity;
        public int yVelocity;

        public bool isLaunched;




        public void release(Player player, int drawPower, double drawAngle)
        {
            isLaunched = true;

            double drawAngleRad = drawAngle * (Math.PI / 180);

            double centerX = player.xCoords + player.width / 2.0;
            double centerY = player.yCoords + player.height / 2.0;

            int radius = 20;

            switch (ownership)
            {
                case "player1":
                    xVelocity = (int)Math.Round(Math.Cos(drawAngleRad) * drawPower);
                    
                    xCoords = (int)Math.Round(centerX + Math.Cos(drawAngleRad) * radius);
                    yCoords = (int)Math.Round(centerY + Math.Sin(drawAngleRad) * radius);
                    break;
                case "player2":
                    xVelocity = -(int)Math.Round(Math.Cos(drawAngleRad) * drawPower);

                    xCoords = (int)Math.Round(centerX + Math.Cos(drawAngleRad) * radius);
                    yCoords = (int)Math.Round(centerY + Math.Sin(drawAngleRad) * radius);
                    break;
            }

            yVelocity = -(int)Math.Round(Math.Sin(drawAngleRad) * drawPower);
        }

        public bool fly()
        {
            if (!isLaunched)
            {
                return false;
            }

            yVelocity += 1; // gravity

            // Predict the next y position
            int nextY = yCoords + yVelocity;

            // Check for ground collision
            if (nextY + 20 < 500) // assuming 20 is the radius and 500 is the ground level
            {
                yCoords = nextY;

                // Only move horizontally if still flying
                if (ownership == "player1")
                {
                    xCoords += xVelocity;
                }
                else if (ownership == "player2")
                {
                    xCoords -= xVelocity;
                }

                return false;
            }
            else
            {
                yCoords = 500 - 20; // Snap to ground
                xVelocity = 0;
                yVelocity = 0;
                isLaunched = false;
                return true;
            }
        }

        public bool CheckCollision(Player opponent)
        {
            Rectangle projRect = new Rectangle(xCoords, yCoords, 20, 20);
            Rectangle playerRect = new Rectangle(opponent.xCoords, opponent.yCoords, opponent.width, opponent.height);

            return projRect.IntersectsWith(playerRect);
        }

        public int GetVelocityMagnitude()
        {
            return (int)Math.Sqrt(xVelocity * xVelocity + yVelocity * yVelocity);
        }

    }
}
