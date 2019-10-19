using System.Collections.Generic;
using System.Linq;

using Android.Content;
using Android.Content.PM;
using KappaLauncher.Misc;
using Android.OS;
using System.Text;
using KappaLauncher.Views;
using System;
using Android.Widget;

namespace KappaLauncher.Apps {
	public static class AppManager {
		public static List<App> Apps { get; private set; }
		private static PackageManager Manager;

		public static void Init(Context context) {
			Manager = context.PackageManager;
			Apps = new List<App>();
		}



		public delegate void LoadingListener(double progress);

		public static void Load(LoadingListener listener) {
			Handler handler = new Handler();
			Apps = (List<App>) DataSaver.Read("apps");
			if(Apps == null) {
				Apps = new List<App>();

				Java.Lang.Thread Th;
				Th = new Java.Lang.Thread(() => {
					Intent loader = new Intent(Intent.ActionMain, null);
					loader.AddCategory(Intent.CategoryLauncher);
					List<ResolveInfo> resolve = Manager.QueryIntentActivities(loader, 0).ToList();

					double progress = 0D;
					resolve.ForEach(e => {
						LoadAppFromResolve(e);
						progress++;
						listener(progress / (double) resolve.Count);

						try {
							Java.Lang.Thread.Sleep(10L);
						} catch { }

					});
				});
				Th.Start();
			} else listener(1);
		}

		public static void LoadAppFromResolve(ResolveInfo e) {
			string package = e.ActivityInfo.PackageName;
			string activity = e.ActivityInfo.Name;
			string name = e.LoadLabel(Manager);

			long installTime = Manager.GetPackageInfo(package, 0).FirstInstallTime;

			App app = new App(package, activity, name, installTime);
			Apps.Add(app);
		}

		public static App GetAppFromKey(string key) {
			return Apps.FirstOrDefault(e => e.Key == key);
		}

		public static void StartApp(App app) {
			using(Intent intent = new Intent(Intent.ActionMain, null)) {
				intent.AddCategory(Intent.CategoryLauncher);
				intent.SetComponent(new ComponentName(app.Package, app.Activity));
				intent.SetFlags(ActivityFlags.NewTask);

				try {
					app.Popularity++;
					Launcher.Context.StartActivity(intent);
					Save();
				} catch(ActivityNotFoundException) {
					Toast.MakeText(Launcher.Context, "Activity Not Found", ToastLength.Short).Show();
				}
			}
		}

		public static void Save() {
			DataSaver.Save("apps", Apps);
		}







		[Serializable]
		public class App {
			public string Key { get; private set; }
			public string Package { get; private set; }
			public string Activity { get; private set; }
			public string Name { get; private set; }
			public long InstallTime { get; private set; }

			public App(string package, string activity, string name, long instllTime) {
				Package = package;
				Activity = activity;
				Name = name;
				InstallTime = instllTime;

				/*var crypt = new System.Security.Cryptography.SHA256Managed();
				var hash = new StringBuilder();
				byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(Package + ":" + Activity));
				foreach (byte theByte in crypto) hash.Append(theByte.ToString("x2"));*/
				Key = Package + ":" + Activity; // hash.ToString();
			}

			public int Popularity { get; set; }
			public bool Hidden { get; set; }
		}
	}
}