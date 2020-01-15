using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumDotNetCoreSample
{
    public class CSVDataHandler
    {

        /// <summary>
        /// This method is used to get read the test data and generate a list of dictionary 
        /// objects based on the number of records in the test data file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="TestDataNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <returns></returns>
        public static IEnumerable<TestCaseData> GetCSVData(String fileName)
        {
            // Variables to store the filepath and the directory path
            string FilePath = null; ;
            string DirPath=Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
            // get all the files that match the extension as .csv in the parent and all the child directories
            String[] Files= Directory.GetFiles(@DirPath, "*.csv",SearchOption.AllDirectories);
            //Iterate through the loop to see if the userdefined file name matches with the files and then retuen
            foreach (var file in Files)
            {
                if (Path.GetFileNameWithoutExtension(file).Equals(fileName))
                {
                     FilePath = file;
                     break;
                }
            }
            // Read the text and throw execptions if the file/directory is not found
            String FileContent;
            try
            {
                FileContent = File.ReadAllText(FilePath);
            }
            catch (DirectoryNotFoundException)
            {
                throw new TestDataNotFoundException("Test Data path to access the file does not exist." + DirPath);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Test Data file does not exist at this path " + DirPath +" and file name "+fileName );
            }
            catch(ArgumentNullException)
            {
                throw new FileNotFoundException("Test Data file does not exist at this path " + DirPath + " and file name " + fileName);
            }
            //Split the text with "\n" and get all the rows and split the first row with "," to get the header
            String[] FileRows = FileContent.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int DataRows = FileRows.Length;
            int ColCount = FileRows[0].Split(',').Length;
            string[] Colums = FileRows[0].Split(',');
            string[] ColNames = FileRows[0].Split(',');
            //loop through the data rows to genearte the dictionary and yield data rows
            for (int i = 1; i < DataRows; i++)
            {
                Dictionary<string, string> Dict = new Dictionary<string, string>();
                string[] RowData = FileRows[i].Split(',');
                for (int j = 0; j < ColCount; j++)
                {
                    Dict.Add(Colums[j], RowData[j]);
                }
                // Test name is generated based on the method name with the text Dataset and the row number from the csv file
                var TestData = new TestCaseData(Dict);
                TestData.SetName( "{m}-Dataset" + i);
                yield return TestData;
            }
        }
    }
}
