using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumDotNetCoreSample
{
    public class FileUtil
    {
        public static String ResultsFilePath;
        public static String ScreenShotFilePath;
        public static String logFilePath;        
        protected  int passCount = 0;
        protected  int failCount = 0;
        public FileUtil()
        {

        }
        /// <summary>
        /// This method is used to set the file path for test results and screenshots
        /// </summary>
        public void SetFilePath()
        {
            String rootPath = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Results"));
            String folderName = DateTime.Now.ToString("dd-MM-yyyy-(hh-mm-ss)");
            String folderPath= Path.Combine(rootPath,folderName);
            Directory.CreateDirectory(folderPath);
            ResultsFilePath = folderPath;
            ScreenShotFilePath = Path.Combine(ResultsFilePath, "screenshots");
            Directory.CreateDirectory(ScreenShotFilePath);
            logFilePath = System.IO.Path.Combine(ResultsFilePath, "log.txt");
        }

        /// <summary>
        /// This method gets the screenshot file path
        /// </summary>
        /// <returns screenshotfilepath></returns>
        public String GetScreenShotPath()
        {
            return ScreenShotFilePath;
        }

        /// <summary>
        /// This method returns the results file path
        /// </summary>
        /// <returns resultsFilePath></returns>
        public String GetResultsFilePath()
        {
            return ResultsFilePath;
        }

        /// <summary>
        /// This methos is used to generate report based on the user options in the resources file
        /// </summary>
        public  void CreateReport()
        {
            //Get the log object and verify if the user wants to write a text report or HTML reprot and call the appropriate method.
            List<String> log= GlobalTestController.GetLog();
            if (TestContext.Parameters["ReportType"]== "HtmlReport")
            {
                CreateHTMLReport(log);
            }else if (TestContext.Parameters["ReportType"] == "TextReport")
            {
                CreateTextReport(log);
            }

        }


        /// <summary>
        /// This method is used to write the log
        /// </summary>
        ///<param name="log"></param>
        public void CreateTextReport(List<String> log)
        {

            StreamWriter writer = File.CreateText(logFilePath);
            int totalTests = log.Count;
            int passCount = 0;
            int failCount = 0;
            foreach (var item in log)
            {
                if (item.Contains("Pass"))
                {
                    passCount = passCount + 1;
                }
                else
                {
                    failCount = failCount + 1;
                }
            }
            writer.WriteLine("****************Automation Execution Log******************** ");
            writer.WriteLine("Execution Summary");
            writer.WriteLine("TotalTestsExecuted  :" + totalTests);
            writer.WriteLine("Passed              :" + passCount);
            writer.WriteLine("Fail                :" + failCount);
            writer.WriteLine("*****************Detailed Log*******************************");
            writer.WriteLine("Test Name" + "\t\t\t\t\t\t\t" + "Error Details" + "\t\t" + "Test Status" + "\t\t" + "Start Time" + "\t\t\t\t" + "End Time");
            foreach (var item in log)
            {
                writer.WriteLine(item);
            }
            writer.Close();

        }
        /// <summary>
        /// This method is used to create the HTML report
        /// </summary>
        /// <param name="log"></param>
        public void CreateHTMLReport(List<String> log)
        {
            List<String> finalResult = new List<string>();
            //genaret the path to the report template
            String reportTeamplatepath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utils", "ReportTemplate.txt"));
            //Read the text and create a report file
            String reader = File.ReadAllText(reportTeamplatepath);
            logFilePath = Path.Combine(ResultsFilePath, "ExecutionReport.html");
            StreamWriter reportFile = File.CreateText(logFilePath);
            //Read the log file and split the data to generate the report 
            foreach (var val in log)
            {
                String[] resData = val.Split(';');
                GetReporterData(resData[0], log, finalResult);
            }
            // read the pass count , failcount and total count and then replace the values in the report.
            reader = reader.Replace("PassCount", passCount.ToString());
            reader = reader.Replace("FailCount", failCount.ToString());
            int totalTests = passCount + failCount;
            reader = reader.Replace("TotalCount", totalTests.ToString());
            reportFile.WriteLine(reader);
            // for each item in the result we will need to write the log in html format using the tags and other background colors
            foreach (var item in finalResult)
            {
                reportFile.WriteLine("<tr>");
                String[] DataSet = item.Split(';');
                foreach (var dString in DataSet)
                {
                    // if the result contains pass then have the background color as green
                    if (dString.Contains("Pass"))
                    {
                        passCount = passCount + 1;
                        reportFile.WriteLine("<td bgcolor=green>" + dString + "</td>");
                    }
                    // if the result contains fail then have the background color as red
                    else if (dString.Contains("Fail"))
                    {
                        failCount = failCount + 1;
                        reportFile.WriteLine("<td bgcolor=Red>" + dString + "</td>");
                    }
                    // if the result contains a screenshot path that ends with.pnf then add the screenshot
                    else if (dString.Contains(".png"))
                    {
                        reportFile.WriteLine("<td><a href = " + dString + " > Screenshot </a></td>");

                    }
                    else
                    {
                        reportFile.WriteLine("<td>" + dString + "</td>");
                    }
                }
                reportFile.WriteLine("</tr>");
            }
            reportFile.WriteLine("</table>");
            reportFile.Close();

        }

        /// <summary>
        /// This method is used to extract the number of retries for each test and also the status of the test as failed or Passed
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private void GetReporterData(String testName, List<String> log,List<String> finalResult)
        {
            bool flag = false;
            // 
                if ((finalResult.Count == 0))
                {
                    ValidateAndGenerateReportInformation(testName, log, finalResult);
                }
                else 
                {
                    foreach (var item in finalResult)
                    {
                        if (item.Contains(testName))
                        {
                         flag = true;
                        }
                    }
                    if (flag==false)
                    {
                        ValidateAndGenerateReportInformation(testName, log, finalResult);
                    }
                }    
        }
        /// <summary>
        /// This method will validate the log file to filter the retry results and pick a pass result 
        /// if the test case was passed after retry or fail result on the last retry. It will also add the
        /// formatted result to the finalresult list for writing the html file.
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="log"></param>
        /// <param name="finalResult"></param>
        private void ValidateAndGenerateReportInformation(string testName, List<string> log, List<String> finalResult)
        {
            String passResult = null, failResult = null;
            int numberOfRetries = 0;
            // looping through each item in the
            foreach (var item in log)
            {
                if (item.Contains(testName) && item.Contains("Pass"))
                {
                    numberOfRetries = numberOfRetries + 1;
                    passResult = item;
                }
                else if(item.Contains(testName) && item.Contains("Fail"))
                {
                    numberOfRetries = numberOfRetries + 1;
                    failResult = item;
                }

            }
            if (passResult != null)
            {
                passCount = passCount + 1;
                finalResult.Add(passResult + ";" + (numberOfRetries-1));
            }
            else
            {
                failCount = failCount + 1;
                finalResult.Add(failResult + ";" + (numberOfRetries-1)); 
            }
        }
    }
}
