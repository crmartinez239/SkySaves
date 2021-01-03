using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SkySave_Test
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog(this);
            string saveFile = openFileDialog.FileName;

            if (saveFile != String.Empty)
            {

                //    richTextBox1.Text = string.Empty;
                //    var saver = new SkySaves.SaveFile();
                //    saver.Import(saveFile);
                //    debug("Name", saver.HeaderInfo.PlayerName);
                //    debug("Level", saver.HeaderInfo.PlayerLevel);
                //    debug("Location", saver.HeaderInfo.PlayerLocation);
                //    debug("Game Date", saver.HeaderInfo.GameDate);
                //    debug("Race", saver.HeaderInfo.PlayerRace);
                //    debug("Sex", saver.HeaderInfo.PlayerSexF);
                //    debug("Version", saver.HeaderInfo.Version);

                //    foreach (string plugIn in saver.PlugIns.PlugIns)
                //        debug("Plug In", plugIn);

                //    foreach (var miscStat in saver.MiscStats.Stats)
                //        debug(miscStat.Name, miscStat.Value.ToString());

                //    pictureBox1.Image = new Bitmap(saver.HeaderInfo.ScreenShot.ScreenShotImage,
                //        new Size(pictureBox1.Width, pictureBox1.Height));
            }
        }

        private void openSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog(this);
            string saveFile = openFileDialog.FileName;

            if (saveFile == string.Empty)
                return;

            var sf = new SkySaves.SaveFile();

            if (!sf.Import(saveFile))
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        //private void appendRedText(string text)
        //{
        //    richTextBox1.SelectionColor = Color.Red;
        //    richTextBox1.AppendText($"{text}: ");
        //    richTextBox1.SelectionColor = SystemColors.WindowText;
        //}

        //private void debug(string title, string text)
        //{
        //    appendRedText(title);
        //    richTextBox1.AppendText($"{text}\n");
        //}

        //private void debug(string title, uint number)
        //{
        //    appendRedText(title);
        //    richTextBox1.AppendText($"{number.ToString()}\n");
        //}

        //private void debug(string title, byte b)
        //{
        //    appendRedText(title);
        //    richTextBox1.AppendText($"{b.ToString()}\n");
        //}

        //private void debug(string title, byte[] b)
        //{
        //    appendRedText(title);
        //    foreach(byte i in b)
        //    {
        //        richTextBox1.AppendText($"{i.ToString()} ");
        //    }

        //    richTextBox1.AppendText("\n");
        //}
    }
}
