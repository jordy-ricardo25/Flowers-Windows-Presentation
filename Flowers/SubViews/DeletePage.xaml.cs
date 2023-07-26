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
    /// Interaction logic for DeletePage.xaml
    /// </summary>
    sealed partial class DeletePage : AeroPage, IFormPage
    {
        public DeletePage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            currentPanel = (this.Content as Grid).Children[1] as StackPanel;
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
            var filled = InputValidator.AreAnyInputFilled(currentPanel.Children);

            if (filled)
                args.Cancel = AlertsManager.ShowCustomAlert("Operación en proceso",
                                                            "Existe información en el formulario, ¿desea descartarla?",
                                                            MessageDialogButton.YesNo) is MessageDialogResult.No;
        }

        public async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputValidator.AreAllInputsFilled(currentPanel.Children))
            {
                AlertsManager.ShowIncompleteDataAlert();
                return;
            }

            if (AlertsManager.ShowCustomAlert("Acción potencialmente peligrosa",
                                              "¿Está seguro de que desea eliminar este registro?",
                                              MessageDialogButton.YesNo) is MessageDialogResult.Yes)
            {
                if (await Pago.Eliminar((currentPanel.Children[1] as TextBox).Text) > 0)
                {
                    AlertsManager.ShowSuccessfullOperationAlert();
                    InputValidator.ResetInputs(currentPanel.Children);
                }
                else
                {
                    AlertsManager.ShowSomethingWentWrongAlert();
                }
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.GetCurrentView().Flyout.IsOpen = false;
        }

        StackPanel currentPanel = null;
    }
}
