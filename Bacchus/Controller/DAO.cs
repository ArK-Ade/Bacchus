using System;
using System.Collections;

namespace Bacchus.Controller
{
    public abstract class DAO
    {
        /**
        * Méthod Create
        * @param ref T data
        * @return boolean 
        */
        public abstract Boolean CreateDAO<T>(ref T data);

        /**
         * Méthod Drop
         * @param ref T data
         * @return boolean 
         */
        public abstract Boolean Delete<T>(ref T data);

        /**
        * Méthod Update
        * @param ref T data
        * @return boolean
        */
        public abstract Boolean Update<T>(ref T data);

        /**
        * Méthod Find
        * @param ref T data
        * @return boolean
        */
        public abstract Boolean Find<T>(ref T data);

        /**
        * Méthod FindID
        * @param ref T data
        * @return int
        */
        public abstract int FindID<T>(ref T data);

        /**
        * Méthod FindName
        * @param ref T data
        * @return string
        */
        public abstract string FindName<T>(ref T data);

        /**
        * Méthod FindAll
        * @param 
        * @return ArrayList
        */
        public abstract ArrayList FindAll();
    }
}
