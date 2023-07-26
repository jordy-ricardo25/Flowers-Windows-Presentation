using System.Threading.Tasks;
using Flowers.Helpers;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents an order's detail inside the application.
    /// </summary>
    sealed class DetallePedido
    {
        #region Properties

        /// <summary>
        /// Gets or sets the order's ID.
        /// </summary>
        public int CodigoPedido { get; set; }

        /// <summary>
        /// Gets or sets the product's code.
        /// </summary>
        public string CodigoProducto { get; set; }

        /// <summary>
        /// Gets or sets the product's quantity.
        /// </summary>
        public int Cantidad { get; set; }

        /// <summary>
        /// Gets or sets the product's unit price.
        /// </summary>
        public decimal PrecioUnidad { get; set; }

        /// <summary>
        /// Gets or sets the product's line number.
        /// </summary>
        public short NumeroLinea { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new order's detail into the database.
        /// </summary>
        /// <param name="detalle">The order's detail to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(DetallePedido detalle)
        {
            var query = "INSERT INTO detalle_pedido VALUES (@codigo_pedido, @codigo_producto, @cantidad, @precio_unidad, @numero_linea);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("codigo_pedido", detalle.CodigoPedido);
            cmd.Parameters.AddWithValue("codigo_producto", detalle.CodigoProducto);
            cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("precio_unidad", detalle.PrecioUnidad);
            cmd.Parameters.AddWithValue("numero_linea", detalle.NumeroLinea);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
