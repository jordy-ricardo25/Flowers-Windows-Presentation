using System.Threading.Tasks;
using Flowers.Helpers;
using System;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents an order inside the application.
    /// </summary>
    sealed class Pedido
    {
        #region Properties

        /// <summary>
        /// Gets or sets the order's ID.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Gets or sets the date when the order was placed.
        /// </summary>
        public DateTime FechaPedido { get; set; }

        /// <summary>
        /// Gets or sets the date when the order was expected.
        /// </summary>
        public DateTime FechaEsperada { get; set; }

        /// <summary>
        /// Gets or sets the date when the order was delivered.
        /// </summary>
        public DateTime? FechaEntrega { get; set; }

        /// <summary>
        /// Gets or sets the order's status.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Gets or sets the order's comments.
        /// </summary>
        public string Comentarios { get; set; }

        /// <summary>
        /// Gets or sets the order's customer ID.
        /// </summary>
        public int CodigoCliente { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new order into the database.
        /// </summary>
        /// <param name="pedido">The order to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(Pedido pedido)
        {
            var query = "INSERT INTO pedido VALUES (@codigo_pedido, @fecha_pedido, @fecha_esperada, @fecha_entrega, @estado, @comentarios, @codigo_cliente);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("codigo_pedido", pedido.Codigo);
            cmd.Parameters.AddWithValue("fecha_pedido", pedido.FechaPedido);
            cmd.Parameters.AddWithValue("fecha_esperada", pedido.FechaEsperada);
            cmd.Parameters.AddWithValue("fecha_entrega", pedido.FechaEntrega.HasValue ? pedido.FechaEntrega : DBNull.Value);
            cmd.Parameters.AddWithValue("estado", pedido.Estado);
            cmd.Parameters.AddWithValue("comentarios", pedido.Comentarios);
            cmd.Parameters.AddWithValue("codigo_cliente", pedido.CodigoCliente);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
