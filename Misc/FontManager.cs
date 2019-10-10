
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;

using System.Linq;

namespace KappaLauncher.Misc {
	class FontManager {
		public static List<Font> Fonts { get; private set; }
		private static Font CurrentFont;
		public static Typeface Current {
			get { return CurrentFont != null ? CurrentFont.Typeface : Typeface.Default; }
		}


		private static AssetManager Assets;
		private static string InternalStorage;
		public static void Init(Context context) {
			Assets = context.Assets;
			InternalStorage = context.FilesDir.ToString();
			CurrentFont = new Font(Typeface.Default, "Default");
		}

		public static void Load() {
			Fonts = new List<Font>();
			LoadFromAssets();
			LoadFromFolder();
			LoadCurrentFont();
		}



		private static void LoadFromAssets() {
			string[] folder = Assets.List("Fonts");
			foreach (string file in folder) {
				Typeface typeface = Typeface.CreateFromAsset(Assets, System.IO.Path.Combine("Fonts", file));
				string name = file;
				name = name.Substring(0, name.LastIndexOf('.') - 1);

				Font font = new Font(typeface, name);
				Fonts.Add(font);
			}
		}

		private static void LoadFromFolder() {
			string path = System.IO.Path.Combine(InternalStorage, "Fonts");
			string[] folder = Directory.GetFiles(path);
			foreach (string file in folder) {
				Typeface typeface = Typeface.CreateFromFile(System.IO.Path.Combine(path, file));
				string name = file;
				name = name.Substring(0, name.LastIndexOf('.') - 1);

				Font font = new Font(typeface, name);
				Fonts.Add(font);
			}
		}

		private static void LoadCurrentFont() {
			string name = (string) DataSaver.Read("CurrentFont");
			CurrentFont = GetFontByName(name);
		}


		public static Font GetFontByName(string name) {
			return Fonts.FirstOrDefault(e => e.Name == name); ;
		}



		public static void Save() {
			DataSaver.Save("CurrentFont", CurrentFont.Name);
		}


		public class Font {
			public Typeface Typeface { get; private set; }
			public string Name { get; private set; }

			public Font(Typeface typeface, string name) {
				Typeface = typeface;
				Name = name;
			}
		}
	}
}