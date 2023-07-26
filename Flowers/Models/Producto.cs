using System.Threading.Tasks;
using Flowers.Helpers;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents a product inside the application.
    /// </summary>
    sealed class Producto
    {
        #region Properties

        /// <summary>
        /// Gets or sets the product's code.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Gets or sets the product's name.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the product's brand.
        /// </summary>
        public string Gama { get; set; }

        /// <summary>
        /// Gets or sets the product's scale.
        /// </summary>
        public string Dimensiones { get; set; }

        /// <summary>
        /// Gets or sets the product's provider.
        /// </summary>
        public string Proveedor { get; set; }

        /// <summary>
        /// Gets or sets the product's description.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Gets or sets the product's stock.
        /// </summary>
        public short Stock { get; set; }

        /// <summary>
        /// Gets or sets the product's price.
        /// </summary>
        public decimal PrecioVenta { get; set; }

        /// <summary>
        /// Gets or sets the product's provider price.
        /// </summary>
        public decimal PrecioProveedor { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new product into the database.
        /// </summary>
        /// <param name="producto">The product to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(Producto producto)
        {
            var query = "INSERT INTO producto VALUES (@codigo_producto, @nombre, @gama, @dimensiones, @proveedor, @descripcion, @cantidad_en_stock, @precio_venta, @precio_proveedor);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("codigo_producto", producto.Codigo);
            cmd.Parameters.AddWithValue("nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("gama", producto.Gama);
            cmd.Parameters.AddWithValue("dimensiones", producto.Dimensiones);
            cmd.Parameters.AddWithValue("proveedor", producto.Proveedor);
            cmd.Parameters.AddWithValue("descripcion", producto.Descripcion);
            cmd.Parameters.AddWithValue("cantidad_en_stock", producto.Stock);
            cmd.Parameters.AddWithValue("precio_venta", producto.PrecioVenta);
            cmd.Parameters.AddWithValue("precio_proveedor", producto.PrecioProveedor);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
