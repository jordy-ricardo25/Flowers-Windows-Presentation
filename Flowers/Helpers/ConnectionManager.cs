using System.Threading.Tasks;
using System.Data;
using System;
using Npgsql;

namespace Flowers.Helpers
{
    /// <summary>
    /// Provides methods to manage the connection to the database, and execute queries.
    /// </summary>
    static class ConnectionManager
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the connection is open.
        /// </summary>
        public static bool IsConnected
        {
            get => connection.State is ConnectionState.Open;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes a query on the database and returns an integer value that represents the number of rows affected.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <returns>An integer value that represents the number of rows affected.</returns>
        public static async Task<int> ExecuteNonQueryAsync(string query)
        {
            if (!IsConnected)
                await connection.OpenAsync();

            using var command = new NpgsqlCommand(query, connection);

            int result;

            try
            {
                result = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
#endif

                result = -1;
            }

            await connection.CloseAsync();

            return result;
        }

        /// <summary>
        /// Executes a query on the database and returns an integer value that represents the number of rows affected.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>An integer value that represents the number of rows affected.</returns>
        public static async Task<int> ExecuteNonQueryAsync(NpgsqlCommand command)
        {
            if (!IsConnected)
                await connection.OpenAsync();

            command.Connection ??= connection;

            int result;

            try
            {
                result = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
#endif

                result = -1;
            }

            await connection.CloseAsync();

            return result;
        }

        /// <summary>
        /// Executes a query on the database and returns a <see cref="DataView"/> object with the results.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <returns>A <see cref="DataView"/> object with the results.</returns>
        public static async Task<DataView> ExecuteReaderAsync(string query)
        {
            if (!IsConnected)
                await connection.OpenAsync();

            using var command = new NpgsqlCommand(query, connection);

            var table = new DataTable();
            table.Load(await command.ExecuteReaderAsync());

            await connection.CloseAsync();

            return table.DefaultView;
        }

        /// <summary>
        /// Executes a query on the database and returns a <see cref="DataView"/> object with the results.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>A <see cref="DataView"/> object with the results.</returns>
        public static async Task<DataView> ExecuteReaderAsync(NpgsqlCommand command)
        {
            if (!IsConnected)
                await connection.OpenAsync();

            command.Connection ??= connection;

            var table = new DataTable();
            table.Load(await command.ExecuteReaderAsync());

            await connection.CloseAsync();

            return table.DefaultView;
        }

        #endregion

        static readonly NpgsqlConnection connection =
            new("Server = localhost; Port = 5432; Database = Jardineria; User Id = postgres; Password = *****@****;");
    }
}
