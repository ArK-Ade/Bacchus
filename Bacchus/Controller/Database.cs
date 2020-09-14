using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using Bacchus.Controller;
using System.Text;

namespace Bacchus.Model
{
    /**
     * Class that permit to acces to the database
     */
    class Database
    {
        /**
         * Attributes 
         */
        private const string dbPath = @"Bacchus.SQLite";
        public SQLiteConnection connection;

        /**
         * Default constructor
         */
        public Database()
        {

        }

        /**
         * Reinitialize the database
         */
        public void InitBDD()
        {
            string reset1 = "DELETE FROM Articles";
            string reset2 = "DELETE FROM Familles";
            string reset3 = "DELETE FROM Marques";
            string reset4 = "DELETE FROM SousFamilles";

            SQLiteCommand commande = new SQLiteCommand(reset1, connection); 
            commande.ExecuteNonQuery();
            
            commande = new SQLiteCommand(reset2, connection);
            commande.ExecuteNonQuery();

            commande = new SQLiteCommand(reset3, connection);
            commande.ExecuteNonQuery();

            commande = new SQLiteCommand(reset4, connection);
            commande.ExecuteNonQuery();  
        }

        /** 
         * open a connection in the database
         */
        public void Connexion()
        {
            try
            {
                String currentPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string absolutePath;
                string connectionString;

                absolutePath = System.IO.Path.Combine(currentPath, dbPath);
                connectionString = string.Format("DataSource={0}", absolutePath);

                connection = new SQLiteConnection(connectionString);
                connection.Open();
                Console.WriteLine("Etat de la connexion : " + connection.State);
            }
            catch (Exception e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
            }
        }

        /**
         * close the connection to the database
         */
        public void Close()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Etat de la connexion : " + connection.State);
            }
            catch (Exception e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
            }
        }

        /** 
         * Fonction that permit to read CSV Files
         */
        public void ExtractData(string path)
        {
            // Variables
            string[] Tligne = new string[100]; 
            char[] splitter = { ';' };

            if (File.Exists(path))
            {
                StreamReader line = new StreamReader(path);
                string ligne = line.ReadLine();
                
                while ((ligne = line.ReadLine()) != null)
                {
                    Tligne = ligne.Split(splitter); 
                                                  
                    string readDescription = Tligne[0];
                    string readRefArticle = Tligne[1];
                    string readMarque = Tligne[2];
                    string readFamille = Tligne[3];
                    string readSous_famille = Tligne[4];
                    string Readprice = Tligne[5];

                    CSVLineToData(readDescription, readRefArticle, readMarque, readFamille, readSous_famille, Readprice);
                }
                line.Close();
            }else{
                MessageBox.Show("Le fichier n'existe pas.");
            }
        }

        /** 
         * Fonction that convert CSV ligne into data
         */
        public void CSVLineToData(string readDescription, string readRefArticle, string readMarque, string readFamille, string readSous_famille, string Readprice)
        {
            ArticlesDAO DAOArticle = new ArticlesDAO(this);
            FamilleDAO DAOFamille = new FamilleDAO(this);
            MarquesDAO DAOMarque = new MarquesDAO(this);
            SousFamilleDAO DAOSSFamille = new SousFamilleDAO(this);

            Articles articles = new Articles(readDescription, readRefArticle, float.Parse(Readprice));
            Familles familles = new Familles { refFamille = -1 ,familleName = readFamille };
            Marques marques = new Marques{ marqueName = readMarque };
            SousFamilles sousfamille = new SousFamilles { sousFamilleName = readSous_famille };

            // If famille doesn't exist
            if (!DAOFamille.Find<Familles>(ref familles))
            {
                DAOFamille.CreateDAO<Familles>(ref familles);
            }

            // If Marque doesn't exist
            if (!DAOMarque.Find<Marques>(ref marques))
            {
                DAOMarque.CreateDAO<Marques>(ref marques);
            }

            // If sousfamille doesn't exist
            if (!DAOSSFamille.Find<SousFamilles>(ref sousfamille))
            {
                int id = DAOFamille.FindID<Familles>(ref familles);
                Familles NEwfamille = new Familles(id);
                sousfamille = new SousFamilles(readSous_famille, id);
                DAOSSFamille.CreateDAO<SousFamilles>(ref sousfamille);
            }

            // If Article doesn't exist
            if (!DAOArticle.Find<Articles>(ref articles))
            {   
                int idMarque = DAOMarque.FindID<Marques>(ref marques);
                int idSousFamille = DAOSSFamille.FindID<SousFamilles>(ref sousfamille);

                articles = new Articles(readDescription, readRefArticle, idMarque,idSousFamille, float.Parse(Readprice));
                DAOArticle.CreateDAO<Articles>(ref articles);

            }else{ // else quantite +1
                articles.quantite+= DAOArticle.Count<Articles>(ref articles);
                DAOArticle.Update<Articles>(ref articles);
            }    
        }

        /**
         * Function that convert data into a CSV file
         */
        public void ExportData(string pathCsv)
        {
            // Variables
            var csv = new StringBuilder();
            pathCsv = System.IO.Path.Combine(pathCsv, @"Data.csv");

            string strSeperator = ";";
            string header = "";
            StringBuilder sbOutput = new StringBuilder();
            String[] inaOutput = { "Description", "Ref", "Marque", "Famille", " Sous - Famille", "Prix H.T." };

            //Creation CSV Files with Headers
            using (FileStream aFStream = new FileStream(pathCsv, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(aFStream))
                {
                    for (int i = 0; i < inaOutput.Length; i++)
                    {
                        header = header + inaOutput[i] + strSeperator;
                    }

                    sw.WriteLine(header);
                }
            }

            try
            {
                string select = "SELECT * FROM Articles " +
                    "INNER JOIN Marques ON Articles.RefMarque = Marques.RefMarque " +
                    "INNER JOIN SousFamilles ON Articles.RefSousFamille = SousFamilles.RefSousFamille " +
                    "INNER JOIN Familles ON Familles.RefFamille = SousFamilles.RefFamille; ";

                SQLiteCommand commande = new SQLiteCommand(select, connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader(); 
                
                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        string readDescription = sQLiteDataReader.GetString(1);
                        string readRefArticle = sQLiteDataReader.GetString(0);
                        float readPrice = Single.Parse(sQLiteDataReader.GetString(4));
                        int refMarque = sQLiteDataReader.GetInt32(3);
                        int refSousFamille = sQLiteDataReader.GetInt32(2);
                        string readMarqueName = sQLiteDataReader.GetString(7);
                        string readFamilleName = sQLiteDataReader.GetString(12);
                        string readSousFamilleName = sQLiteDataReader.GetString(10);

                        DataToCsvLine(readDescription, readRefArticle, readMarqueName, readFamilleName, readSousFamilleName, readPrice, pathCsv);
                    }
                }
                else
                {
                    MessageBox.Show("La base de données est vide");
                }
            }
            catch(SQLiteException e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
            }
        }

        /**
         * Function that convert data into a csv line
         */
        public void DataToCsvLine(string description, string refArticle, string marqueName, string familleName, string sousFamilleName, float price, string pathCsv)
        {

            // Function taht convert special caracters into simple one
            description = convertirChaineSansAccent(description);
            refArticle = convertirChaineSansAccent(refArticle);
            marqueName = convertirChaineSansAccent(marqueName);
            familleName = convertirChaineSansAccent(familleName);
            sousFamilleName = convertirChaineSansAccent(sousFamilleName);

            // Variables
            String[] newLine = { description,  refArticle,  marqueName,  familleName, sousFamilleName, Convert.ToString(price)};
            string strSeperator = ";";

            // Concatenate all the data
            for (int i = 0; i < newLine.Length; i++)
            {
                File.AppendAllText(pathCsv,newLine[i] + strSeperator);
            }

            // End of the line
            File.AppendAllText(pathCsv,"\n"); 
        }

        private string convertirChaineSansAccent(string chaine)
        {
            // Déclaration de variables
            string accent = "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç";
            string sansAccent = "AAAAAAaaaaaaOOOOOOooooooEEEEeeeeIIIIiiiiUUUUuuuuyNnCc";

            // Conversion des chaines en tableaux de caractères
            char[] tableauSansAccent = sansAccent.ToCharArray();
            char[] tableauAccent = accent.ToCharArray();

            // Pour chaque accent
            for (int i = 0; i < accent.Length; i++)
            {
                // Remplacement de l'accent par son équivalent sans accent dans la chaîne de caractères
                chaine = chaine.Replace(tableauAccent[i].ToString(), tableauSansAccent[i].ToString());
            }

            // Retour du résultat
            return chaine;
        }
    }
}