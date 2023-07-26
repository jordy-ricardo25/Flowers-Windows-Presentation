using System.Windows.Navigation;
using System.Windows.Controls;
using Flowers.Controls;
using MSB.UI.Controls;
using System.Windows;
using Flowers.Helpers;
using Flowers.Models;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace Flowers.SubViews
{
    /// <summary>
    /// Interaction logic for InsertPage.xaml
    /// </summary>
    sealed partial class InsertPage : AeroPage
    {
        public InsertPage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CBType.SelectedIndex = 0;
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.GetCurrentView().Flyout.Closing += OnFlyoutClosing;
        }

        public override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.GetCurrentView().Flyout.Closing -= OnFlyoutClosing;
        }

        private void OnFlyoutClosing(object sender, FlyoutClosingEventArgs args)
        {
            var filled = InputValidator.AreAnyInputFilled(current_panel?.Children);

            if (filled)
                args.Cancel = AlertsManager.ShowCustomAlert("Operación en proceso",
                                                            "Existe información en el formulario, ¿desea descartarla?",
                                                            MessageDialogButton.YesNo) is MessageDialogResult.No;
        }

        public async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputValidator.AreAllInputsFilled(current_panel.Children))
            {
                AlertsManager.ShowIncompleteDataAlert();
                return;
            }

            switch (CBType.SelectedIndex)
            {
                case 0:
                    {
                        if (!int.TryParse((SPClient.Children[1] as TextBox).Text, out int cod_cliente))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de cliente");
                            return;
                        }
                        if (!int.TryParse((SPClient.Children[25] as TextBox).Text, out int cod_rep_ventas))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de representante de ventas");
                            return;
                        }
                        if (!decimal.TryParse((SPClient.Children[27] as TextBox).Text, out decimal limite_credito))
                        {
                            AlertsManager.ShowBadFormatAlert("Límite de crédito");
                            return;
                        }

                        if (await Cliente.Insertar(new Cliente
                        {
                            Codigo = cod_cliente,
                            Nombre = (SPClient.Children[3] as TextBox).Text,
                            NombreContacto = (SPClient.Children[5] as TextBox).Text,
                            ApellidoContacto = (SPClient.Children[7] as TextBox).Text,
                            Telefono = (SPClient.Children[9] as TextBox).Text,
                            Fax = (SPClient.Children[11] as TextBox).Text,
                            Direccion1 = (SPClient.Children[13] as TextBox).Text,
                            Direccion2 = (SPClient.Children[15] as TextBox).Text,
                            Ciudad = (SPClient.Children[17] as TextBox).Text,
                            Region = (SPClient.Children[19] as TextBox).Text,
                            Pais = (SPClient.Children[21] as TextBox).Text,
                            CodigoPostal = (SPClient.Children[23] as TextBox).Text,
                            CodigoRepresentanteVentas = cod_rep_ventas,
                            LimiteCredito = limite_credito
                        }) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPClient.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 1:
                    {
                        if (!int.TryParse((SPEmployee.Children[1] as TextBox).Text, out int codigo))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de empleado");
                            return;
                        }

                        if (!int.TryParse((SPEmployee.Children[15] as TextBox).Text, out int codigo_jefe))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de jefe");
                            return;
                        }

                        if (await Empleado.Insertar(new Empleado
                        {
                            Codigo = codigo,
                            Nombre = (SPEmployee.Children[3] as TextBox).Text,
                            Apellido1 = (SPEmployee.Children[5] as TextBox).Text,
                            Apellido2 = (SPEmployee.Children[7] as TextBox).Text,
                            Extension = (SPEmployee.Children[9] as TextBox).Text,
                            Email = (SPEmployee.Children[11] as TextBox).Text,
                            CodigoOficina = (SPEmployee.Children[13] as TextBox).Text,
                            CodigoJefe = codigo_jefe,
                            Puesto = (SPEmployee.Children[17] as TextBox).Text
                        }) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPEmployee.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 2:
                    {
                        // TODO: create a new office object...
                        var oficina = new Oficina
                        {
                            Codigo = (SPOffice.Children[1] as TextBox).Text,
                            Ciudad = (SPOffice.Children[3] as TextBox).Text,
                            Pais = (SPOffice.Children[5] as TextBox).Text,
                            Region = (SPOffice.Children[7] as TextBox).Text,
                            CodigoPostal = (SPOffice.Children[9] as TextBox).Text,
                            Telefono = (SPOffice.Children[11] as TextBox).Text,
                            Direccion1 = (SPOffice.Children[13] as TextBox).Text,
                            Direccion2 = (SPOffice.Children[15] as TextBox).Text,
                        };

                        // try to insert the office into the database and show the corresponding alert
                        if (await Oficina.Insertar(oficina) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPOffice.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 3:
                    {
                        if (!short.TryParse((SPProduct.Children[13] as TextBox).Text, out short stock))
                        {
                            AlertsManager.ShowBadFormatAlert("Stock");
                            return;
                        }
                        if (!decimal.TryParse((SPProduct.Children[15] as TextBox).Text, out decimal precio_venta))
                        {
                            AlertsManager.ShowBadFormatAlert("Precio de venta");
                            return;
                        }
                        if (!decimal.TryParse((SPProduct.Children[17] as TextBox).Text, out decimal precio_compra))
                        {
                            AlertsManager.ShowBadFormatAlert("Precio de compra");
                            return;
                        }

                        // TODO: create a new product object...
                        var producto = new Producto
                        {
                            Codigo = (SPProduct.Children[1] as TextBox).Text,
                            Nombre = (SPProduct.Children[3] as TextBox).Text,
                            Gama = (SPProduct.Children[5] as TextBox).Text,
                            Dimensiones = (SPProduct.Children[7] as TextBox).Text,
                            Proveedor = (SPProduct.Children[9] as TextBox).Text,
                            Descripcion = (SPProduct.Children[11] as TextBox).Text,
                            Stock = stock,
                            PrecioVenta = precio_venta,
                            PrecioProveedor = precio_compra
                        };

                        // try to insert the product into the database and show the corresponding alert
                        if (await Producto.Insertar(producto) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPProduct.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 4:
                    {
                        // IMPORTANT: this is a workaround to fix the date format issue...
                        if (ci is null)
                        {
                            ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);

                            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                            Thread.CurrentThread.CurrentCulture = ci;
                        }

                        if (!int.TryParse((SPOrder.Children[1] as TextBox).Text, out int cod_pedido))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de pedido");
                            return;
                        }
                        if (!int.TryParse((SPOrder.Children[13] as TextBox).Text, out int cod_cliente))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de cliente");
                            return;
                        }

                        var pedido = new Pedido
                        {
                            Codigo = cod_pedido,
                            FechaPedido = (SPOrder.Children[3] as DatePicker).SelectedDate.Value,
                            FechaEsperada = (SPOrder.Children[5] as DatePicker).SelectedDate.Value,
                            FechaEntrega = (SPOrder.Children[7] as DatePicker).SelectedDate.HasValue ? (SPOrder.Children[7] as DatePicker).SelectedDate.Value : null,
                            Estado = (SPOrder.Children[9] as TextBox).Text,
                            Comentarios = (SPOrder.Children[11] as TextBox).Text,
                            CodigoCliente = cod_cliente,
                        };

                        if (await Pedido.Insertar(pedido) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPOrder.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 5:
                    {
                        // IMPORTANT: this is a workaround to fix the date format issue...
                        if (ci is null)
                        {
                            ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);

                            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                            Thread.CurrentThread.CurrentCulture = ci;
                        }

                        if (!int.TryParse((SPPayment.Children[1] as TextBox).Text, out int cod_cliente))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de cliente");
                            return;
                        }
                        if (!decimal.TryParse((SPPayment.Children[9] as TextBox).Text, out decimal total))
                        {
                            AlertsManager.ShowBadFormatAlert("Total");
                            return;
                        }

                        var pago = new Pago
                        {
                            IdTransaccion = (SPPayment.Children[5] as TextBox).Text,
                            CodigoCliente = cod_cliente,
                            FormaPago = (SPPayment.Children[3] as TextBox).Text,
                            Fecha = (SPPayment.Children[7] as DatePicker).SelectedDate.Value,
                            Total = total
                        };

                        if (await Pago.Insertar(pago) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPPayment.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 6:
                    {
                        if (!int.TryParse((SPOrderDetail.Children[1] as TextBox).Text, out int cod_pedido))
                        {
                            AlertsManager.ShowBadFormatAlert("Código de pedido");
                            return;
                        }
                        if (!int.TryParse((SPOrderDetail.Children[5] as TextBox).Text, out int cantidad))
                        {
                            AlertsManager.ShowBadFormatAlert("Cantidad");
                            return;
                        }
                        if (!decimal.TryParse((SPOrderDetail.Children[7] as TextBox).Text, out decimal precio))
                        {
                            AlertsManager.ShowBadFormatAlert("Precio unitario");
                            return;
                        }
                        if (!short.TryParse((SPOrderDetail.Children[9] as TextBox).Text, out short num_linea))
                        {
                            AlertsManager.ShowBadFormatAlert("Número de linea");
                            return;
                        }

                        // TODO: create a new order detail object...
                        var detalle = new DetallePedido
                        {
                            CodigoPedido = cod_pedido,
                            CodigoProducto = (SPOrderDetail.Children[3] as TextBox).Text,
                            Cantidad = cantidad,
                            PrecioUnidad = precio,
                            NumeroLinea = num_linea
                        };

                        // try to insert the order detail into the database and show the corresponding alert
                        if (await DetallePedido.Insertar(detalle) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPOrderDetail.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
                case 7:
                    {
                        // TODO: create a new product brand object...
                        var gama = new GamaProducto
                        {
                            Gama = (SPProductBrand.Children[1] as TextBox).Text,
                            DescripcionTXT = (SPProductBrand.Children[3] as TextBox).Text,
                            DescripcionHTML = (SPProductBrand.Children[5] as TextBox).Text,
                            Imagen = (SPProductBrand.Children[7] as TextBox).Text
                        };

                        // try to insert the product brand into the database and show the corresponding alert
                        if (await GamaProducto.Insertar(gama) > 0)
                        {
                            AlertsManager.ShowSuccessfullOperationAlert();
                            InputValidator.ResetInputs(SPProductBrand.Children);
                        }
                        else
                            AlertsManager.ShowSomethingWentWrongAlert();
                    }
                    break;
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.GetCurrentView().Flyout.IsOpen = false;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBType.SelectedItem is null)
            {
                SPMain.Children.OfType<StackPanel>().ToList().ForEach(panel => panel.Visibility = Visibility.Collapsed);
                current_panel = null;

                return;
            }

            if (current_panel is not null)
            {
                InputValidator.ResetInputs(current_panel.Children);
                current_panel.Visibility = Visibility.Collapsed;
            }

            var panel = SPMain.Children[CBType.SelectedIndex + 2] as StackPanel;

            panel.Visibility = Visibility.Visible;

            current_panel = panel;
        }

        CultureInfo ci = null;
        StackPanel current_panel = null;
    }
}
