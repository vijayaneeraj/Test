using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Sorter.Common;
using Sorter.DataAcessLayer;
using Sorter.Model;

namespace Sorter.Controller
{
    public class FileNameSorter : FileSorter
    {
        public FileNameSorter(string inputFile)
            : base(inputFile)
        {
        }

        protected virtual INameSorter NameSorterWrapper
        {
            get { return _nameSorter ?? (_nameSorter = new NameSorter()); }
        }

        protected virtual ITextFileReaderWritter TextFileReaderWritter
        {
            get { return _textFileNameProvider ?? (_textFileNameProvider = new TextFileReaderWritter()); }
        }

        #region FileSorter Abstract Implimentation

        protected override void PerformSort()
        {
            /*
             * 1. Read Names from File
             * 2. Sort names
             * 3. Write sorted name into output file
             */
            var unsortedNames = TextFileReaderWritter.ReadAll(InputFile);
            var sortedNames = NameSorterWrapper.SortNameByLastAndFirstName(unsortedNames);
            TextFileReaderWritter.WriteAll(sortedNames, OutputFile);
        }

        protected override void DisplaySortedResult()
        {
            if (IsFileExist(OutputFile))
            {
                var sortedNames = TextFileReaderWritter.ReadAll(OutputFile);
                WriteSortedNamesToConsole(sortedNames);
                Log(String.Format("Finished: Created {0} ",GetFileName(OutputFile) ));
            }
            else
            {
                Log("Finished. Sorted output file has not created. Please check error log.");
            }
        }

        protected override bool ValidateInputParameter()
        {
            if (IsFileExist(InputFile))
            {
                return true;
            }
            Log(string.Format("InputFile:{0} does not exist. Verify inputfile path.", InputFile));
            return false;
        }

        #endregion

        #region  Wrapper for Suytem dependent code
        protected virtual bool IsFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        protected virtual string GetFileName(string filePath)
        {
            return Path.GetFileName(OutputFile);
        }
        #endregion

        protected virtual void WriteSortedNamesToConsole(IList<NameModel> sortedNames)
        {
            if (sortedNames != null && sortedNames.Count > 0)
            {
                sortedNames.ToList().ForEach(n =>
                    Log(string.Format("{0}, {1}", n.LastName, n.FirstName)));
            }
        }

        private INameSorter _nameSorter;
        private ITextFileReaderWritter _textFileNameProvider;

    }
}