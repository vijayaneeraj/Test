using System.Collections.Generic;
using System.Linq;
using Sorter.Model;

namespace Sorter.Common
{
    /* The actual work-horse which has the logical implmentation for the NameModel 
     * sorting
     */ 
    public class NameSorter : INameSorter
    {
        public IList<NameModel> SortNameByLastAndFirstName(IList<NameModel> unsortedNames)
        {
            if (unsortedNames != null && unsortedNames.Count > 0)
            {
                return unsortedNames.OrderBy(n => n.LastName).ThenBy(n => n.FirstName).ToList();
            }
            return null;
        }
    }
}