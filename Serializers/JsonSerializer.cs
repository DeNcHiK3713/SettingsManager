using Newtonsoft.Json.Linq;
using System.IO;

namespace Settings.Serializers
{
    public class JsonSerializer : ISerializer
    {
        private JObject jobject;

        void ISerializer.Load(string path)
        {
            jobject = JObject.Parse(File.ReadAllText(path));
        }

        void ISerializer.Save(string path)
        {
            File.WriteAllText(path, jobject.ToString());
        }

        T ISerializer.GetSection<T>()
        {
            var section = jobject["Settings"][typeof(T).Name];
            return section == null ? default : section.ToObject<T>();
        }

        void ISerializer.SetSection<T>(T data)
        {
            jobject["Settings"][typeof(T).Name] = JToken.FromObject(data);
        }
    }
}