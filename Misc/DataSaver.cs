using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Android.Content

namespace KappaLauncher.Misc {
    class DataSaver {
        public static string InternalStorage { get; private set; }
        public static void Init(Context context) {
            InternalStorage = context.FilesDir.ToString();
        }

        public static void Save(string name, object data) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(InternalStorage, name),
                                           FileMode.Create,
                                           FileAccess.Write,
                                           FileShare.None);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static object Read(string name) {
            try {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(Path.Combine(InternalStorage, name),
                                               FileMode.Open,
                                               FileAccess.Read,
                                               FileShare.Read);
                object result = formatter.Deserialize(stream);
                stream.Close();
                return result;
            } catch (Exception) {
                return null;
            }
        }
        public static void Delete(string name) {
            File.Delete(Path.Combine(InternalStorage, name));
        }
    }
}