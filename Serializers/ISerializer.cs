namespace Settings.Serializers
{
    public interface ISerializer
    {
        void Load(string path);

        void Save(string path);

        T GetSection<T>();

        void SetSection<T>(T data);
    }
}