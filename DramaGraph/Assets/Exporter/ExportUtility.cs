using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tool.Export
{
    public static class ExportUtility
    {
        public static bool RunExe(string fileName, string argument, out string errorMsg)
        {
            bool result = false;
            if (string.IsNullOrEmpty(fileName))
            {
                errorMsg = "File Name is Empty";
                return false;
            }
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.Arguments = argument;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;
            errorMsg = string.Empty;
            try
            {
                if (process.Start())
                {
                    errorMsg = process.StandardError.ReadToEnd();
                    if (string.IsNullOrEmpty(errorMsg))
                    {
                        result = true;
                    }
                    process.WaitForExit();
                }
            }
            catch (Exception e)
            {
                int a = 0;
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                }
            }
            return result;
        }
    }
}
