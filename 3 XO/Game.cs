using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_XO
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }



        private void Game_Load(object sender, EventArgs e)
        {
            //load meu
        }

        private void MBStart_Click(object sender, EventArgs e)
        {
            DisableMenu();
            GameStart();
        }

        private void MBSettings_Click(object sender, EventArgs e)
        {
            DisableMenu();
            EnableSettings();
        }

        private void SettingsApply_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            DisableSettings();
            EnableMenu();
        }


        private void MBAbout_Click(object sender, EventArgs e)
        {

        }

        private void MBExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Escape) { EnableMenu(); }



            else if (e.KeyValue == (char)Keys.Up) {/*moove field*/ }
            else if (e.KeyValue == (char)Keys.Down) {/*moove field*/ }
            else if (e.KeyValue == (char)Keys.Left) {/*moove field*/ }
            else if (e.KeyValue == (char)Keys.Right) {/*moove field*/ }
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            if (GameStarted)
            {
                Clean(sender, e);
                DrawField(sender, e);
                DrawX(sender, e);
                DrawO(sender, e);
                if (ShowHints){
                    DrawForce(sender, e);
                }
            }
        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            Invalidate();
        }

        private void Game_MouseClick(object sender, MouseEventArgs e)
        {
            GameStep(new Vector2(e.X, e.Y));
            Invalidate();
        }

        private void vsComputer_CheckedChanged(object sender, EventArgs e)
        {
            vsAI = !vsAI;
        }
    }
}
