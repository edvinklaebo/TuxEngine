#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using OpenGL;
using OpenGL.CoreUI;

namespace TuxEngine
{
    public sealed class RenderEvent
    {
        public delegate void RendererDelegate(object sender, NativeWindowEventArgs args);

        public event RendererDelegate Render;


        public void Process()
        {
            OnRender(this, null);
        }


        private void OnRender(object sender, NativeWindowEventArgs args)
        {
            Render?.Invoke(sender, args);
        }
    }

    public static class Renderer
    {
        public static void Render(object sender, NativeWindowEventArgs args)
        {
            var nativeWindow = (NativeWindow)sender;

            Gl.Viewport(0, 0, (int)nativeWindow.Width, (int)nativeWindow.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.ClearColor(0.2F, 0.2F, 0.2F, 1.0F);
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
    }
}