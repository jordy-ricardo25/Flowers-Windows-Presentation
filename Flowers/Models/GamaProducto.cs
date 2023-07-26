using System.Threading.Tasks;
using Flowers.Helpers;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents a product's brand inside the application.
    /// </summary>
    sealed class GamaProducto
    {
        #region Properties

        /// <summary>
        /// Gets or sets the brand's code.
        /// </summary>
        public string Gama { get; set; }

        /// <summary>
        /// Gets or sets the brand's description.
        /// </summary>
        public string DescripcionHTML { get; set; }

        /// <summary>
        /// Gets or sets the brand's description.
        /// </summary>
        public string DescripcionTXT { get; set; }

        /// <summary>
        /// Gets or sets the brand's image.
        /// </summary>
        public string Imagen { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new brand into the database.
        /// </summary>
        /// <param name="gama">The brand to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(GamaProducto gama)
        {
            var query = "INSERT INTO gama_producto VALUES (@gama, @descripcion_texto, @descripcion_html, @imagen);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("gama", gama.Gama);
            cmd.Parameters.AddWithValue("descripcion_texto", gama.DescripcionTXT);
            cmd.Parameters.AddWithValue("descripcion_html", gama.DescripcionHTML);
            cmd.Parameters.AddWithValue("imagen", gama.Imagen);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
