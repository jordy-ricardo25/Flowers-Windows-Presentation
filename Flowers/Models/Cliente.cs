using System.Threading.Tasks;
using Flowers.Helpers;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents a customer inside the application.
    /// </summary>
    sealed class Cliente
    {
        #region Properties

        /// <summary>
        /// Gets or sets the customer's ID.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the customer's contact name.
        /// </summary>
        public string NombreContacto { get; set; }

        /// <summary>
        /// Gets or sets the customer's contact last name.
        /// </summary>
        public string ApellidoContacto { get; set; }

        /// <summary>
        /// Gets or sets the customer's phone number.
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Gets or sets the customer's fax number.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the customer's main address.
        /// </summary>
        public string Direccion1 { get; set; }

        /// <summary>
        /// Gets or sets the customer's secondary address.
        /// </summary>
        public string Direccion2 { get; set; }

        /// <summary>
        /// Gets or sets the customer's city.
        /// </summary>
        public string Ciudad { get; set; }

        /// <summary>
        /// Gets or sets the customer's region or state.
        /// </summary>
        public string Region { get; set; }
        
        /// <summary>
        /// Gets or sets the customer's country.
        /// </summary>
        public string Pais { get; set; }

        /// <summary>
        /// Gets or sets the customer's postal code.
        /// </summary>
        public string CodigoPostal { get; set; }

        /// <summary>
        /// Gets or sets the customer's sales representative.
        /// </summary>
        public int CodigoRepresentanteVentas { get; set; }

        /// <summary>
        /// Gets or sets the customer's credit limit.
        /// </summary>
        public decimal LimiteCredito { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new customer into the database.
        /// </summary>
        /// <param name="cliente">The customer to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(Cliente cliente)
        {
            var query = "INSERT INTO cliente VALUES (@codigo_cliente, @nombre_cliente, @nombre_contacto, @apellido_contacto, @telefono, @fax, @linea_direccion1, @linea_direccion2, @ciudad, @region, @pais, @codigo_postal, @codigo_empleado_rep_ventas, @limite_credito);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("codigo_cliente", cliente.Codigo);
            cmd.Parameters.AddWithValue("nombre_cliente", cliente.Nombre);
            cmd.Parameters.AddWithValue("nombre_contacto", cliente.NombreContacto);
            cmd.Parameters.AddWithValue("apellido_contacto", cliente.ApellidoContacto);
            cmd.Parameters.AddWithValue("telefono", cliente.Telefono);
            cmd.Parameters.AddWithValue("fax", cliente.Fax);
            cmd.Parameters.AddWithValue("linea_direccion1", cliente.Direccion1);
            cmd.Parameters.AddWithValue("linea_direccion2", cliente.Direccion2);
            cmd.Parameters.AddWithValue("ciudad", cliente.Ciudad);
            cmd.Parameters.AddWithValue("region", cliente.Region);
            cmd.Parameters.AddWithValue("pais", cliente.Region);
            cmd.Parameters.AddWithValue("codigo_postal", cliente.CodigoPostal);
            cmd.Parameters.AddWithValue("codigo_empleado_rep_ventas", cliente.CodigoRepresentanteVentas);
            cmd.Parameters.AddWithValue("limite_credito", cliente.LimiteCredito);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        #endregion
    }
}
