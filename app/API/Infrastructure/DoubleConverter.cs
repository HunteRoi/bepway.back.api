using AutoMapper;

namespace API.Infrastructure
{
    public class StringDoubleConverter : IValueConverter<double, string>
    {
        public string Convert(double source, ResolutionContext context) => source.ToString();
    }

    public class DoubleConverter : IValueConverter<string, double>
    {
        public double Convert(string source, ResolutionContext context) => double.Parse(source, System.Globalization.CultureInfo.InvariantCulture);
    }
}