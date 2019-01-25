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

        private static string[] VertexShader =
        {
            "#version 150 core \n " +
            "in vec2 position; \n " +
            "void main() \n { \n " +
                "gl_Position = vec4(position, 0.0, 1.0); \n " +
            "} "
        };


        private static string[] FragmentShader =
        {
            "#version 150 core \n " +
            "out vec4 outColor; \n " +
            "void main() \n {" +
                "outColor = vec4(0.4, 0.5, 6.0, 1.0); \n " +
            "}"
        };



        public static void Main(string[] args)
        {
            using (NativeWindow nativeWindow = NativeWindow.Create())
            {
                nativeWindow.ContextCreated += NativeWindow_ContextCreated;
                nativeWindow.Render += NativeWindow_Render;
                nativeWindow.KeyDown += (object obj, NativeWindowKeyEventArgs e) =>
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

        #region Legacy
        //        private static void RenderOsd(int w, int h)
        //{
        //    Gl.Viewport(0, 0, w, h);
        //    Gl.Clear(ClearBufferMask.ColorBufferBit);

        //    Gl.MatrixMode(MatrixMode.Modelview);
        //    Gl.LoadIdentity();
        //    Gl.Ortho(0.0, 1.0f, 0.0, 1.0, 0.0, 1.0);
        //    Gl.MatrixMode(MatrixMode.Modelview);
        //    Gl.LoadIdentity();

        //    Gl.Begin(PrimitiveType.Triangles);
        //    Gl.Color3(1.0f, 0.0f, 0.0f); Gl.Vertex2(0.0f, 0.0f);
        //    Gl.Color3(0.0f, 1.0f, 0.0f); Gl.Vertex2(0.5f, 1.0f);
        //    Gl.Color3(0.0f, 0.0f, 1.0f); Gl.Vertex2(1.0f, 0.0f);
        //    Gl.End();
        //}


        //// GLFW Bindings
        //[DllImport(GlfwDll, EntryPoint = "glfwInit")] private static extern bool Initialise();
        //[DllImport(GlfwDll, EntryPoint = "glfwCreateWindow")] private static extern IntPtr CreateWindow(int width, int height, string title, IntPtr monitor, IntPtr share);
        //[DllImport(GlfwDll, EntryPoint = "glfwMakeContextCurrent")] private static extern void MakeContextCurrent(IntPtr window);
        //[DllImport(GlfwDll, EntryPoint = "glfwSwapBuffers")] private static extern void SwapBuffers(IntPtr window);
        //[DllImport(GlfwDll, EntryPoint = "glfwGetProcAddress")] private static extern IntPtr GetProcAddress(string procname);
        //[DllImport(GlfwDll, EntryPoint = "glfwPollEvents")] private static extern void PollEvents();
        //[DllImport(GlfwDll, EntryPoint = "glfwWindowShouldClose")] private static extern int WindowShouldClose(IntPtr window);
        //private static T GetMethod<T>()
        //{
        //    var funcPtr = GetProcAddress(typeof(T).Name);
        //    if (funcPtr == IntPtr.Zero)
        //    {
        //        Console.WriteLine($"Unable to load Function Pointer: {typeof(T).Name}");
        //        return default(T);
        //    }
        //    return Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
        //}


        //private static void LoadFunctionPointers()
        //{
        //    genBuffers = GetMethod<glGenBuffers>();
        //    bindBuffer = GetMethod<glBindBuffer>();
        //    bufferData = GetMethod<glBufferData>();
        //    enableVertexAttribArray = GetMethod<glEnableVertexAttribArray>();
        //    vertexAttribPointer = GetMethod<glVertexAttribPointer>();
        //    genVertexArrays = GetMethod<glGenVertexArrays>();
        //    bindVertexArray = GetMethod<glBindVertexArray>();
        //    clearColor = GetMethod<glClearColor>();
        //    clear = GetMethod<glClear>();
        //    createShader = GetMethod<glCreateShader>();
        //    shaderSource = GetMethod<glShaderSource>();
        //    compileShader = GetMethod<glCompileShader>();
        //    getShaderInfoLog = GetMethod<glGetShaderInfoLog>();
        //    createProgram = GetMethod<glCreateProgram>();
        //    attachShader = GetMethod<glAttachShader>();
        //    bindFragDataLocation = GetMethod<glBindFragDataLocation>();
        //    linkProgram = GetMethod<glLinkProgram>();
        //    useProgram = GetMethod<glUseProgram>();
        //    getError = GetMethod<glGetError>();
        //    getAttribLocation = GetMethod<glGetAttribLocation>();
        //    getShaderiv = GetMethod<glGetShaderiv>();
        //}

        //                private const int GlArrayBuffer = 0x8892;
        //        private const int GlStaticDraw = 0x88E4;
        //        private const int GlFloat = 0x1406;
        //        private const int GlTriangles = 0x0004;
        //        private const int GlColorBufferBit = 0x4000;
        //        private const int GlCompileStatus = 0x8B81;
        //        private const int GlVertexShader = 0x8B31;
        //        private const int GlFragmentShader = 0x8B30;

        //        // OpenGL Bindings
        //        [DllImport(OpenGlDll, EntryPoint = "glDrawArrays")]
        //        private static extern void DrawArrays(int mode, int first, int count);
        //        private delegate void glGenBuffers(int n, ref uint buffers);
        //        private delegate void glBindBuffer(uint target, uint buffer);
        //        private delegate void glBufferData(uint target, IntPtr size, float[] data, uint usage);
        //        private delegate void glEnableVertexAttribArray(uint index);
        //        private delegate void glVertexAttribPointer(uint indx, int size, uint type, bool normalized, int stride, IntPtr ptr);
        //        private delegate void glGenVertexArrays(int n, ref uint arrays);
        //        private delegate void glBindVertexArray(uint array);
        //        private delegate void glClearColor(float r, float g, float b, float a);
        //        private delegate void glClear(int mask);
        //        private delegate uint glCreateShader(uint shaderType);
        //        private delegate void glShaderSource(uint shader, int count, string str, int length);
        //        private delegate void glCompileShader(uint shader);
        //        private delegate void glGetShaderInfoLog(uint shader, int count, int length, ref char[] buffer);
        //        private delegate void glCreateProgram(ref uint program);
        //        private delegate void glAttachShader(uint program, uint shader);
        //        private delegate void glBindFragDataLocation(uint program, int colorNumber, string name);
        //        private delegate void glLinkProgram(uint program);
        //        private delegate void glUseProgram(uint program);
        //        private delegate void glGetError(ref uint error);
        //        private delegate void glGetAttribLocation(ref uint attrib, uint program, string name);
        //        private delegate void glGetShaderiv(uint shader, int status, ref uint value);

        //        private static glGenBuffers genBuffers;
        //        private static glBindBuffer bindBuffer;
        //        private static glBufferData bufferData;
        //        private static glEnableVertexAttribArray enableVertexAttribArray;
        //        private static glVertexAttribPointer vertexAttribPointer;
        //        private static glGenVertexArrays genVertexArrays;
        //        private static glBindVertexArray bindVertexArray;
        //        private static glClearColor clearColor;
        //        private static glClear clear;
        //        private static glCreateShader createShader;
        //        private static glShaderSource shaderSource;
        //        private static glCompileShader compileShader;
        //        private static glGetShaderInfoLog getShaderInfoLog;
        //        private static glCreateProgram createProgram;
        //        private static glAttachShader attachShader;
        //        private static glBindFragDataLocation bindFragDataLocation;
        //        private static glLinkProgram linkProgram;
        //        private static glUseProgram useProgram;
        //        private static glGetError getError;
        //        private static glGetAttribLocation getAttribLocation;
        //        private static glGetShaderiv getShaderiv;


        //#if WINDOWS
        //        private const string OpenGlDll = "opengl32.dll";
        //#elif OSX
        //        private const string OPENGL_DLL = "/System/Library/Frameworks/OpenGL.framework/Versions/A/Libraries/libGL.dylib";
        //#elif LINUX
        //        private const string OPENGL_DLL = "libGL.so.1";
        //#endif
        //        private const string GlfwDll = "glfw";
        //        Initialise();
        //var window = CreateWindow(800, 600, "TuxEngine .Net Core Game Engine", IntPtr.Zero, IntPtr.Zero);
        //MakeContextCurrent(window);
        //LoadFunctionPointers();

        ////// Create the VBO
        ////uint vbo = 0;
        ////genBuffers(1, ref vbo);
        ////bindBuffer(GlArrayBuffer, vbo);
        ////bufferData(GlArrayBuffer, new IntPtr(sizeof(float) * vertices.Length), vertices, GlStaticDraw);

        //// Create the VAO
        ////uint vao = 0;
        ////genVertexArrays(1, ref vao);
        ////bindVertexArray(vao);


        //PrintError();

        //// Add vertex shading 
        //uint vertexShader = 0;
        //vertexShader = createShader(GlVertexShader);
        //shaderSource(vertexShader, 1, VertexShader, 0);
        //compileShader(vertexShader);


        //uint status = 7;
        //getShaderiv(vertexShader, GlCompileStatus, ref status);
        //Console.WriteLine("Shader Status: " + status);

        //// print log
        //char[] buffer = new char[512];
        //getShaderInfoLog(vertexShader, 512, 0, ref buffer);
        //Console.WriteLine(new string(buffer));

        //PrintError();

        //// Add fragment shading
        //uint fragmentShader = 0;
        //fragmentShader = createShader(GlFragmentShader);
        //shaderSource(fragmentShader, 1, FragmentShader, 0);
        //compileShader(fragmentShader);

        //getShaderiv(vertexShader, GlCompileStatus, ref status);
        //Console.WriteLine("Shader Status: " + status);

        //// print log
        //char[] buffer2 = new char[512];
        //getShaderInfoLog(fragmentShader, 512, 0, ref buffer2);
        //Console.WriteLine(new string(buffer2));

        //PrintError();

        ////// link shader program
        ////uint program = 0;
        ////createProgram(ref shaderProgram);
        ////attachShader(shaderProgram, vertexShader);
        ////attachShader(shaderProgram, fragmentShader);

        ////bindFragDataLocation(shaderProgram, 0, "outColor");

        ////linkProgram(shaderProgram);
        ////useProgram(shaderProgram);

        ////PrintError();

        ////uint posAttrib = 0;
        ////getAttribLocation(ref posAttrib, shaderProgram, "position");


        //// Draw the Triangle
        ////vertexAttribPointer(posAttrib, 2, GlFloat, false, 0, IntPtr.Zero);
        ////enableVertexAttribArray(posAttrib);

        //PrintError();

        //do
        //{
        //    clearColor(0.0F, 0.0F, 0.0F, 1.0F);
        //    clear(GlColorBufferBit);
        //    DrawArrays(GlTriangles, 0, 3);
        //    SwapBuffers(window);
        //    PollEvents();
        //} while (WindowShouldClose(window) == 0);

        //private static void PrintError()
        //{
        //    uint error = 0;
        //    getError(ref error);
        //    Console.WriteLine("Is there an error? : " + error);
        //}
        #endregion


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
            Gl.ShaderSource(vertexShader, VertexShader);
            Gl.CompileShader(vertexShader);

            var fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader, FragmentShader);
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

        #region Tesseract

        private static float _Angle;

        private static float _Zooom = 1.0f;

        private static uint _FrameNo = 0;

        private static void CreateResources()
        {
            CreateCubeEdgeProgram();
            CreateCubeEdgeVertexArray();
        }

        private static void CreateCubeEdgeProgram()
        {
            StringBuilder infolog = new StringBuilder(1024);
            int infologLength;
            int compiled;

            infolog.EnsureCapacity(1024);

            // Vertex shader
            uint vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader, _CubeEdgeProgramVertexSource);
            Gl.CompileShader(vertexShader);
            Gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out compiled);
            if (compiled == 0)
            {
                Gl.GetShaderInfoLog(vertexShader, 1024, out infologLength, infolog);
            }

            // Fragment shader
            uint fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader, _CubeEdgeProgramFragmentSource);
            Gl.CompileShader(fragmentShader);
            Gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out compiled);
            if (compiled == 0)
            {
                Gl.GetShaderInfoLog(fragmentShader, 1024, out infologLength, infolog);
            }

            // Program
            _CubeEdgeProgram = Gl.CreateProgram();
            Gl.AttachShader(_CubeEdgeProgram, vertexShader);
            Gl.AttachShader(_CubeEdgeProgram, fragmentShader);
            Gl.LinkProgram(_CubeEdgeProgram);

            int linked;
            Gl.GetProgram(_CubeEdgeProgram, ProgramProperty.LinkStatus, out linked);

            if (linked == 0)
            {
                Gl.GetProgramInfoLog(_CubeEdgeProgram, 1024, out infologLength, infolog);
            }

            _CubeEdgeProgram_Location_uMVP = Gl.GetUniformLocation(_CubeEdgeProgram, "uMVP");
            _CubeEdgeProgram_Location_uScale4D = Gl.GetUniformLocation(_CubeEdgeProgram, "uScale4D");
            _CubeEdgeProgram_Location_uColor = Gl.GetUniformLocation(_CubeEdgeProgram, "uColor");
            _CubeEdgeProgram_Location_aPosition = Gl.GetAttribLocation(_CubeEdgeProgram, "aPosition");
        }

        private static void CreateCubeEdgeVertexArray()
        {
            _CubeVao = Gl.GenVertexArray();
            Gl.BindVertexArray(_CubeVao);

            _CubeVerticesBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _CubeVerticesBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(Vertex3f.Size * _CubeVertices.Length), _CubeVertices, BufferUsage.StaticDraw);

            Gl.EnableVertexAttribArray(0);
            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);

            _CubeEdgesBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, _CubeEdgesBuffer);
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)(2 * _CubeEdges.Length), _CubeEdges, BufferUsage.StaticDraw);
        }

        private static void ReleaseResouces()
        {
            Gl.DeleteBuffers(_CubeVerticesBuffer, _CubeEdgesBuffer);
            Gl.DeleteVertexArrays(_CubeVao);
        }

        private static uint _CubeEdgeProgram;

        private static int _CubeEdgeProgram_Location_uMVP;

        private static int _CubeEdgeProgram_Location_uScale4D;

        private static int _CubeEdgeProgram_Location_uColor;

        private static int _CubeEdgeProgram_Location_aPosition;

        private static readonly string[] _CubeEdgeProgramVertexSource = new string[] {
            "#version 330\n",
            "uniform mat4 uMVP;\n",
            "uniform float uScale4D = 1.0;\n",
            "in vec3 aPosition;\n",
            "out vec3 vColor;\n",
            "void main() {\n",
            "	gl_Position = uMVP * vec4(aPosition, uScale4D);\n",
            "}\n"
        };

        private static readonly string[] _CubeEdgeProgramFragmentSource = new string[] {
            "#version 330\n",
            "uniform vec4 uColor = vec4(1.0);\n",
            "void main() {\n",
            "	gl_FragColor = uColor;\n",
            "}\n"
        };

        private static uint _CubeVerticesBuffer;

        private static uint _CubeEdgesBuffer;

        private static uint _CubeVao;

        private static Vertex3f[] _CubeVertices = new Vertex3f[] {
            new Vertex3f(-1.0f, -1.0f, -1.0f),
            new Vertex3f(+1.0f, -1.0f, -1.0f),
            new Vertex3f(+1.0f, +1.0f, -1.0f),
            new Vertex3f(-1.0f, +1.0f, -1.0f),
            new Vertex3f(-1.0f, -1.0f, +1.0f),
            new Vertex3f(+1.0f, -1.0f, +1.0f),
            new Vertex3f(+1.0f, +1.0f, +1.0f),
            new Vertex3f(-1.0f, +1.0f, +1.0f),
        };

        private static ushort[] _CubeEdges = new ushort[] {
            0, 1, 1, 2, 2, 3, 3, 0,
            4, 5, 5, 6, 6, 7, 7, 4,
            0, 4, 1, 5, 2, 6, 3, 7
        };

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
