using System.Windows.Navigation;
using System.Windows.Controls;
using Flowers.Controls;
using MSB.UI.Controls;
using System.Windows;
using Flowers.Helpers;
using Flowers.Models;

namespace Flowers.SubViews
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    sealed partial class UpdatePage : AeroPage
    {
        public UpdatePage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewer =(this.Content as Grid).Children[1] as ScrollViewer;

            search_panel = (viewer.Content as StackPanel).Children[1] as StackPanel;
            main_panel = (viewer.Content as StackPanel).Children[3] as StackPanel;
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
            var filled = InputValidator.AreAnyInputFilled(main_panel?.Children);

            if (filled)
                args.Cancel = AlertsManager.ShowCustomAlert("Operación en proceso",
                                                            "Existe información en el formulario, ¿desea descartarla?",
                                                            MessageDialogButton.YesNo) is MessageDialogResult.No;
        }

        public async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputValidator.AreAllInputsFilled(search_panel.IsEnabled ? search_panel.Children : main_panel.Children))
            {
                AlertsManager.ShowIncompleteDataAlert();
                return;
            }

            if (!main_panel.IsEnabled)
                return;

            if (!int.TryParse((main_panel.Children[13] as TextBox).Text, out int codigo_jefe))
            {
                AlertsManager.ShowBadFormatAlert("Código del jefe");
                return;
            }
            
            if (await Empleado.Actualizar(new Empleado
            {
                Codigo = codigo_empleado,
                Nombre = (main_panel.Children[1] as TextBox).Text,
                Apellido1 = (main_panel.Children[3] as TextBox).Text,
                Apellido2 = (main_panel.Children[5] as TextBox).Text,
                Extension = (main_panel.Children[7] as TextBox).Text,
                Email = (main_panel.Children[9] as TextBox).Text,
                CodigoOficina = (main_panel.Children[11] as TextBox).Text,
                CodigoJefe = codigo_jefe,
                Puesto = (main_panel.Children[15] as TextBox).Text
            }) > 0)
            {
                AlertsManager.ShowSuccessfullOperationAlert();
                InputValidator.ResetInputs(search_panel.Children);
                InputValidator.ResetInputs(main_panel.Children);

                search_panel.IsEnabled = true;
                main_panel.IsEnabled = false;
            }
            else
                AlertsManager.ShowSomethingWentWrongAlert();
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.GetCurrentView().Flyout.IsOpen = false;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse((search_panel.Children[0] as TextBox).Text, out int codigo))
            {
                AlertsManager.ShowBadFormatAlert("Código");
                return;
            }

            if (await Empleado.Seleccionar(codigo) is not Empleado empleado)
            {
                AlertsManager.ShowNoResultsAlert();
                return;
            }

            search_panel.IsEnabled = false;

            codigo_empleado = empleado.Codigo;

            (main_panel.Children[1] as TextBox).Text = empleado.Nombre;
            (main_panel.Children[3] as TextBox).Text = empleado.Apellido1;
            (main_panel.Children[5] as TextBox).Text = empleado.Apellido2;
            (main_panel.Children[7] as TextBox).Text = empleado.Extension;
            (main_panel.Children[9] as TextBox).Text = empleado.Email;
            (main_panel.Children[11] as TextBox).Text = empleado.CodigoOficina;
            (main_panel.Children[13] as TextBox).Text = empleado.CodigoJefe.ToString();
            (main_panel.Children[15] as TextBox).Text = empleado.Puesto;


            main_panel.IsEnabled = true;
        }

        int codigo_empleado = -1;
        StackPanel search_panel = null;
        StackPanel main_panel = null;
    }
}
