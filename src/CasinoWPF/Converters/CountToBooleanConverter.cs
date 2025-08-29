using System.Globalization;
using System.Windows.Data;


namespace CasinoWPF.Converters
{ 
public class CountToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count && parameter is string paramString && int.TryParse(paramString, out int targetCount))
            return count == targetCount;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}