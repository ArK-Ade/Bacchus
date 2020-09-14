using Bacchus.Model;
using System;
using System.Collections;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Bacchus.Controller
{
    class FamilleDAO : DAO
    {
        Database database { get; set; }

        public FamilleDAO()
        {
            database = new Database();
        }

        public FamilleDAO(Database db)
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
            if (data.GetType().Name == "Familles")
            {
                Familles famille = (Familles)Convert.ChangeType(data, typeof(Familles));
                try
                {
                    string sql = "insert into familles (refFamille,nom) values (null, '" + famille.familleName + "')";
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
            if (data.GetType().Name == "Familles")
            {
                Familles famille = (Familles)Convert.ChangeType(data, typeof(Familles));
                try
                {
                    string sql = "delete from familles WHERE refFamille=" + famille.refFamille;
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
            if (data.GetType().Name == "Familles")
            {
                Familles famille = (Familles)Convert.ChangeType(data, typeof(Familles));
                try
                {
                    string sql = "update familles SET nom = '" + famille.familleName + "' WHERE refFamille = " + famille.refFamille;
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
            if (data.GetType().Name == "Familles")
            {
                Familles famille = (Familles)Convert.ChangeType(data, typeof(Familles));
                try
                {
                    string sql = "SELECT * FROM Familles WHERE nom = '" + famille.familleName + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if(sQLiteDataReader.HasRows)
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
        * Méthod FindById
        * @param obj ref T data
        * @return int
        */
        public override int FindID<T>(ref T data)
        {
            if (data.GetType().Name == "Familles")
            {
                Familles famille = (Familles)Convert.ChangeType(data, typeof(Familles));
                int idFound;
                try
                {
                    string sql = "SELECT * FROM Familles WHERE nom = '" + famille.familleName + "'";
                    SQLiteCommand commande = new SQLiteCommand(sql, database.connection);
                    SQLiteDataReader sQLiteDataReader = commande.ExecuteReader();

                    if (sQLiteDataReader.HasRows)
                    {
                        sQLiteDataReader.Read();
                        idFound = sQLiteDataReader.GetInt32(0); // Erreur
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
        * Méthod FindById
        * @param obj ref T data
        * @return string
        */
        public override string FindName<T>(ref T data)
        {
            if (data.GetType().Name == "Familles")
            {
                Familles famille = (Familles)Convert.ChangeType(data, typeof(Familles));
                string nameFound;
                try
                {
                    string sql = "SELECT * FROM Familles WHERE nom = '" + famille.familleName + "'";
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
                string sql = "SELECT DISTINCT nom FROM Familles";
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

    }
}