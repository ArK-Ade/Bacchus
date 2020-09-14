using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus.Vue
{
    public partial class ExportForm : Form
    {
        private Database db = new Database();
        private String pathCSV = "";

        public ExportForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO Ouvre une fenetre qui dirige l'utilisateur vers la postion ou il veut exporter le fichier

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowser.Description = "Select the directory that you want to use as the default.";
            folderBrowser.ShowNewFolderButton = false;

            // OK button was pressed.
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                pathCSV = folderBrowser.SelectedPath;
                try
                {
                    db.Connexion();
                    db.ExportData(pathCSV);
                    MessageBox.Show("Base de données exportée");
                    db.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + exp.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + exp.Message);
                    db.Close();
                }
            }
            else
            {
                MessageBox.Show("Aucun dossier choisi");
            }
        }
    }
}
