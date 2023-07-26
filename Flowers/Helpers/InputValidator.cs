using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System;

namespace Flowers.Helpers
{
    static class InputValidator
    {
        #region Properties

        /// <summary>
        /// Gets the value of the InputType dependency property.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static InputType GetInputType(FrameworkElement target)
            => (InputType)target.GetValue(InputTypeProperty);

        /// <summary>
        /// Sets the value of the InputType dependency property.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static void SetInputType(FrameworkElement target, InputType value)
            => target.SetValue(InputTypeProperty, value);

        #endregion

        #region Dependency properties

        /// <summary>
        /// Indentifies the InputType dependency property.
        /// </summary>
        public static readonly DependencyProperty InputTypeProperty =
                DependencyProperty.RegisterAttached("InputType", typeof(InputType), typeof(InputValidator), new PropertyMetadata(InputType.Default, InputTypeChanged_Callback));

        #endregion

        #region Callbacks

        private static void InputTypeChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                if (d is TextBox tb)
                    tb.PreviewTextInput += OnPreviewTextInput;
                else if (d is PasswordBox pb)
                    pb.PreviewTextInput += OnPreviewTextInput;
                else
                    throw new NotSupportedException();
            }
        }
        #endregion

        #region Methods

        private static void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var target = sender as FrameworkElement;
            var input = (InputType)target.GetValue(InputTypeProperty);

            e.Handled = input is InputType.Integer ? !valInteger.IsMatch(e.Text) : !valDecimal.IsMatch(e.Text);
        }

        /// <summary>
        /// Checks if all inputs are filled.
        /// </summary>
        /// <param name="elements">The collection of elements to check.</param>
        /// <returns>**<see langword="true"/>** if all inputs are filled; otherwise, **<see langword="false"/>**.</returns>
        public static bool AreAllInputsFilled(UIElementCollection elements)
        {
            var unfilled = (from UIElement element in elements
                            where
                                element is TextBox tb && tb.Text is "" && tb.Tag is "Requested" ||
                                element is PasswordBox pb && pb.Password is "" && pb.Tag is "Requested" ||
                                element is DatePicker dp && dp.SelectedDate is null && dp.Tag is "Requested"
                            select element).FirstOrDefault();

            return unfilled is null;
        }

        /// <summary>
        /// Checks if any input is filled.
        /// </summary>
        /// <param name="elements">The collection of elements to check.</param>
        /// <returns>**<see langword="true"/>** if any input is filled; otherwise, **<see langword="false"/>**.</returns>
        public static bool AreAnyInputFilled(UIElementCollection elements)
        {
            foreach (UIElement element in elements)
            {
                if (element is TextBox tb && tb.Text is not "")
                    return true;
                else if (element is PasswordBox pb && pb.Password is not "")
                    return true;
                else if (element is DatePicker dp && dp.SelectedDate is not null)
                    return true;
            }
            
            return false;
        }

        /// <summary>
        /// Resets all inputs.
        /// </summary>
        /// <param name="elements">The collection of elements to reset.</param>
        public static void ResetInputs(UIElementCollection elements)
        {
            elements.OfType<TextBox>().ToList().ForEach(tb => tb.Text = "");
            elements.OfType<PasswordBox>().ToList().ForEach(pb => pb.Password = "");
            elements.OfType<DatePicker>().ToList().ForEach(dp => dp.SelectedDate = null);
        }

        #endregion

        static readonly Regex valInteger = new(@"^[-+]?\d*$");
        static readonly Regex valDecimal = new(@"^[-+]?\d*(\.\d*)?$");
    }

    /// <summary>
    /// Define constants for the type of input.
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// The default input type.
        /// </summary>
        Default,

        /// <summary>
        /// The input type is integer.
        /// </summary>
        Integer,

        /// <summary>
        /// The input type is decimal.
        /// </summary>
        Decimal
    }
}



