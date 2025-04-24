using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace KoteskiOlmesLB_426.Views
{
    public partial class FeedbackWindow : Window
    {
        public FeedbackWindow()
        {
            InitializeComponent();
        }

        private void SubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            var name = NameInput.Text.Trim();
            var email = EmailInput.Text.Trim();
            var title = TitleInput.Text.Trim();
            var message = MessageInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Bitte füllen Sie alle Felder aus.", "Fehlende Angaben", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string htmlTemplate = @"<!DOCTYPE html>
<html><body>
<form id='feedbackForm' method='POST' action='https://formsubmit.co/el/bonece'>
  <input type='hidden' name='name' value='[[NAME]]'>
  <input type='hidden' name='email' value='[[EMAIL]]'>
  <input type='hidden' name='_subject' value='CasinoWPF – [[TITLE]]'>
  <input type='hidden' name='message' value='[[MESSAGE]]'>
  <input type='hidden' name='_template' value='table'>
</form>
<script>document.getElementById('feedbackForm').submit();</script>
</body></html>";

            htmlTemplate = htmlTemplate.Replace("[[NAME]]", WebUtility.HtmlEncode(name));
            htmlTemplate = htmlTemplate.Replace("[[EMAIL]]", WebUtility.HtmlEncode(email));
            htmlTemplate = htmlTemplate.Replace("[[TITLE]]", WebUtility.HtmlEncode(title));
            htmlTemplate = htmlTemplate.Replace("[[MESSAGE]]", WebUtility.HtmlEncode(message));

            string tempFilePath = Path.Combine(Path.GetTempPath(), "CasinoWPF_Feedback.html");
            File.WriteAllText(tempFilePath, htmlTemplate);

            Process.Start(new ProcessStartInfo(tempFilePath) { UseShellExecute = true });
            MessageBox.Show("Ihr Feedback wird über das Standard-Browserformular übermittelt.", "Gesendet", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}