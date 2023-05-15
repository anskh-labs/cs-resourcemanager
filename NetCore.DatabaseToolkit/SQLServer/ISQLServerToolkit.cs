using System;

namespace NetCore.DatabaseToolkit.SQLServer
{
    internal interface ISQLServerToolkit
    {
        /// <summary>
        /// Restore a SQL Server database by first getting the File List from SQL Server using the provided .bak file. 
        /// Then set the database to single user, perform the restore, and set it back to multi user. This uses WITH REPLACE
        /// so be aware it will overwrite the existing database.
        /// </summary>
        /// <param name="databaseName">The name of the database on server.</param>
        /// <param name="localDatabasePath">The local path to the database we're restoring.</param>
        /// <exception cref="ArgumentException">If localDatabasePath doesn't end with .bak.</exception>
        void RestoreDatabase(string databaseName, string localDatabasePath = null);
        /// <summary>
        /// Backup a SQL Server database using WITH FORMAT, be aware this will wipe out the existing media set.
        /// </summary>
        /// <param name="databaseName">The name of the database on server.</param>
        /// <param name="localDatabasePath">The local path to the database we're restoring.</param>
        /// <exception cref="ArgumentException">If localDatabasePath doesn't end with .bak.</exception>
        void BackupDatabase(string databaseName, string localDatabasePath = null);
    }
}
