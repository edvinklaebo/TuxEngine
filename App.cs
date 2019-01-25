#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;

using OpenGL.CoreUI;

namespace TuxEngine
{
    public static class App
    {
        public static void Logger(string s)
        {
            Console.WriteLine(s);
        }


        public static readonly LoggerEvent LoggerEvent = new LoggerEvent();
        public static readonly InputEvent InputEvent = new InputEvent();


        private static void Main()
        {
            var windowContextEvent = new WindowContextEvent();
            var fileLogger = new FileLogger("tuxEngine.log");

            // Setup file & console logging
            LoggerEvent.Log += fileLogger.Logger;
            LoggerEvent.Log += Logger;

            LoggerEvent.Process("Initialized logger");

            InputEvent.HandleInput += WindowContext.FullscreenWindow;
            InputEvent.HandleInput += WindowContext.Close;
            InputEvent.HandleInput += KeyLogger.Log;

            windowContextEvent.CreateWindow += WindowContext.CreateWindow;
            windowContextEvent.Process();

            fileLogger.Close();
        }
    }

    public static class KeyLogger
    {
        public static void Log(NativeWindowKeyEventArgs e)
        {
            App.LoggerEvent.Process("KEY PRESSED: " + e.Key);
        }
    }
}