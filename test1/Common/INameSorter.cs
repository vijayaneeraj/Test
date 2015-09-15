using System.Collections.Generic;
using Sorter.Model;

namespace Sorter.Common
{
    /* Interface definition for the NameModel sorter - 
     * which could perform a sorting on a list of Name Models
     */ 
    public interface INameSorter
    {
        /* Performs Sort based on first, and last names on a list
         * 
         * args -
         *      unsortedNames - List of NameModels which has to be sorted
         * 
         * Returns - List of sorted names
         */ 
        IList<NameModel> SortNameByLastAndFirstName(IList<NameModel> unsortedNames);
    }
}