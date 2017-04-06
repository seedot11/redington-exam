using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NLog;
using RedingtonExam.BusinessObjects;

namespace RedingtonExam.Web
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCombined_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PerformCalculation(ProbablilityOperation.CombinedWith);
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, ex.Message);
            }
        }

        private void btnEither_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PerformCalculation(ProbablilityOperation.EitherOr);
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, ex.Message);
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            txtLog.Text = string.Empty;
        }

        private void txtProb1_OnLostFocus(object sender, RoutedEventArgs e)
        {
            HandleInvalidInput((TextBox) sender);
        }

        private void txtProb2_OnLostFocus(object sender, RoutedEventArgs e)
        {
            HandleInvalidInput((TextBox) sender);
        }

        /// <summary>
        /// Clears the text for the fields which are not related to logging.
        /// </summary>
        private void ClearMainText()
        {
            txtProb1.Text = txtProb2.Text = txtResult.Text = string.Empty;
            HandleInvalidInput(txtProb1);
            HandleInvalidInput(txtProb2);
        }

        /// <summary>
        /// Validates the input to ensure it is a valid probability.
        /// </summary>
        /// <param name="input">The input to validate.</param>
        /// <param name="number">The converted double from the string.</param>
        /// <returns>True if the input is valid, false otherwise.</returns>
        private static bool IsValidInput(string input, out double number)
        {
            return (double.TryParse(input.Trim(), out number) && number >= 0 && number <= 1);
        }

        /// <summary>
        /// Validates the input to ensure it is a valid probability.
        /// </summary>
        /// <param name="input">The input to validate.</param>
        /// <returns>True if the input is valid, false otherwise.</returns>
        private static bool IsValidInput(string input)
        {
            double d;
            return IsValidInput(input, out d);
        }

        /// <summary>
        /// Handles the situation where the input is not valid by changing the offending textbox red and informing
        /// the user via tooltip.
        /// </summary>
        /// <param name="textbox">The TextBox control to check.</param>
        private static void HandleInvalidInput(TextBox textbox)
        {
            if (string.IsNullOrWhiteSpace(textbox.Text) || IsValidInput(textbox.Text))
            {
                textbox.Background = Brushes.White;
                textbox.ToolTip = string.Empty;
            }
            else
            {
                textbox.Background = Brushes.LightSalmon;
                textbox.ToolTip = textbox.Text + " is not a valid value. Please enter a value between 0 and 1.";
            }
        }

        /// <summary>
        /// Using the information entered by the user, performs the relevant calculation
        /// and displays it in the results text box.
        /// </summary>
        /// <param name="operation">The operation to perform with the input.</param>
        private void PerformCalculation(ProbablilityOperation operation)
        {
            double p1, p2;
            if (!IsValidInput(txtProb1.Text, out p1) || !IsValidInput(txtProb2.Text, out p2))
            {
                Log(LogLevel.Info, "Invalid values for probabilities.");
                ClearMainText();
                return;
            }

            switch (operation)
            {
                case ProbablilityOperation.CombinedWith:
                    txtResult.Text = Probablility.CombinedWith(p1, p2).ToString("0.#####");
                    break;
                case ProbablilityOperation.EitherOr:
                    txtResult.Text = Probablility.EitherOr(p1, p2).ToString("0.#####");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, "Invalid operation.");
            }

            Log(LogLevel.Info, operation +  " of " + p1 + " and " + p2 + ": " + txtResult.Text);
        }

        /// <summary>
        /// Logs a message on the log text box and to wherever is configured by NLog.
        /// </summary>
        /// <param name="logLevel">The severity of the message.</param>
        /// <param name="message">The message to log.</param>
        private void Log(LogLevel logLevel, string message)
        {
            logger.Log(logLevel, message);
            txtLog.AppendText(DateTime.Now.ToUniversalTime() + ": " + message + '\n');
            txtLog.ScrollToEnd();
        }
    }
}
