using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

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