using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class SousFamilles
    {

        public int refSousFamille { get; set; }

        public int refFamille { get; set; }

        public String sousFamilleName { get; set; }

        /** Default constructor
         * 
         */
        public SousFamilles() {}

        /** Confortable constructor
         *  @param obj string newName
         *  @param obj int newRefFamille
         */
        public SousFamilles(string newName, int newRefFamille)
        {
            sousFamilleName = newName;
            refFamille = newRefFamille;
        }
    }
}
