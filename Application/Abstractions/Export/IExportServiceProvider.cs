namespace Application.Abstractions.Export;

public interface IExportServiceProvider
{
    ICollection<string> GetAvailableExporters();
    IExportService? GetExportService(string format);
}