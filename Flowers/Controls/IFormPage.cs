using System.Windows;

namespace Flowers.Controls
{
    public interface IFormPage
    {
        protected void AcceptButton_Click(object sender, RoutedEventArgs e);

        protected void CancelButton_Click(object sender, RoutedEventArgs e);
    }
}
