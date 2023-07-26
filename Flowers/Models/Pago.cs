using System.Threading.Tasks;
using Flowers.Helpers;
using System;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents a payment inside the application.
    /// </summary>
    sealed class Pago
    {
        #region Properties

        /// <summary>
        /// Gets or sets the customer's ID.
        /// </summary>
        public int CodigoCliente { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        public string FormaPago { get; set; }

        /// <summary>
        /// Gets or sets the transaction ID.
        /// </summary>
        public string IdTransaccion { get; set; }

        /// <summary>
        /// Gets or sets the payment date.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Gets or sets the payment amount.
        /// </summary>
        public decimal Total { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new payment into the database.
        /// </summary>
        /// <param name="pago">The payment to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(Pago pago)
        {
            var query = "INSERT INTO pago VALUES (@codigo_cliente, @forma_pago, @id_transaccion, @fecha_pago, @total);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("codigo_cliente", pago.CodigoCliente);
            cmd.Parameters.AddWithValue("forma_pago", pago.FormaPago);
            cmd.Parameters.AddWithValue("id_transaccion", pago.IdTransaccion);
            cmd.Parameters.AddWithValue("fecha_pago", pago.Fecha);
            cmd.Parameters.AddWithValue("total", pago.Total);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        /// <summary>
        /// Deletes a payment from the database.
        /// </summary>
        /// <param name="id">The transaction ID of the payment to delete.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Eliminar(string id)
        {
            var query = "DELETE FROM pago WHERE id_transaccion = @id_transaccion;";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("id_transaccion", id);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
