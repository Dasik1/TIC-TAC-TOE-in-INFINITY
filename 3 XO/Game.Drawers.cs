using _3_XO.Properties;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace _3_XO
{
    partial class Game
    {


        private int BoxSise = 40;

        private void EnableMenu()
        {
            MBGMain.Enabled = true;
            MBGStripe.Enabled = true;
            MButtonStart.Enabled = true;
            MButtonSettings.Enabled = true;
            MButtonAbout.Enabled = true;
            MButtonExit.Enabled = true;

            MBGMain.Visible = true;
            MBGStripe.Visible = true;
            MButtonStart.Visible = true;
            MButtonSettings.Visible = true;
            MButtonAbout.Visible = true;
            MButtonExit.Visible = true;

            this.Cursor = Cursors.Cross;
        }

        private void DisableMenu()
        {
            MBGMain.Enabled = false;
            MBGStripe.Enabled = false;
            MButtonStart.Enabled = false;
            MButtonSettings.Enabled = false;
            MButtonAbout.Enabled = false;
            MButtonExit.Enabled = false;

            MBGMain.Visible = false;
            MBGStripe.Visible = false;
            MButtonStart.Visible = false;
            MButtonSettings.Visible = false;
            MButtonAbout.Visible = false;
            MButtonExit.Visible = false;
        }

        private void EnableSettings()
        {
            MBGMain.Enabled = true;
            SBGStripe.Enabled = true;
            vsComputer.Enabled = true;
            Hints.Enabled = true;
            SettingsApply.Enabled = true;

            MBGMain.Visible = true;
            SBGStripe.Visible = true;
            vsComputer.Visible = true;
            Hints.Visible = true;
            SettingsApply.Visible = true;
        }

        private void DisableSettings()
        {
            MBGMain.Enabled = false;
            SBGStripe.Enabled = false;
            vsComputer.Enabled = false;
            Hints.Enabled = false;
            SettingsApply.Enabled = false;

            MBGMain.Visible = false;
            SBGStripe.Visible = false;
            vsComputer.Visible = false;
            Hints.Visible = false;
            SettingsApply.Visible = false;
        }


        private void Clean(object sender, PaintEventArgs e) { e.Graphics.Clear(BackColor); }
        private void DrawField(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 169, 169, 169), 2);
            for (int i = BoxSise; i < this.Width; i += BoxSise)
            {
                e.Graphics.DrawLine(pen, i, 0, i, this.Height);
            }

            for (int i = BoxSise; i <= this.Height; i += BoxSise)
            {
                e.Graphics.DrawLine(pen, 0, i, this.Width, i);
            }
        }

        private void CursorX() { this.Cursor = new Cursor("Resources/Xcur.cur"); }
        private void DrawX(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 255), 2);
            foreach (Vector2 el in xArray)
            {
                Vector2 pos = el - FirstBoxCords;
                if (((pos.X >= 0) && (pos.Y >= 0)) && ((pos.X < this.Width / BoxSise) && (pos.Y < this.Height / BoxSise)))
                {

                    e.Graphics.DrawLine(pen, BoxSise * pos.X, BoxSise * pos.Y,
                                               BoxSise * (pos.X + 1), BoxSise * (pos.Y + 1));
                    e.Graphics.DrawLine(pen, BoxSise * (pos.X + 1), BoxSise * pos.Y,
                                               BoxSise * pos.X, BoxSise * (pos.Y + 1));

                }
            }
        }

        private void CursorO() { this.Cursor = new Cursor("Resources/Ocur.cur"); }
        private void DrawO(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 255, 0, 0), 2);
            foreach (Vector2 el in oArray)
            {
                Vector2 pos = el - FirstBoxCords;
                if (((pos.X >= 0) && (pos.Y >= 0)) && ((pos.X < this.Width / BoxSise) && (pos.Y < this.Height / BoxSise)))
                {

                    e.Graphics.DrawEllipse(pen, BoxSise * pos.X, BoxSise * pos.Y,
                                               BoxSise, BoxSise);

                }
            }
        }


        private void DrawForce(object sender, PaintEventArgs e)
        {
            
            foreach (var el in xWeights)
            {
                Vector2 pos = el.Key - FirstBoxCords;
                if (((pos.X >= 0) && (pos.Y >= 0)) && ((pos.X < this.Width / BoxSise) && (pos.Y < this.Height / BoxSise)))
                {
                    e.Graphics.DrawString((string)el.Value.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos.X * BoxSise, pos.Y * BoxSise);
                }
            }

            
            foreach (var el in oWeights)
            {
                Vector2 pos = el.Key - FirstBoxCords;
                if (((pos.X >= 0) && (pos.Y >= 0)) && ((pos.X < this.Width / BoxSise) && (pos.Y < this.Height / BoxSise)))
                {
                    e.Graphics.DrawString((string)el.Value.ToString(), new Font("Arial", 8), new SolidBrush(Color.Black), pos.X * BoxSise, pos.Y * BoxSise + BoxSise / 2);
                }
            }
        }










    }
}
