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
    public sealed class InputEvent
    {
        public delegate void InputDelegate(NativeWindowKeyEventArgs e);

        public event InputDelegate HandleInput;



        public void Process(NativeWindowKeyEventArgs e)
        {
            OnHandleInput(e);
        }


        private void OnHandleInput(NativeWindowKeyEventArgs e)
        {
            HandleInput?.Invoke(e);
        }
    }

    public static class InputHandler
    {

        public static void HandleInput(object sender, NativeWindowKeyEventArgs e)
        {
            App.InputEvent.Process(e);
        }
    }
}