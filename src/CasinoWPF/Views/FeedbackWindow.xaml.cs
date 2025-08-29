using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CasinoWPF.Views
{
    public partial class FeedbackWindow : Window
    {
        public FeedbackWindow()
        {
            InitializeComponent();
        }

        private void FeedbackOption_Changed(object sender, RoutedEventArgs e)
        {
            if (GitHubOption == null || GitHubInfo == null || FormFields == null || SubmitButton == null)
                return;

            if (GitHubOption.IsChecked == true)
            {
                GitHubInfo.Visibility = Visibility.Visible;
                FormFields.Visibility = Visibility.Collapsed;
                SubmitButton.Content = "Zu GitHub wechseln";
            }
            else
            {
                GitHubInfo.Visibility = Visibility.Collapsed;
                FormFields.Visibility = Visibility.Visible;
                SubmitButton.Content = "Absenden";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (GitHubOption.IsChecked == true)
            {
                OpenGitHubIssue();
            }
            else
            {
                SendViaEmail();
            }
        }

        private void SendViaEmail()
        {
            var name = NameInput.Text.Trim();
            var email = EmailInput.Text.Trim();
            var title = TitleInput.Text.Trim();
            var message = MessageInput.Text.Trim();

            // Validation because all fields are required grrr
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(message))
            {
                ShowError("Bitte füllen Sie alle Felder aus.");
                return;
            }

            if (!IsValidEmail(email))
            {
                ShowError("Bitte geben Sie eine gültige E-Mail-Adresse ein.");
                return;
            }

            HideError();

            // create a mailtolink
            string mailtoLink = $"mailto:david.github.questions@gmail.com" +
                                $"?subject=CasinoWPF Feedback: {Uri.EscapeDataString(title)}" +
                                $"&body={Uri.EscapeDataString($"Name: {name}\nE-Mail: {email}\n\n{message}")}";

            // open standard mail client 
            Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });

            MessageBox.Show("Ihr Feedback wird nun über Ihre E-Mail-App gesendet.", "Feedback",
                MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void OpenGitHubIssue()
        {
            string githubUrl = "https://github.com/seakyy/CasinoWPF/issues/new";

            // open in new browser tab
            Process.Start(new ProcessStartInfo(githubUrl) { UseShellExecute = true });

            MessageBox.Show("Sie werden nun zu GitHub weitergeleitet, um ein Issue zu erstellen.", "GitHub Issue",
                MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            ErrorText.Visibility = Visibility.Collapsed;
        }
    }
}