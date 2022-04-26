using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPITest.Extensions
{
    public static class TwoDListExtensions
    {
        public static bool SequencesEqual<T>(this List<List<T>> originList, List<List<T>> comparatorList)
        {
            //Run fastest checks first

            if(originList == null || comparatorList == null) return false;
          
            if(ReferenceEquals(originList, comparatorList)) return true;

            if(originList.Count != comparatorList.Count) return false;

            //Loop through each instance of the origin list
            for(int i = 0; i < originList.Count; i++)
            {
                //Now we can compare the individual lists
                if(!originList[i].SequenceEqual(comparatorList[i])) return false;
            }

            //If all checks passed, return true
            return true;
        }
    }
}
