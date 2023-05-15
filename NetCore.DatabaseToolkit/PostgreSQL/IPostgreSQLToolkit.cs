namespace NetCore.DatabaseToolkit.PostgreSQL
{
    public interface IPostgreSQLToolkit
    {
        /// <summary>
        /// Restore a PostgreSQL database using pg_restore. Make sure the following appsettings.json properties
        /// <see cref="ApplicationOptions.PostgreSQLHost"/>, <see cref="ApplicationOptions.PostgreSQLPort"/>,
        /// and <see cref="ApplicationOptions.PostgreSQLUser"/> are set and match exactly what's in your pgpass.conf file (Windows).
        /// </summary>
        /// <param name="databaseName">The name of the database on the Postgres server we're restoring.</param>
        /// <param name="localDatabasePath">The local file path to the .sql database file where we're restoring from.</param>
        void RestoreDatabase(string databaseName, string localDatabasePath);

        /// <summary>
        /// Backup a PostgreSQL database using pg_dump. Make sure the following appsettings.json properties
        /// <see cref="ApplicationOptions.PostgreSQLHost"/>, <see cref="ApplicationOptions.PostgreSQLPort"/>,
        /// and <see cref="ApplicationOptions.PostgreSQLUser"/> are set and match exactly what's in your pgpass.conf file (Windows).
        /// </summary>
        /// <param name="databaseName">The name of the database on the Postgres server we're backing up.</param>
        /// <param name="localDatabasePath">The local file path to the .sql database file where the backup is saved.</param>
        void BackupDatabase(string databaseName, string localDatabasePath);
    }
}
