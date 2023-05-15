using System;
using System.Diagnostics;
using System.IO;

namespace NetCore.DatabaseToolkit.SQLite
{
    public class SQLiteToolkit : ISQLiteToolkit
    {
        /// <summary>
        /// Backup a sqlite database using sqlite3 and the .backup dot command.
        /// </summary>
        /// <param name="databaseName">This is the database you connect to including extension.</param>
        /// <param name="localDatabasePath">The path where the backup is stored including extension.</param>
        /// <returns></returns>
        public (int,string) BackupDatabase(string databaseName, string localDatabasePath)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";

            // sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $@"/c sqlite3 ""{databaseName}"" "".backup ""{localDatabasePath.Replace(@"\", @"\\")}""""";
            startInfo.RedirectStandardOutput= true;
            startInfo.CreateNoWindow= true;
            startInfo.WindowStyle= ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            int statusCode = process.ExitCode;
            string output = process.StandardOutput.ReadToEnd();
            process.Close();

            return (statusCode, output);
        }

        /// <summary>
        /// Restore a sqlite database using sqlite3 and the .restore dot command.
        /// </summary>
        /// <param name="databaseName">This is the database you connect to including extension.</param>
        /// <param name="localDatabasePath">The path where the backup is stored including extension.</param>
        /// <returns></returns>
        public (int,string) RestoreDatabase(string databaseName, string localDatabasePath)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";

            // sqlite needs to have two slashes instead of one for the path it uses inside sqlite3
            startInfo.Arguments = $@"/c sqlite3 ""{databaseName}"" "".restore ""{localDatabasePath.Replace(@"\", @"\\")}""""";
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            int statusCode = process.ExitCode;
            string output = process.StandardOutput.ReadToEnd();
            process.Close();

            return (statusCode, output);
        }
    }
}
