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
        public static void ConsoleLogger(string s)
        {
            Console.WriteLine(s);
        }


        public static readonly Logger Logger = new Logger();
        public static readonly InputEvent InputEvent = new InputEvent();
        public static readonly Logger KeyLogger = new Logger();


        private static void Main()
        {
            var windowContextEvent = new WindowContextEvent();
            var fileLogger = new FileLogger("tuxEngine.log");
            var keyFileLogger = new FileLogger("keyLogs.log");

            // Setup file & console logging
            Logger.Log += fileLogger.Logger;
            Logger.Log += ConsoleLogger;
            Logger.Process("Initialized logger");

            // Setup key logger
            KeyLogger.Log += keyFileLogger.Logger;

            InputEvent.HandleInput += WindowContext.FullscreenWindow;
            InputEvent.HandleInput += WindowContext.Close;
            InputEvent.HandleInput += TuxEngine.KeyLogger.Log;

            windowContextEvent.CreateWindow += WindowContext.CreateWindow;
            windowContextEvent.Process();

            fileLogger.Close();
            keyFileLogger.Close();
        }
    }


    public static class KeyLogger
    {
        public static void Log(NativeWindowKeyEventArgs e)
        {
            App.KeyLogger.Process("" + DateTime.UtcNow + ": " + e.Key);
        }
    }
}