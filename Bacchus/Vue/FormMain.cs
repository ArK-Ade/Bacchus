using Bacchus.Controller;
using Bacchus.Model;
using Bacchus.Vue;
using System;
using System.Collections;
using System.Windows.Forms;

namespace Bacchus
{

    public partial class FormMain : Form
    {
       
        public FormMain()
        {
            InitializeComponent();
        }

        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {     
            Form Form2 = new ImportForm();
            Form2.ShowDialog();    
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            //splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Initialisation 
            listView1.Columns.Clear();
            listView1.Items.Clear();
            listView1.Groups.Clear();

            Database data = new Database();
            data.Connexion();

            

            if (e.Node.Name == "Tous les articles")
            {
                listView1.Columns.Add("Description", 200, HorizontalAlignment.Center);
                listView1.Columns.Add("Familles", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Sous-Familles", 50, HorizontalAlignment.Center);
                listView1.Columns.Add("Marques", 50, HorizontalAlignment.Center);
                listView1.Columns.Add("Quantite",50, HorizontalAlignment.Center);


                ListViewGroup listViewGroup = new ListViewGroup("un");
                listView1.Groups.Add(listViewGroup);

                ArticlesDAO articles = new ArticlesDAO(data);
                ArrayList array = articles.FindAll();

                foreach(string[] list in array)
                {
                    ListViewItem lvi = new ListViewItem(list);
                    listView1.Items.Add(lvi);
                }

            }
            else if(e.Node.Name == "Familles"){
                listView1.Columns.Add("Familles", 200, HorizontalAlignment.Center);

                FamilleDAO articles = new FamilleDAO(data);
                ArrayList array = articles.FindAll();

                foreach (string list in array)
                {
                    ListViewItem lvi = new ListViewItem(list);
                    listView1.Items.Add(lvi);
                }
            }
            else if (e.Node.Name == "Marques")
            {
                listView1.Columns.Add("Marques", 200, HorizontalAlignment.Center);

                MarquesDAO articles = new MarquesDAO(data);
                ArrayList array = articles.FindAll();

                foreach (string list in array)
                {
                    ListViewItem lvi = new ListViewItem(list);
                    listView1.Items.Add(lvi);
                }
            }
            else if (e.Node.Parent.Name == "Familles") // Famille
            {
                listView1.Columns.Add("Description", 200, HorizontalAlignment.Center);
                listView1.Columns.Add("Sous-Familles", 200, HorizontalAlignment.Center);
                listView1.Columns.Add("Marques", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Quantite", 50, HorizontalAlignment.Center);

                ArticlesDAO articles = new ArticlesDAO(data);

                string name = e.Node.Text;
                ArrayList array = articles.FindbyFamily(name);

                foreach (string[] list in array)
                {
                    ListViewItem lvi = new ListViewItem(list);
                    listView1.Items.Add(lvi);
                }
            }
            else if (e.Node.Parent.Name == "Marques") // Marques
            {
                listView1.Columns.Add("Description", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Quantite", 50, HorizontalAlignment.Center);

                ArticlesDAO articles = new ArticlesDAO(data);

                string name = e.Node.Text;
                ArrayList array = articles.FindbyMarques(name);

                foreach (string[] list in array)
                {
                    ListViewItem lvi = new ListViewItem(list);
                    listView1.Items.Add(lvi);
                }
            }
            else if (e.Node.Parent.Parent.Name == "Familles") // Sous famille
            {
                listView1.Columns.Add("Description", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Marques", 100, HorizontalAlignment.Center);
                listView1.Columns.Add("Quantite", 50, HorizontalAlignment.Center);

                ArticlesDAO articles = new ArticlesDAO(data);

                string name = e.Node.Text;
                ArrayList array = articles.FindbySousFamille(name);

                foreach (string[] list in array)
                {
                    ListViewItem lvi = new ListViewItem(list);
                    listView1.Items.Add(lvi);
                }
            }

            data.Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO Grouper par trie effectuer
            
        }

        private void ColumnClick(object o, ColumnClickEventArgs e)
        {
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);

            
            
        }

        // Implements the manual sorting of items by columns.
        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        // Chargement de la fenetre principale 
        private void FormMain_Load(object sender, EventArgs e)
        {
            // 
            listView1.ColumnClick += new ColumnClickEventHandler(ColumnClick);

            // Initialisation des variables
            Database data = new Database();
            data.Connexion();

            MarquesDAO marques = new MarquesDAO(data);
            SousFamilleDAO sousFamille = new SousFamilleDAO(data);
            FamilleDAO famille = new FamilleDAO(data);
            ArticlesDAO article = new ArticlesDAO(data);
            TreeNodeCollection nodes = treeView1.Nodes;

            // Recuperation des données
            ArrayList familyFound = famille.FindAll();
            ArrayList marqueFound = marques.FindAll();
            
            foreach (TreeNode n in nodes)
            {

                // Famille
                if(n.Name == "Familles")
                {
                    foreach (string item in familyFound)
                    {
                        TreeNode newNode = new TreeNode(item);
                        nodes[n.Index].Nodes.Insert(0,newNode);
                    }

                    // Sousfamille
                    foreach(TreeNode n2 in nodes[n.Index].Nodes)
                    {
                        String nameFamily = n2.Text;
                        ArrayList sousfamilyFound = sousFamille.Findbyfamily(nameFamily);

                        foreach (string item in sousfamilyFound)
                        {
                            TreeNode newNode = new TreeNode(item);
                            nodes[n.Index].Nodes[n2.Index].Nodes.Add(newNode);
                        }
                    }
                }
                // Marque
                else if(n.Name == "Marques")
                {
                    foreach(string item in marqueFound)
                    {
                        TreeNode newNode = new TreeNode(item);
                        nodes[n.Index].Nodes.Insert(0, newNode);
                    }
                } 
            }
            data.Close();
        }

        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Form1 = new ExportForm();
            Form1.ShowDialog();
        }
    }
}
