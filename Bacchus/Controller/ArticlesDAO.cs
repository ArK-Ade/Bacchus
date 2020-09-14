using Bacchus.Model;
using System;
using System.Collections;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Bacchus.Controller
{
    class ArticlesDAO : DAO
    {
        private Database database { get; set ; }

        public ArticlesDAO()
        {
            database = new Database();
        }

        public ArticlesDAO(Database db)
        {
            database = db;
        }

        /**
        * Méthod Create
        * @param obj ref T data
        * @return boolean 
        */
        public override Boolean CreateDAO<T>(ref T data)
        {
            if (data.GetType().Name == "Articles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));

                try
                {
                    string sql = "insert into Articles (refArticle,description,refsousfamille,refmarque, prixHT,quantite) values ('"+ articles.refArticle+"', '" + articles.description+"',"+articles.refSousFamille + "," + articles.refMarque + ",'" + articles.prixHT + "',1)";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    commande.ExecuteNonQuery();
                    return true;
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return false;
                }
            }
            return false;
        }

        /**
        * Méthod Drop
        * @param obj ref T data
        * @return boolean 
        */
        public override Boolean Delete<T>(ref T data)
        {
            if (data.GetType().Name == "Articles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));

                try
                {
                    string sql = "delete from Articles WHERE refArticle=" + articles.refArticle;
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    commande.ExecuteNonQuery();
                    return true;
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return false;
                }
            }
            return false;
        }

        /**
        * Méthod Update
        * @param obj ref T data
        * @return boolean
        */
        public override Boolean Update<T>(ref T data)
        {
            if (data.GetType().Name == "Articles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));
                try
                {
                    string sql = "update Articles SET description = '" + articles.description+ "', refsousfamille = " + articles.refSousFamille + ",refmarque = " + articles.refMarque+ ", prixHT = '" + articles.prixHT + "' ,quantite = " + articles.quantite + " WHERE  refArticle='" + articles.refArticle+"'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    commande.ExecuteNonQuery();
                    return true;
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return false;
                }
            }
            return false;

        }

        /**
        * Méthod Find
        * @param obj ref T data
        * @return boolean
        */
        public override Boolean Find<T>(ref T data)
        {
            if (data.GetType().Name == "Articles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));
                try
                {
                    string sql = "SELECT * FROM Articles WHERE description = '" + articles.description + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if (sQLiteDataReader.HasRows)
                    {
                        sQLiteDataReader.Close();
                        return true;
                    }
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return false;
                }
            }
            return false;
        }

        /**
        * Méthod FindID
        * @param obj ref T data
        * @return int
        */
        public override int FindID<T>(ref T data)
        {
            if (data.GetType().Name == "Articles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));
                int idName;

                try
                {
                    string sql = "SELECT * FROM Articles WHERE refArticle = '" + articles.refArticle + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if (sQLiteDataReader.HasRows)
                    {
                        sQLiteDataReader.Read();
                        idName = sQLiteDataReader.GetInt32(0);
                        sQLiteDataReader.Close();
                        return idName;
                    }
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return 0;
                }
            }
            return 0;
        }

        /**
        * Méthod FindName
        * @param obj ref T data
        * @return string
        */
        public override string FindName<T>(ref T data)
        {
            if (data.GetType().Name == "Familles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));
                string nameFound;
                try
                {
                    string sql = "SELECT * FROM Articles WHERE nom = '" + articles.refArticle + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if (sQLiteDataReader.HasRows)
                    {
                        sQLiteDataReader.Read();
                        nameFound = sQLiteDataReader.GetString(0);
                        sQLiteDataReader.Close();
                        return nameFound;
                    }
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return "";
                }
            }
            return "";
        }


        /**
        * Méthod Count
        * @param obj ref T data
        * @return int
        */
        public int Count<T>(ref T data)
        {
            if (data.GetType().Name == "Articles")
            {
                Articles articles = (Articles)Convert.ChangeType(data, typeof(Articles));
                int countFind; 
                try
                {
                    string sql = "SELECT quantite FROM Articles WHERE description = '" + articles.description + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if (sQLiteDataReader.HasRows)
                    {
                        sQLiteDataReader.Read();
                        countFind = sQLiteDataReader.GetInt32(0);
                        sQLiteDataReader.Close();
                        return countFind;
                    }
                }catch (SQLiteException e){
                    MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                    Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                    return 0;
                }
            }
            return 0;
        }

        public override ArrayList FindAll()
        {
            string nameFound;
            string familyFound;
            string sousfamillefound;
            string marquesfound;
            int quantityfound;
            ArrayList list = new ArrayList();

            try
            {
                string sql = "SELECT * FROM Articles " +
                    "INNER JOIN Marques ON Articles.RefMarque = Marques.RefMarque " +
                    "INNER JOIN SousFamilles ON Articles.RefSousFamille = SousFamilles.RefSousFamille " +
                    "INNER JOIN Familles ON Familles.RefFamille = SousFamilles.RefFamille; ";

                SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        nameFound = sQLiteDataReader.GetString(1);
                        familyFound = sQLiteDataReader.GetString(12);
                        sousfamillefound = sQLiteDataReader.GetString(10);
                        marquesfound = sQLiteDataReader.GetString(7);
                        quantityfound = sQLiteDataReader.GetInt32(5);

                        string[] petitlist =  { nameFound, familyFound,sousfamillefound,marquesfound, Convert.ToString(quantityfound) };

                        list.Add(petitlist);
                    }
                }
                sQLiteDataReader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                return null;
            }

            return list;
        }

        public ArrayList FindbyFamily(string familyName)
        {
            string nameFound;
            string sousfamillefound;
            string marquesfound;
            int quantityfound;

            ArrayList list = new ArrayList();

            try
            {
                string sql = "SELECT * FROM Articles " +
                    "INNER JOIN Marques ON Articles.RefMarque = Marques.RefMarque " +
                    "INNER JOIN SousFamilles ON Articles.RefSousFamille = SousFamilles.RefSousFamille " +
                    "INNER JOIN Familles ON Familles.RefFamille = SousFamilles.RefFamille " +
                    "WHERE Familles.Nom = '"+familyName+"'; ";

                SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        nameFound = sQLiteDataReader.GetString(1);
                        sousfamillefound = sQLiteDataReader.GetString(10);
                        marquesfound = sQLiteDataReader.GetString(7);
                        quantityfound = sQLiteDataReader.GetInt32(5);

                        string[] petitlist = { nameFound, sousfamillefound, marquesfound, Convert.ToString(quantityfound) };

                        list.Add(petitlist);
                    }
                }
                sQLiteDataReader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                return null;
            }

            return list;
        }

        public ArrayList FindbySousFamille(string SousFamilleName)
        {
            string nameFound;
            string marquesfound;
            int quantityfound;

            ArrayList list = new ArrayList();

            try
            {
                string sql = "SELECT * FROM Articles " +
                    "INNER JOIN Marques ON Articles.RefMarque = Marques.RefMarque " +
                    "INNER JOIN SousFamilles ON Articles.RefSousFamille = SousFamilles.RefSousFamille " +
                    "INNER JOIN Familles ON Familles.RefFamille = SousFamilles.RefFamille " +
                    "WHERE SousFamilles.Nom = '" + SousFamilleName + "'; ";

                SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        nameFound = sQLiteDataReader.GetString(1);
                        marquesfound = sQLiteDataReader.GetString(7);
                        quantityfound = sQLiteDataReader.GetInt32(5);

                        string[] petitlist = { nameFound, marquesfound, Convert.ToString(quantityfound) };

                        list.Add(petitlist);
                    }
                }
                sQLiteDataReader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                return null;
            }

            return list;
        }

        public ArrayList FindbyMarques(string marquesName)
        {
            string nameFound;
            int quantityfound;

            ArrayList list = new ArrayList();

            try
            {
                string sql = "SELECT * FROM Articles " +
                    "INNER JOIN Marques ON Articles.RefMarque = Marques.RefMarque " +
                    "INNER JOIN SousFamilles ON Articles.RefSousFamille = SousFamilles.RefSousFamille " +
                    "INNER JOIN Familles ON Familles.RefFamille = SousFamilles.RefFamille " +
                    "WHERE Marques.Nom = '" + marquesName + "'; ";

                SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        nameFound = sQLiteDataReader.GetString(1);
                        quantityfound = sQLiteDataReader.GetInt32(5);

                        string[] petitlist = { nameFound, Convert.ToString(quantityfound) };

                        list.Add(petitlist);
                    }
                }
                sQLiteDataReader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("L'erreur suivante a été rencontrée :" + e.Message);
                Console.WriteLine("L'erreur suivante a été rencontrée :" + e.Message);
                return null;
            }

            return list;
        }

       
    }
}