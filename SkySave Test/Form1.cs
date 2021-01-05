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

        private void openSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog(this);
            string saveFile = openFileDialog.FileName;

            if (saveFile == string.Empty)
                return;

            var sf = new SkySaves.SaveFile();

            if (!sf.Import(saveFile))
            {
                MessageBox.Show(sf.ErrorMessage, "File Error");
                return;
            }

            fillList(sf);    
        }

        private void fillList(SkySaves.SaveFile sf)
        {
            var headerNode = new TreeNode("Header");
            headerNode.Nodes.Add($"Name - {sf.HeaderInfo.PlayerName}");
            headerNode.Nodes.Add($"Level - {sf.HeaderInfo.PlayerLevel}");
            headerNode.Nodes.Add($"Sex - {sf.HeaderInfo.PlayerSexF}");
            headerNode.Nodes.Add($"Race - {sf.HeaderInfo.PlayerRace}");
            headerNode.Nodes.Add($"Game Date - {sf.HeaderInfo.GameDate}");
            headerNode.Nodes.Add($"Location - {sf.HeaderInfo.PlayerLocation}");
            headerNode.Nodes.Add($"Version - {sf.HeaderInfo.Version}");

            var plugInsNode = new TreeNode("PlugIns");
            foreach (string plugIn in sf.PlugIns.PlugIns)
                plugInsNode.Nodes.Add(plugIn);

            var miscStatNode = new TreeNode("Misc Stats");
            foreach (var stat in sf.MiscStats.Stats)
                miscStatNode.Nodes.Add($"{stat.CategoryF} - {stat.Name} - {stat.Value}");

            var crimeInfoNode = new TreeNode("Crime Info");
            foreach (var crime in sf.ProcessList.AllCrimes)
                crimeInfoNode.Nodes.Add($"{crime.CrimeTypeF} - Bounty - {crime.Bounty}");

            treeView1.Nodes.Add(headerNode);
            treeView1.Nodes.Add(plugInsNode);
            treeView1.Nodes.Add(miscStatNode);
            treeView1.Nodes.Add(crimeInfoNode);
        }

    }

}
