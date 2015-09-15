using System.Collections.Generic;
using Sorter.Model;

/* 
 * Adapter interface class to the File-Handling, which could 
 * handle NameModel(adapting)
 */
namespace Sorter.DataAcessLayer
{
    public interface ITextFileReaderWritter
    {
        // Interface to read from the file,
        // extract NameModel(s)
        // args:
        //      fileName - input names file        
        IList<NameModel> ReadAll(string fileName);

        // Interface to write to file, all the name model
        // args: 
        //      names           - List of name models that should be 
        //                        printed to file
        //      outputFileName  - output file name
        void WriteAll(IList<NameModel> names, string outputFileName);
    }
}