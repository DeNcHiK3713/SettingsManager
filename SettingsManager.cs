using Newtonsoft.Json;
using System;
using System.IO;

namespace SettingsManager
{
    public class SettingsManager<T> where T : class
    {
        #region Fields
        private string configPath;
        public T Settings;
        #endregion

        public SettingsManager(string _configPath)
        {
            if (string.IsNullOrEmpty(_configPath))
            {
                throw new ArgumentNullException(nameof(_configPath));
            }
            configPath = _configPath;
            AppDomain.CurrentDomain.ProcessExit += (s, e) => SaveSettings();
            LoadSettings();
        }

        public void LoadSettings()
        {
            Settings = File.Exists(configPath) ? JsonConvert.DeserializeObject<T>(File.ReadAllText(configPath)) : default;
        }
        public void SaveSettings()
        {
            File.WriteAllText(configPath, JsonConvert.SerializeObject(Settings, Formatting.Indented));
        }
    }
}
