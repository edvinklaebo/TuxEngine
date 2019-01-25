#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System.IO;

namespace TuxEngine
{
    public class Logger
    {
        public delegate void LogHandler(string message);

        public event LogHandler Log;


        public void Process(string message)
        {
            OnLog(message);
        }


        protected void OnLog(string message)
        {
            Log?.Invoke(message);
        }
    }

    public class FileLogger
    {
        private readonly FileStream fileStream;
        private readonly StreamWriter streamWriter;


        public FileLogger(string filename)
        {
            this.fileStream = new FileStream(filename, FileMode.Create);
            this.streamWriter = new StreamWriter(this.fileStream);
        }


        public void Logger(string s)
        {
            this.streamWriter.WriteLine(s);
        }


        public void Close()
        {
            this.streamWriter.Close();
            this.fileStream.Close();
        }
    }
}