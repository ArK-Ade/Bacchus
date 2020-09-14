using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using Bacchus.Model;
using System.Data.SQLite;

namespace Bacchus
{
    public partial class ImportForm : Form
    {
        private Database db = new Database();
        private String pathCSV = "";

        public ImportForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileName = "Select a csv file",
                Filter = "Text files (*.csv)|*.csv",
                Title = "Open csv file"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               label1.Text = openFileDialog1.SafeFileName;
               pathCSV = openFileDialog1.FileName;

            }
        }

        /** <summary>
         *  Overwrite Mode
         * </summary>
         */
        private void button2_Click(object sender, EventArgs e)
        {
            if (!(pathCSV == ""))
            {
                try
                {
                    db.Connexion();
                    db.InitBDD();
                    db.ExtractData(pathCSV);
                    MessageBox.Show("Base de données importée");
                    db.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + exp.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + exp.Message);
                    db.Close();
                } 
            }else{
                 MessageBox.Show("Aucun fichier choisi");
            }
        }

        /** <summary>
         *  Add Mode
         * </summary>
         */
        private void button3_Click(object sender, EventArgs e)
        {
            if(!(pathCSV == ""))
            {
                try
                {
                    db.Connexion();
                    db.ExtractData(pathCSV);
                    MessageBox.Show("Base de données importée");
                    db.Close();
                }
                catch(Exception exp){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + exp.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + exp.Message);
                    db.Close();
                }
            }
            else
            {
                MessageBox.Show("Aucun fichier choisi");
            }
            
        }
    }
}
