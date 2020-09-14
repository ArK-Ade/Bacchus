using Bacchus.Model;
using System;
using System.Collections;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Bacchus.Controller
{
    class SousFamilleDAO : DAO
    {
        Database database { get; set; }

        public SousFamilleDAO()
        {
            database = new Database();
        }

        public SousFamilleDAO(Database db)
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
            if (data.GetType().Name == "SousFamilles")
            {
                SousFamilles sousfamille = (SousFamilles)Convert.ChangeType(data, typeof(SousFamilles));

                try
                {
                    string sql = "insert into sousfamilles (refSousFamille,refFamille,nom) values (null, " + sousfamille.refFamille + ", '" + sousfamille.sousFamilleName + "')";
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
            if (data.GetType().Name == "SousFamilles")
            {
                SousFamilles sousfamille = (SousFamilles)Convert.ChangeType(data, typeof(SousFamilles));

                try
                {
                    string sql = "delete from sousfamilles WHERE refSousFamille=" + sousfamille.refSousFamille;
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
            if (data.GetType().Name == "SousFamilles")
            {
                SousFamilles sousfamille = (SousFamilles)Convert.ChangeType(data, typeof(SousFamilles));

                try
                {
                    string sql = "update sousfamilles SET refSousFamille = " + sousfamille.refSousFamille + ", nom = " + sousfamille.sousFamilleName + "  WHERE refSousFamille=" + sousfamille.refSousFamille;
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
            if (data.GetType().Name == "SousFamilles")
            {
                SousFamilles sousfamille = (SousFamilles)Convert.ChangeType(data, typeof(SousFamilles));

                try
                {
                    string sql = "SELECT * FROM SousFamilles WHERE nom ='" + sousfamille.sousFamilleName + "'";
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
            if (data.GetType().Name == "SousFamilles")
            {
                SousFamilles sousfamille = (SousFamilles)Convert.ChangeType(data, typeof(SousFamilles));
                int idFound;

                try
                {
                    string sql = "SELECT * FROM SousFamilles WHERE nom = '" + sousfamille.sousFamilleName + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if (sQLiteDataReader.HasRows)
                    {
                        sQLiteDataReader.Read();
                        idFound = sQLiteDataReader.GetInt32(0);
                        sQLiteDataReader.Close();
                        return idFound;
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
            if (data.GetType().Name == "SousFamilles")
            {
                SousFamilles sousfamille = (SousFamilles)Convert.ChangeType(data, typeof(SousFamilles));
                string nameFound;

                try
                {
                    string sql = "SELECT * FROM SousFamilles WHERE nom = '" + sousfamille.sousFamilleName + "'";
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

        public override ArrayList FindAll()
        {
            string nameFound;
            ArrayList list = new ArrayList();

            try
            {
                string sql = "SELECT DISTINCT nom FROM SousFamilles";
                SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        nameFound = sQLiteDataReader.GetString(0);
                        list.Add(nameFound);
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

        public ArrayList Findbyfamily(String famille)
        {
            string sousFamilyFound;
            ArrayList list = new ArrayList();

            try
            {
                string sql = "SELECT SousFamilles.Nom FROM Familles INNER JOIN SousFamilles ON Familles.RefFamille = SousFamilles.RefFamille WHERE Familles.Nom = '"+famille+"'";
                SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                if (sQLiteDataReader.HasRows)
                {
                    while (sQLiteDataReader.Read())
                    {
                        sousFamilyFound = sQLiteDataReader.GetString(0);
                        list.Add(sousFamilyFound);
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
