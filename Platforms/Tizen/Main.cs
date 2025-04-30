using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace JGualpa_TC_S5_PA
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
