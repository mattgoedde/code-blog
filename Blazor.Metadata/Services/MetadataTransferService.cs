using Blazor.Metadata.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Blazor.Metadata.Services
{
    public class MetadataTransferService : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly NavigationManager _navigationManager;

        private readonly MetadataOptions _options;

        private MetadataValues? _selectedMetadata;
        public MetadataValues? SelectedMetadata { get => _selectedMetadata; set { _selectedMetadata = value; OnPropertyChanged(); } }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public MetadataTransferService(NavigationManager navigationManager, IOptionsMonitor<MetadataOptions> optionsDelegate)
        {
            _navigationManager = navigationManager;

            _options = optionsDelegate.Get("MetadataOptions");

            SelectedMetadata = _options.Metadatas["/"];

            _navigationManager.LocationChanged += UpdateMetadata;
        }

        private void UpdateMetadata(string url)
        {
            SelectedMetadata = _options.Metadatas.FirstOrDefault(vp => url.EndsWith(vp.Key)).Value;
            if (SelectedMetadata is null) SelectedMetadata = _options.Metadatas["/"];
        }

        private void UpdateMetadata(object? sender, LocationChangedEventArgs e)
        {
            UpdateMetadata(e.Location);
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= UpdateMetadata;
            GC.SuppressFinalize(this);
        }
    }
}
