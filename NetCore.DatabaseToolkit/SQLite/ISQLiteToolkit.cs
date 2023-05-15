using System;
using System.Diagnostics;
using System.IO;

namespace NetCore.DatabaseToolkit.SQLite
{
    public interface ISQLiteToolkit
    {
        /// <summary>
        /// Backup a sqlite database using sqlite3 and the .backup dot command.
        /// </summary>
        /// <param name="databaseName">This is the database you connect to including extension.</param>
        /// <param name="localDatabasePath">The path where the backup is stored including extension.</param>
        /// <returns></returns>
        (int,string) BackupDatabase(string databaseName, string localDatabasePath);

        /// <summary>
        /// Restore a sqlite database using sqlite3 and the .restore dot command.
        /// </summary>
        /// <param name="databaseName">This is the database you connect to including extension.</param>
        /// <param name="localDatabasePath">The path where the backup is stored including extension.</param>
        /// <returns></returns>
        (int,string) RestoreDatabase(string databaseName, string localDatabasePath);
    }
}
