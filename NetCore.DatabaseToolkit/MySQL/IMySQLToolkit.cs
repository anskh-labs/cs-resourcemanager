using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.DatabaseToolkit.MySQL
{
    public interface IMySQLToolkit
    {
        /// <summary>
        /// Restore a MySQL database using mysql. Make sure the appsettings.json properties
        /// <see cref="ApplicationOptions.MySqlDefaultsFilePath"/> points to your my.ini (Windows) file.
        /// </summary>
        /// <param name="databaseName">The name of the database on the MySQL server to restore.</param>
        /// <param name="localDatabasePath">The local path to the .sql database file that's being restored.</param>
        void RestoreDatabase(string databaseName, string localDatabasePath);

        /// <summary>
        /// Backup a MySQL database using mysqldump. Make sure the appsettings.json properties
        /// <see cref="ApplicationOptions.MySqlDefaultsFilePath"/> points to your my.ini (Windows) file
        /// and <see cref="ApplicationOptions.MySqlDumpPath" /> points to your mysqldump.exe file.
        /// </summary>
        /// <param name="databaseName">The name of the database on the MySQL server.</param>
        /// <param name="localDatabasePath">The local path to the .sql database file where the backup will be saved.</param>
        void BackupDatabase(string databaseName, string localDatabasePath);
    }
}
