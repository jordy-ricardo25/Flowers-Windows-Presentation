using System.Data;
using System.Threading.Tasks;
using Flowers.Helpers;
using Npgsql;

namespace Flowers.Models
{
    /// <summary>
    /// Represents an employee inside the application.
    /// </summary>
    sealed class Empleado
    {
        #region Properties

        /// <summary>
        /// Gets or sets the employee's ID.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Gets or sets the employee's name.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Gets or sets the employee's first last name.
        /// </summary>
        public string Apellido1 { get; set; }

        /// <summary>
        /// Gets or sets the employee's second last name.
        /// </summary>
        public string Apellido2 { get; set; }

        /// <summary>
        /// Gets or sets the employee's extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the employee's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the employee's office code.
        /// </summary>
        public string CodigoOficina { get; set; }

        /// <summary>
        /// Gets or sets the employee's boss code.
        /// </summary>
        public int CodigoJefe { get; set; }

        /// <summary>
        /// Gets or sets the employee's position.
        /// </summary>
        public string Puesto { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new employee into the database.
        /// </summary>
        /// <param name="empleado">The employee to insert.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Insertar(Empleado empleado)
        {
            var query = $"INSERT INTO empleado VALUES (@codigo_empleado, @nombre, @apellido1, @apellido2, @extension, @email, @codigo_oficina, @codigo_jefe, @puesto);";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("@codigo_empleado", empleado.Codigo);
            cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
            cmd.Parameters.AddWithValue("@apellido1", empleado.Apellido1);
            cmd.Parameters.AddWithValue("@apellido2", empleado.Apellido2);
            cmd.Parameters.AddWithValue("@extension", empleado.Extension);
            cmd.Parameters.AddWithValue("@email", empleado.Email);
            cmd.Parameters.AddWithValue("@codigo_oficina", empleado.CodigoOficina);
            cmd.Parameters.AddWithValue("@codigo_jefe", empleado.CodigoJefe);
            cmd.Parameters.AddWithValue("@puesto", empleado.Puesto);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        /// <summary>
        /// Updates an existing employee in the database.
        /// </summary>
        /// <param name="empleado">The employee to update.</param>
        /// <returns>The number of rows affected.</returns>
        public static async Task<int> Actualizar(Empleado empleado)
        {
            var query = "UPDATE empleado SET nombre = @nombre, apellido1 = @apellido1, apellido2 = @apellido2, extension = @extension, email = @email,"
                + $" codigo_oficina = @codigo_oficina, codigo_jefe = @codigo_jefe, puesto = @puesto WHERE codigo_empleado = @codigo_empleado;";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("@codigo_empleado", empleado.Codigo);
            cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
            cmd.Parameters.AddWithValue("@apellido1", empleado.Apellido1);
            cmd.Parameters.AddWithValue("@apellido2", empleado.Apellido2);
            cmd.Parameters.AddWithValue("@extension", empleado.Extension);
            cmd.Parameters.AddWithValue("@email", empleado.Email);
            cmd.Parameters.AddWithValue("@codigo_oficina", empleado.CodigoOficina);
            cmd.Parameters.AddWithValue("@codigo_jefe", empleado.CodigoJefe);
            cmd.Parameters.AddWithValue("@puesto", empleado.Puesto);

            return await ConnectionManager.ExecuteNonQueryAsync(cmd);
        }

        /// <summary>
        /// Selects an employee from the database.
        /// </summary>
        /// <param name="codigo">The employee's ID.</param>
        /// <returns>The selected employee if found; otherwise **<see langword="null"/>**.</returns>
        public static async Task<Empleado> Seleccionar(int codigo)
        {
            var query = $"SELECT * FROM empleado WHERE codigo_empleado = @codigo_empleado;";
            var cmd = new NpgsqlCommand(query);

            cmd.Parameters.AddWithValue("@codigo_empleado", codigo);

            var view = await ConnectionManager.ExecuteReaderAsync(cmd);

            if (view.Table.Rows.Count > 0)
            {
                return new Empleado
                {
                    Codigo = view.Table.Rows[0].Field<int>(0),
                    Nombre = view.Table.Rows[0].Field<string>(1),
                    Apellido1 = view.Table.Rows[0].Field<string>(2),
                    Apellido2 = view.Table.Rows[0].Field<string>(3),
                    Extension = view.Table.Rows[0].Field<string>(4),
                    Email = view.Table.Rows[0].Field<string>(5),
                    CodigoOficina = view.Table.Rows[0].Field<string>(6),
                    CodigoJefe = view.Table.Rows[0].Field<int>(7),
                    Puesto = view.Table.Rows[0].Field<string>(8)
                };
            }

            return null;
        }

        #endregion
    }
}
