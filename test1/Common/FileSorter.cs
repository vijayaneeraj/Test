using System;
using System.IO;

namespace Sorter.Common
{
    /*
     * Abstract class for sorting contents of file
     * like:
     *  - Strings
     *  - Numbers
     *  - foating types etc. 
     *  which is '\n' (newline) separated
     */

    public abstract class FileSorter : ISorter
    {
        //Derived class support concreate implementation 
        private const string SortetFileNameSuffix = "-sorted";

        // Contructor works on input file
        protected FileSorter(string inputFile)
        {
            InputFile = inputFile;
            OutputFile = GenerateOutputFileName(inputFile);
        }

        protected string InputFile { get; set; }
        protected string OutputFile { get; set; }

        // Overriding the Sort() function which actually 
        // defines the meaning of the sort for sub types
        // The algo for a file sort is defined as below: 
        // 
        // Step 1: Validation   - Validation based on the concrete implementation like:
        //                          --> check if file exist
        //                          --> Is file empty, etc.
        // Step 2: Perform Sort - Perform the actual sort(newline separated)
        //                        alphanumeric
        // Step 3: Display      - Display to console or write to file based on the 
        //                        requirement/concrete implementation
        public void Sort()
        {
            // Step 1: Validation
            ValidateInputParameter();

            // Step 2: Perform Sort
            PerformSort();

            // Step 3: Display result
            DisplaySortedResult();

            Console.ReadKey();
        }

        protected virtual void Log(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }


        /*
         *  Creates an auto generated output file to store the result
         */ 
        private string GenerateOutputFileName(string inputFile)
        {
            if (File.Exists(inputFile))
            {
                var sortedFileName = inputFile;
                var lastIndex = sortedFileName.LastIndexOf(".", StringComparison.Ordinal);
                if (lastIndex != -1)
                {
                    sortedFileName = sortedFileName.Insert(lastIndex, SortetFileNameSuffix);
                    if (File.Exists(sortedFileName))
                    {
                        File.Delete(sortedFileName);
                    }
                    return sortedFileName;
                }
            }
            return String.Empty;
        }

        #region AbstractMembers

        protected abstract void PerformSort();
        protected abstract void DisplaySortedResult();
        protected abstract bool ValidateInputParameter();

        #endregion
    }
}