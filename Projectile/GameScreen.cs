using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//By Garrett and Ewan


namespace Projectile
{
    public partial class GameScreen : UserControl
    {
        bool gameOver = false;
        string winner;

        string turn = "player1";

        Player player1 = new Player();
        Player player2 = new Player();

        projectile projectile = new projectile();

        Random randGen = new Random();

        Brush redBrush = Brushes.Red;
        Brush blackBrush = Brushes.Black;
        Brush blueBrush = Brushes.Blue;
        Brush greenBrush = Brushes.Green;
        Brush greyBrush = Brushes.Gray;

        Rectangle ground = new Rectangle();

        Obstacle rock;

        bool isDragging = false;
        Point dragStart;
        Point currentMouse;

        public GameScreen()
        {
            InitializeComponent();

            ground.Y = this.Height - 50;
            ground.Height = 50;

            ground.Width = this.Width;

            player1.xCoords = this.Width / 2 - 200;
            player1.yCoords = this.Height - player1.height;

            player2.xCoords = this.Width / 2 + 200;
            player2.yCoords = this.Height - player2.height;

            projectile.ownership = "player1";

            projectile.xCoords = player1.xCoords + player1.width / 2 - 10; // projectile size is 20x20
            projectile.yCoords = player1.yCoords + player1.height / 2 - 10;


            rock = new Obstacle(this.Width / 2, this.Height - 55, 40, 60);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player1.isSliding)
            {
                player1.xCoords += player1.slideVelocity;

                if (Math.Abs(player1.slideVelocity) > 0)
                {
                    player1.slideVelocity = (int)(player1.slideVelocity * 0.8); // slow down
                    if (Math.Abs(player1.slideVelocity) < 1)
                    {
                        player1.slideVelocity = 0;
                        player1.isSliding = false;
                        SwitchTurn();
                    }
                }
            }

            if (player2.isSliding)
            {
                player2.xCoords += player2.slideVelocity;

                if (Math.Abs(player2.slideVelocity) > 0)
                {
                    player2.slideVelocity = (int)(player2.slideVelocity * 0.8);
                    if (Math.Abs(player2.slideVelocity) < 1)
                    {
                        player2.slideVelocity = 0;
                        player2.isSliding = false;
                        SwitchTurn();
                    }
                }
            }

            bool landed = projectile.fly();

            Player opponent;
            if (turn == "player1")
            {
                opponent = player2;
            }
            else
            {
                opponent = player1;
            }

            if (projectile.isLaunched && projectile.CheckCollision(opponent))
            {
                int damage = projectile.GetVelocityMagnitude();
                opponent.Health -= damage;

                if (opponent.Health <= 0)
                {
                    gameOver = true;
                    winner = turn;
                    projectile.isLaunched = false;
                    return;
                }

                // Begin sliding
                int slideSpeed = damage / 4;
                if (turn == "player1")
                {
                    player2.slideVelocity = slideSpeed;
                    player2.isSliding = true;
                }
                else
                {
                    player1.slideVelocity = -slideSpeed;
                    player1.isSliding = true;
                }

                projectile.isLaunched = false;
                landed = true;
            }


            if (opponent.Health <= 0)
            {
                gameOver = true;
                winner = turn;
                projectile.isLaunched = false;
            }

            if (landed && !player1.isSliding && !player2.isSliding)
            {
                SwitchTurn();
            }

            Refresh();
        }


        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {            
            
            e.Graphics.FillRectangle(greenBrush, ground);

            e.Graphics.FillRectangle(redBrush, player1.xCoords, player1.yCoords, player1.width, player1.height);

            e.Graphics.FillRectangle(blueBrush, player2.xCoords, player2.yCoords, player2.width, player2.height);

            DrawHealthBar(e.Graphics, player1);

            DrawHealthBar(e.Graphics, player2);

            e.Graphics.FillEllipse(greyBrush, rock.hitbox);

            if (gameOver)
            {
                string message = winner.ToUpper() + " WINS!";
                Font font = new Font("Arial", 32, FontStyle.Bold);
                SizeF textSize = e.Graphics.MeasureString(message, font);

                float x = (this.Width - textSize.Width) / 2;
                float y = (this.Height - textSize.Height) / 2;

                e.Graphics.DrawString(message, font, Brushes.Gold, x, y);

                return;
            }

            if (isDragging) // If dragging, draw the projectile at the mouse location
            {
                Point mousePos = this.PointToClient(Cursor.Position);
                e.Graphics.FillRectangle(blackBrush, mousePos.X - 10, mousePos.Y - 10, 20, 20);

                int dx = dragStart.X - mousePos.X;
                int dy = dragStart.Y - mousePos.Y;

                double distance = Math.Sqrt(dx * dx + dy * dy);
                double angle = -(Math.Atan2(dy, dx)); // In radians
                int maxPower = 35;
                double power = Math.Min(distance, maxPower);

                double xVel = Math.Cos(angle) * power;
                double yVel = -Math.Sin(angle) * power;

                double x = projectile.xCoords;
                double y = projectile.yCoords;

                for (int i = 0; i < 5; i++)
                {
                    x += xVel * 0.2;
                    y += yVel * 0.2;
                    yVel += 1 * 0.2; // Gravity

                    e.Graphics.FillEllipse(Brushes.Gray, (float)x + 10 - 3, (float)y + 10 - 3, 6, 6); // small dots
                }
            }
            else
            {
                e.Graphics.FillRectangle(blackBrush, projectile.xCoords, projectile.yCoords, 20, 20);
            }
        }

        private void GameScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (!projectile.isLaunched)
            {
                isDragging = true;
                dragStart = new Point(projectile.xCoords + 10, projectile.yCoords + 10); // center of projectile
                currentMouse = e.Location;
            }
        }

        private void GameScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && !projectile.isLaunched)
            {
                isDragging = false;
                currentMouse = e.Location;

                int dx = dragStart.X - currentMouse.X;
                int dy = dragStart.Y - currentMouse.Y;

                double distance = Math.Sqrt(dx * dx + dy * dy);
                double angle = -(Math.Atan2(dy, dx) * 180 / Math.PI);

                int maxPower = 35;
                int power = (int)Math.Min(distance, maxPower);

                switch (turn)
                {
                    case "player1":
                        projectile.release(player1, power, angle);
                        break;
                    case "player2":
                        projectile.release(player2, power, angle);
                        break;
                }
            }
        }

        private void SwitchTurn()
        {
            if (turn == "player1")
            {
                turn = "player2";
                projectile.ownership = "player2";
                projectile.xCoords = player2.xCoords + player2.width / 2 - 10;
                projectile.yCoords = player2.yCoords + player2.height / 2 - 10;
            }
            else
            {
                turn = "player1";
                projectile.ownership = "player1";
                projectile.xCoords = player1.xCoords + player1.width / 2 - 10;
                projectile.yCoords = player1.yCoords + player1.height / 2 - 10;
            }
        }

        private void DrawHealthBar(Graphics g, Player player)
        {
            int barWidth = 50;
            int barHeight = 10;
            int padding = 5;

            int barX = player.xCoords + player.width / 2 - barWidth / 2;
            int barY = player.yCoords - barHeight - padding;

            g.FillRectangle(Brushes.DarkGray, barX, barY, barWidth, barHeight);
            int health = Math.Max(0, Math.Min(100, player.Health));
            int red = (int)(255 * (1 - health / 100.0));
            int green = (int)(255 * (health / 100.0));

            Brush healthBrush = new SolidBrush(Color.FromArgb(red, green, 0)); //Should be able to transition from green to red?

            int filledWidth = (int)(barWidth * (health / 100.0));
            g.FillRectangle(healthBrush, barX, barY, filledWidth, barHeight);
        }


    }
}
