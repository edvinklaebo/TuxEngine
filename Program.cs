using OpenGL;

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



        #region Event Handling

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

            Gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out var compiled);
            if (compiled != 0)
                return vertexShader;

            Gl.DeleteShader(vertexShader);

            const int logMaxLength = 1024;

            var infoLog = new StringBuilder(logMaxLength);

            Gl.GetShaderInfoLog(vertexShader, logMaxLength, out _, infoLog);

            throw new InvalidOperationException($"unable to compile shader: {infoLog}");
        }

    }
}
