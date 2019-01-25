#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;

namespace TuxEngine
{
    public class Event
    {
        public delegate void Delegate(object sender, EventArgs e);

        public event EventHandler EventHandle;


        protected void OnEventHandle(object sender, EventArgs e)
        {
            EventHandle?.Invoke(sender, e);
        }
    }
}