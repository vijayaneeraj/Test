using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Sorter.DataAcessLayer;
using Sorter.Model;

namespace Sorter.DataAcessLayer
{
    /* Implementation of the File-Handling adapter
     */
    public class TextFileReaderWritter : ITextFileReaderWritter
    {
        override public IList<NameModel> ReadAll(string filePath)
        {
            IList<NameModel> users = null;
            if (File.Exists(filePath))
            {
                using (var reader = File.OpenRead(filePath))
                using (var textFileParser = new TextFieldParser(reader))
                {
                    textFileParser.TrimWhiteSpace = true;
                    textFileParser.Delimiters = new[] {","};
                    while (!textFileParser.EndOfData)
                    {
                        //Read comma sepearted line
                        var line = textFileParser.ReadFields();
                        //Create mapped model for each row of data

                        var name = GetNameModel(line);
                        if (name != null)
                        {
                            if (users == null) users = new List<NameModel>();
                            users.Add(name);
                        }
                    }
                }
            }
            return users;
        }

        override public void WriteAll(IList<NameModel> names, string filePath)
        {
            if (names != null && names.Count > 0)
            {
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var name in names)
                    {
                        var lineText = GetTextLine(name);
                        if (!string.IsNullOrEmpty(lineText))
                        {
                            writer.WriteLine(lineText);
                        }
                    }
                }
            }
        }

        protected string GetTextLine(NameModel name)
        {

            if (name == null || (string.IsNullOrEmpty(name.FirstName)
                                 && string.IsNullOrEmpty(name.LastName)))
            {
                return string.Empty;
            }

            return string.IsNullOrEmpty(name.FirstName) 
                    ? string.Format("{0}", name.LastName) 
                    : string.Format("{0}, {1}", name.LastName, name.FirstName);
         
        }

        protected NameModel GetNameModel(string[] fields)
        {
            if (fields!=null && fields.Length > 0)
            {
                return new NameModel
                {
                    LastName = fields[0],
                    FirstName = fields.Length == 2 ? fields[1]:string.Empty
                };
            }
            return null;
        }
    }
}