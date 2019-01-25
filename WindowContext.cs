#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;

using OpenGL;
using OpenGL.CoreUI;

namespace TuxEngine
{
    public sealed class WindowContextEvent
    {
        public delegate void WindowContextDelegate(object sender, EventArgs e);

        public event WindowContextDelegate CreateWindow;


        public void Process()
        {
            Console.WriteLine("Creating window..");
            OnCreateWindow(this, null);
        }


        private void OnCreateWindow(object sender, EventArgs e)
        {
            CreateWindow?.Invoke(sender, e);
        }
    }

    public static class WindowContext
    {
        private static readonly float[] vertices =
        {
            -0.5f, -0.5f,
            0.5f, -0.5f,
            0.0f, 0.5f,
        };

        private static readonly string[] vertexShader =
        {
            "#version 150 core \n " +
            "in vec2 position; \n " +
            "void main() \n { \n " +
            "gl_Position = vec4(position, 0.0, 1.0); \n " +
            "} "
        };

        private static readonly string[] fragmentShader =
        {
            "#version 150 core \n " +
            "out vec4 outColor; \n " +
            "void main() \n {" +
            "outColor = vec4(0.4, 0.5, 6.0, 1.0); \n " +
            "}"
        };

        public static NativeWindow NativeWindow { get; set; }

        public static void CreateWindow(object sender, EventArgs e)
        {
            using (NativeWindow = NativeWindow.Create())
            {
                var renderEvent = new RenderEvent();
                renderEvent.Render += Renderer.Render;

                NativeWindow.ContextCreated += NativeWindowContextCreated;
                NativeWindow.Render += Renderer.Render;
                NativeWindow.KeyDown += InputHandler.HandleInput;
                NativeWindow.Animation = true;

                NativeWindow.Create(0, 0, 800, 600, NativeWindowStyle.Resizeable);

                NativeWindow.Show();
                NativeWindow.Run();
            }
        }


        private static void NativeWindowContextCreated(object sender, NativeWindowEventArgs e)
        {
            var vao = new uint[1];
            Gl.GenVertexArrays(vao);
            Gl.BindVertexArray(vao[0]);

            var vbo = new uint[1];
            Gl.GenBuffers(vbo);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo[0]);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(sizeof(float) * vertices.Length), vertices, BufferUsage.StaticDraw);

            var vShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vShader, vertexShader);
            Gl.CompileShader(vShader);

            var fShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fShader, fragmentShader);
            Gl.CompileShader(fShader);

            var program = Gl.CreateProgram();
            Gl.AttachShader(program, vShader);
            Gl.AttachShader(program, fShader);
            Gl.BindFragDataLocation(program, 0, "outColor");
            Gl.LinkProgram(program);
            Gl.UseProgram(program);

            var pos = Gl.GetAttribLocation(program, "position");
            Gl.VertexAttribPointer((uint)pos, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray((uint)pos);
        }


        public static void FullscreenWindow(NativeWindowKeyEventArgs e)
        {
            if (e.Key == KeyCode.F)
                NativeWindow.Fullscreen = !NativeWindow.Fullscreen;
            else if (e.Key == KeyCode.Return)
                NativeWindow.Fullscreen = !NativeWindow.Fullscreen;
        }


        public static void Close(NativeWindowKeyEventArgs e)
        {
            if (e.Key == KeyCode.Escape)
                NativeWindow.Stop();
        }
    }
}