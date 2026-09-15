using System.Windows;
using System.Windows.Input;

namespace FocusCube;

public partial class CustomTimeDialog : Window
{
    public CustomTimeDialog(TimeSpan currentDuration)
    {
        InitializeComponent();
        TimeInput.Text = TimeInputParser.Format(currentDuration);
        Loaded += (_, _) =>
        {
            TimeInput.Focus();
            TimeInput.SelectAll();
        };
    }

    public TimeSpan SelectedDuration { get; private set; }

    private void Start_Click(object sender, RoutedEventArgs e) => TryAccept();

    private void TimeInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        e.Handled = true;
        TryAccept();
    }

    private void TryAccept()
    {
        if (!TimeInputParser.TryParse(TimeInput.Text, out var duration))
        {
            ErrorText.Text = "Informe um tempo válido maior que zero.";
            TimeInput.Focus();
            TimeInput.SelectAll();
            return;
        }

        SelectedDuration = duration;
        DialogResult = true;
    }
}
