using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Android.Content;
using KappaLauncher.Views;

namespace KappaLauncher.Misc {
	class DataSaver {
		public static string InternalStorage { get; private set; }

		public static void Init(Context context) {
			InternalStorage = context.FilesDir.ToString();
		}

		public static void Save(string name, object data) {
			try {
				String path = Path.Combine(InternalStorage, name);
				File.Delete(path);
				using(var writer = new StreamWriter(File.Exists(path) ? File.Open(path, FileMode.Open) : File.Create(path))) {
					IFormatter formatter = new BinaryFormatter();
					formatter.Serialize(writer.BaseStream, data);
					writer.Close();
				}
			} catch {
				Launcher.Message("Couldn't write file");
			}
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
				stream.Dispose();
				return result;
			} catch(Exception) {
				return null;
			}
		}

		public static void Delete(string name) {
			File.Delete(Path.Combine(InternalStorage, name));
		}
	}
}