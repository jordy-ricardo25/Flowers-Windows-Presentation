using MSB.UI.Controls;

namespace Flowers.Helpers
{
    static class AlertsManager
    {
        /// <summary>
        /// Show an alert that indicates that some requested data is incomplete.
        /// </summary>
        public static void ShowIncompleteDataAlert()
        {
            new MessageDialog
            {
                Title = "Campos vacios",
                Message = "Los campos marcados con * son obligatorios, por favor llene todos los campos."
            }.Show();
        }

        /// <summary>
        /// Show an alert that indicates that some data has a bad format.
        /// </summary>
        public static void ShowBadFormatAlert(string param)
        {
            new MessageDialog
            {
                Title = "Formato incorrecto",
                Message = $"El campo \"{param}\" tiene un formato incorrecto."
            }.Show();
        }

        public static void ShowSomethingWentWrongAlert()
        {
            new MessageDialog
            {
                Title = "Algo salió mal",
                Message = "No se pudo realizar la operación."
            }.Show();
        }

        public static void ShowSuccessfullOperationAlert()
        {
            new MessageDialog
            {
                Title = "Operación exitosa",
                Message = "La operación se realizó correctamente."
            }.Show();
        }

        public static void ShowNoResultsAlert()
        {
            new MessageDialog
            {
                Title = "Sin resultados",
                Message = "La operación no ha devuelto ningun resultado"
            }.Show();
        }

        /// <summary>
        /// Show an alert with a custom message.
        /// </summary>
        /// <param name="title">The title of the alert.</param>
        /// <param name="msg">The message of the alert.</param>
        /// <param name="btns">The buttons of the alert.</param>
        /// <returns>The <see cref="MessageDialogResult"/> of the alert.</returns>
        public static MessageDialogResult ShowCustomAlert(string title, string msg, MessageDialogButton btns = MessageDialogButton.OK)
        {
            return new MessageDialog
            {
                Title = title,
                Message = msg,
                Buttons = btns
            }.Show();
        }
    }
}
