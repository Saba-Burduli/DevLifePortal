using System.ComponentModel.Composition.Hosting;
using System.Composition;
using Application.Abstractions.Export;

namespace Infrastructure.Providers;


    public class ExportServiceProvider : IExportServiceProvider
    {
        private readonly CompositionContainer _container;
        private readonly Dictionary<string, IExportService> _exporters;

        [ImportingConstructor]
        public ExportServiceProvider(CompositionContainer container)
        {
            _container = container;
            _exporters = _container.GetExports<IExportService>()
                .ToDictionary(e => e.Value.Format, e => e.Value);
        }

        public ICollection<string> GetAvailableExporters()
        {
            return _exporters.Keys;
        }

        public IExportService? GetExportService(string format)
        {
            _exporters.TryGetValue(format, out var exporter);
            return exporter;
        }
    }