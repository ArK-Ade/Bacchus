using Bacchus.Model;
using System;
using System.Collections;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Bacchus.Controller
{
    class MarquesDAO : DAO
    {
        Database database { get; set; }

        public MarquesDAO()
        {
            database = new Database();
        }

        public MarquesDAO(Database db)
        {
            database = db;
        }

        /**
        * Méthod Create
        * @param obj Marques marque
        * @return boolean 
        */
        public override Boolean CreateDAO<T>(ref T data)
        {
            if (data.GetType().Name == "Marques")
            {
                Marques marque = (Marques)Convert.ChangeType(data, typeof(Marques));
                try
                {
                    string sql = "insert into marques (refMarque,nom) values (null, '" + marque.marqueName + "')";
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
        * @param obj Marques marque
        * @return boolean 
        */
        public override Boolean Delete<T>(ref T data)
        {
            if (data.GetType().Name == "Marques")
            {
                Marques marque = (Marques)Convert.ChangeType(data, typeof(Marques));
                try
                {
                    string sql = "delete from marques values refMarques=" + marque.refMarque;
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
        * @param obj Marques marque
        * @return boolean
        */
        public override Boolean Update<T>(ref T data)
        {
            if (data.GetType().Name == "Marques")
            {
                Marques marque = (Marques)Convert.ChangeType(data, typeof(Marques));
                try
                {
                    string sql = "update marques SET nom = " + marque.marqueName + " WHERE refMarques=" + marque.refMarque;
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
        * @param obj Marques marque
        * @return boolean
        */
        public override Boolean Find<T>(ref T data)
        {
            if (data.GetType().Name == "Marques")
            {
                Marques marque = (Marques)Convert.ChangeType(data, typeof(Marques));
                try
                {
                    string sql = "SELECT * FROM Marques WHERE nom ='" + marque.marqueName + "'";
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
        * Méthod FindById
        * @param obj Marques marque
        * @return int
        */
        public override int FindID<T>(ref T data)
        {
            if (data.GetType().Name == "Marques")
            {
                Marques marque = (Marques)Convert.ChangeType(data, typeof(Marques));
                int idFound;
                try
                {
                    string sql = "SELECT * FROM Marques WHERE nom = '" + marque.marqueName + "'";
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

        public override string FindName<T>(ref T data)
        {
            if (data.GetType().Name == "Marques")
            {
                Marques marque = (Marques)Convert.ChangeType(data, typeof(Marques));
                string nameFound;
                try
                {
                    string sql = "SELECT * FROM Marques WHERE nom = '" + marque.marqueName + "'";
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
                string sql = "SELECT DISTINCT nom FROM Marques";
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