using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using Flowers.Controls;
using Flowers.Models;
using Newtonsoft.Json;
using System.IO;
using System;
using Flowers.Helpers;
using MSB.UI.Controls;

namespace Flowers.Views
{
    /// <summary>
    /// Interaction logic for ExercisePage.xaml
    /// </summary>
    sealed partial class ExercisePage : AeroPage
    {
        public ExercisePage()
        {
            this.InitializeComponent();
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (exercises is null)
            {
                Uri uri = new("/Resources/Exercises.json", UriKind.Relative);

                using var stream = Application.GetResourceStream(uri).Stream;

                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                exercises = JsonConvert.DeserializeObject<List<Exercise>>(json);
            }

            if (!int.TryParse((e.ExtraData as string), out index))
                return;
            
            var rootGrid = this.Content as Grid;

            var TBTitle = rootGrid.Children[0] as TextBlock;
            var TBDescription = rootGrid.Children[1] as TextBlock;

            TBTitle.Text = exercises[index - 1].Title;
            TBDescription.Text = exercises[index - 1].Description;
        }

        private async void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            var dataGrid = (this.Content as Grid).Children[3] as DataGrid;

            dataGrid.ItemsSource = await ConnectionManager.ExecuteReaderAsync(index switch
            {
                1 => "SELECT codigo_pedido AS \"Codigo del pedido\", codigo_cliente AS \"Codigo del cliente\", " +
                     "fecha_esperada AS \"Fecha esperada\", fecha_entrega AS \"Fecha de entrega\"" +
                     "FROM pedido WHERE fecha_entrega > fecha_esperada;",
                /***************************************************************************************************/
                2 => "SELECT c.nombre_cliente AS \"Nombre del cliente\", g.gama AS \"Gama del producto\", " +
                     "g.descripcion_texto AS \"Descripcion\"" +
                     "FROM cliente AS c " +
                     "JOIN  pedido AS pe ON (c.codigo_cliente = pe.codigo_cliente) " +
                     "JOIN detalle_pedido AS dp ON (pe.codigo_pedido = dp.codigo_pedido) " +
                     "JOIN producto AS p ON (dp.codigo_producto = p.codigo_producto) " +
                     "JOIN gama_producto AS g ON (p.gama = g.gama) " +
                     "GROUP BY (c.nombre_cliente, g.gama, g.descripcion_texto);",
                /***************************************************************************************************/
                3 => "SELECT e.nombre AS \"Nombre del empleado\" FROM empleado AS e " +
                     "LEFT JOIN oficina AS o ON e.codigo_oficina = o.codigo_oficina " +
                     "WHERE e.codigo_oficina IS NULL;",
                /***************************************************************************************************/
                4 => "SELECT SUM(dp.cantidad * dp.precio_unidad) AS \"Base imponible\", " +
                     "SUM(dp.cantidad * dp.precio_unidad) * 0.21 AS \"IVA\", " +
                     "SUM(dp.cantidad * dp.precio_unidad) + (SUM (dp.cantidad * dp.precio_unidad) * 0.21 ) AS \"TOTAL\" " +
                     "FROM detalle_pedido AS dp;",
                /***************************************************************************************************/
                5 => "SELECT dp.codigo_producto AS \"Codigo del producto\", " +
                     "SUM(dp.cantidad * dp.precio_unidad) AS \"Base imponible\", " +
                     "SUM(dp.cantidad * dp.precio_unidad) * 0.21 AS \"IVA\", " +
                     "SUM(dp.cantidad * dp.precio_unidad) + (SUM(dp.cantidad * dp.precio_unidad) * 0.21) AS \"Total\" " +
                     "FROM detalle_pedido AS dp GROUP BY dp.codigo_producto;",
                /***************************************************************************************************/
                6 => "SELECT nombre AS \"Nombre del producto\" " +
                     "FROM producto WHERE precio_venta = (SELECT MAX(precio_venta) FROM producto);",
                /***************************************************************************************************/
                7 => "SELECT c.nombre_cliente AS \"Nombre del cliente\", " +
                     "rv.nombre AS \"Nombre del representante de ventas\", " +
                     "rv.apellido1 AS \"Apellido del representante de ventas\", " +
                     "o.telefono AS \"Telefono de la oficina\" FROM cliente AS c " +
                     "INNER JOIN empleado AS rv ON c.codigo_empleado_rep_ventas = rv.codigo_empleado " +
                     "JOIN oficina AS o ON rv.codigo_oficina = o.codigo_oficina " +
                     "WHERE c.codigo_cliente  NOT IN (SELECT codigo_cliente FROM pago);",
                _ => ""
            });

            if (dataGrid.Items.Count < 2)
                AlertsManager.ShowNoResultsAlert();
        }

        int index = 0;
        static List<Exercise> exercises = null;
    }
}
