using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class Articles
    {
        public String refArticle { get; set; }

        public String description { get; set; }

        public int refSousFamille { get; set; }

        public int refMarque { get; set; }

        public float prixHT { get; set; }

        public int quantite { get; set; }

        /** Default constructor
         * 
         */
        public Articles() {}

        /** Confortable constructor
        *  @param obj float prixht
        */
        public Articles(float prix)
        {
            prixHT = prix;
        }

        /** Confortable constructor
        *  @param obj string description
        *  @param obj string refArticle
        *  @param obj float prixht
        */
        public Articles(string description, string refArticle, float prixht)
        {
            this.description = description;
            this.refArticle = refArticle;
            prixHT = prixht;
            quantite = 1;
        }

        /** Confortable constructor
         *  @param obj string description
         *  @param obj string refArticle
         *  @param obj int marque
         *  @param obj int sousfamille
         *  @param obj float prixht
         */
        public Articles(string description, string refArticle, int marque, int sousfamille, float prixht)
        {
            this.description = description;
            this.refArticle = refArticle;
            prixHT = prixht;
            refMarque = marque;
            refSousFamille = sousfamille;

        }
    }
}