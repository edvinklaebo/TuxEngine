using OpenGL;
using OpenGL.CoreUI;

using System;
using System.Text;

namespace TuxEngine
{
    public class Program
    {
        private static readonly float[] vertices = {
            -0.5f, -0.5f,
            0.5f, -0.5f,
            0.0f,  0.5f,
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



        public static void Main()
        {
            using (NativeWindow nativeWindow = NativeWindow.Create())
            {
                nativeWindow.ContextCreated += NativeWindow_ContextCreated;
                nativeWindow.Render += NativeWindow_Render;
                nativeWindow.KeyDown += (obj, e) =>
                {
                    switch (e.Key)
                    {
                        case KeyCode.Escape:
                            nativeWindow.Stop();
                            break;

                        case KeyCode.F:
                            nativeWindow.Fullscreen = !nativeWindow.Fullscreen;
                            break;
                    }
                };
                nativeWindow.Animation = true;

                nativeWindow.Create(0, 0, 800, 600, NativeWindowStyle.Resizeable);

                nativeWindow.Show();
                nativeWindow.Run();
            }
        }

        #region Event Handling

        private static void NativeWindow_ContextCreated(object sender, NativeWindowEventArgs e)
        {
            NativeWindow nativeWindow = (NativeWindow)sender;

            var vao = new uint[1];
            Gl.GenVertexArrays(vao);
            Gl.BindVertexArray(vao[0]);

            uint[] vbo = new uint[1];
            Gl.GenBuffers(vbo);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo[0]);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(sizeof(float) * vertices.Length), vertices, BufferUsage.StaticDraw);

            var vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader, Program.vertexShader);
            Gl.CompileShader(vertexShader);

            var fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader, Program.fragmentShader);
            Gl.CompileShader(fragmentShader);

            var program = Gl.CreateProgram();
            Gl.AttachShader(program, vertexShader);
            Gl.AttachShader(program, fragmentShader);
            Gl.BindFragDataLocation(program, 0, "outColor");
            Gl.LinkProgram(program);
            Gl.UseProgram(program);

            var pos = Gl.GetAttribLocation(program, "position");
            Gl.VertexAttribPointer((uint)pos, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray((uint)pos);


        }

        private static void NativeWindow_Render(object sender, NativeWindowEventArgs e)
        {
            NativeWindow nativeWindow = (NativeWindow)sender;

            Gl.Viewport(0, 0, (int)nativeWindow.Width, (int)nativeWindow.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.ClearColor(0.2F, 0.2F, 0.2F, 1.0F);
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }

        #endregion

        public static uint CreateVertexShaderShader()
        {
            string[] source = {
                "#version 150 compatibility\n",
                "uniform mat4 uMVP;\n",
                "in vec2 aPosition;\n",
                "in vec3 aColor;\n",
                "out vec3 vColor;\n",
                "void main() {\n",
                "	gl_Position = uMVP * vec4(aPosition, 0.0, 1.0);\n",
                "	vColor = aColor;\n",
                "}\n"
            };

            uint vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader, source);
            Gl.CompileShader(vertexShader);
            int compiled;

            Gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out compiled);
            if (compiled != 0)
                return vertexShader;

            // Not compiled!
            Gl.DeleteShader(vertexShader);

            const int logMaxLength = 1024;

            StringBuilder infolog = new StringBuilder(logMaxLength);
            int infologLength;

            Gl.GetShaderInfoLog(vertexShader, logMaxLength, out infologLength, infolog);

            throw new InvalidOperationException($"unable to compile shader: {infolog}");
        }

    }
}
