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

namespace KappaLauncher.Application.Misc {
    class DataSaver {
        private static string Path;
        public static void Init(Context context) {
            Path = context.FilesDir.ToString();
        }



        public static void Save(string name, object data) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path + name,
                                           FileMode.Create,
                                           FileAccess.Write,
                                           FileShare.None);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static object Load(string name) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path + name,
                                           FileMode.Open,
                                           FileAccess.Read,
                                           FileShare.Read);
            object result = formatter.Deserialize(stream);
            stream.Close();
            return result;
        }
    }
}