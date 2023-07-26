using System.Threading.Tasks;
using Flowers.Helpers;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents an office inside the application.
    /// </summary>
    sealed class Oficina
    {
        #region Properties

        /// <summary>
        /// Gets or sets the office's code.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Gets or sets the office's city.
        /// </summary>
        public string Ciudad { get; set; }

        /// <summary>
        /// Gets or sets the office's country.
        /// </summary>
        public string Pais { get; set; }

        /// <summary>
        /// Gets or sets the office's region or state.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the office's postal code.
        /// </summary>
        public string CodigoPostal { get; set; }

        /// <summary>
        /// Gets or sets the office's phone number.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Gets or sets the office's main address.
        /// </summary>
        public string Direccion1 { get; set; }

        /// <summary>
        /// Gets or sets the office's secondary address.
        /// </summary>
        public string Direccion2 { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new office into the database.
        /// </summary>
        /// <param name="oficina">The office to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(Oficina oficina)
        {
            var query = "INSERT INTO oficina VALUES (@codigo_oficina, @ciudad, @pais, @region, @codigo_postal, @telefono, @linea_direccion1, @linea_direccion2);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("codigo_oficina", oficina.Codigo);
            cmd.Parameters.AddWithValue("ciudad", oficina.Ciudad);
            cmd.Parameters.AddWithValue("pais", oficina.Pais);
            cmd.Parameters.AddWithValue("region", oficina.Region);
            cmd.Parameters.AddWithValue("codigo_postal", oficina.CodigoPostal);
            cmd.Parameters.AddWithValue("telefono", oficina.Telefono);
            cmd.Parameters.AddWithValue("linea_direccion1", oficina.Direccion1);
            cmd.Parameters.AddWithValue("linea_direccion2", oficina.Direccion2);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
