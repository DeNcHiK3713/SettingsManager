using Settings.Serializers;
using System;
using System.IO;

namespace Settings
{
    public class SettingsManager
    {
        #region Fields

        private string configPath;
        private ISerializer provider;

        #endregion Fields

        public SettingsManager(string _configPath = null)
        {
            if (string.IsNullOrEmpty(_configPath))
            {
                _configPath = "appsettings.json";
            }
            if (!File.Exists(_configPath))
            {
                var ex = new FileNotFoundException(nameof(_configPath));
                throw ex;
            }
            switch (Path.GetExtension(_configPath))
            {
                case ".json":
                    provider = new JsonSerializer();
                    break;

                case ".xml":
                    provider = new XmlSerializer();
                    break;

                default:
                    var ex = new Exception($"Unknown extension {Path.GetExtension(_configPath)}.");
                    throw ex;
            }
            configPath = _configPath;
            AppDomain.CurrentDomain.ProcessExit += (s, e) => SaveSettings();
            LoadSettings();
        }

        public T GetSection<T>()
        {
            return provider.GetSection<T>();
        }

        public void SetSection<T>(T data)
        {
            provider.SetSection<T>(data);
        }

        private void LoadSettings()
        {
            provider.Load(configPath);
        }

        public void SaveSettings()
        {
            provider.Save(configPath);
        }
    }
}