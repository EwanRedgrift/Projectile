using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projectile
{
    public partial class MenuScreen : UserControl
    {
        public MenuScreen()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new GameScreen());
        }

        private void instructionButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new InstructionScreen());
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
