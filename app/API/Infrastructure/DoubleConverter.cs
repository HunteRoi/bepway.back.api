using AutoMapper;

namespace API.Infrastructure
{
    public class StringSurfaceConverter : IValueConverter<double, string>
    {
        public string Convert(double source, ResolutionContext context) => (source*100).ToString();
        // multiplicated by 100 because DB contains data in SQUARE METERS but display is in ARES
    }

    public class DoubleSurfaceConverter : IValueConverter<string, double>
    {
        public double Convert(string source, ResolutionContext context) => (double.Parse(source, System.Globalization.CultureInfo.InvariantCulture)/100);
        // divided by 100 because display is in ARES, not in SQUARE METERS
    }

    public class DoubleCoordinatesConverter : IValueConverter<string, double>
    {
        public double Convert(string source, ResolutionContext context) => double.Parse(source, System.Globalization.CultureInfo.InvariantCulture);
    }

    public class StringCoordinatesConverter : IValueConverter<double, string>
    {
        public string Convert(double source, ResolutionContext context) => source.ToString();
    }
}