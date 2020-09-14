using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.Model
{
    class Familles
    {

        public int refFamille { get; set; }

        public String familleName { get; set; }

        /** Default constructor
         * 
         */
        public Familles(){}

        /** Confortable constructor
         *  @param obj int NewrefFamille
         */
        public Familles(int NewrefFamille)
        {
            refFamille = NewrefFamille;
        }
    }
}
