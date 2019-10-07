﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using KappaLauncher.Application.Launcher;
using KappaLauncher.Application.Misc;
using KappaLauncher.Misc;

namespace KappaLauncher {
    [Application]
    public class LauncherApplication : Android.App.Application {
        public LauncherApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) {
        }

        public override void OnCreate() {
            base.OnCreate();

            Context context = ApplicationContext;

            Screen.Init(context);
            DataSaver.Init(context);

            Launcher.Init(context);
        }
    }
}