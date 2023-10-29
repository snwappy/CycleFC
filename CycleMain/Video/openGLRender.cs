using System;
using System.Drawing;
using System.Drawing.Imaging;
using CycleCore.Nes.Output.Video;
using CycleCore.Nes;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ClearBufferMask = OpenTK.Graphics.OpenGL.ClearBufferMask;
using TextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;
using PixelInternalFormat = OpenTK.Graphics.OpenGL.PixelInternalFormat;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType;

namespace CycleMain
{
    public class VideoOpenGL : IVideoDevice, IDisposable
    {
        RegionFormat TvFormat;
        bool disposed;
        bool isRendering = false;
        bool canRender = true;
        int scanlines = 240;
        int linesToSkip = 0;
        GLControl surface;
        int backBuffer;
        bool deviceLost;
        byte[] displayData = new byte[245760];
        int BuffSize = 229376;
        int displayDevice;
        Rectangle displayRect;
        int nesDisplay;
        bool Initialized = false;
        bool fullScreen = false;

        int ModeIndex = 0;
        bool IsImmediate = true;
        bool CutLinesInNTSC = true;
        private RegionFormat tVSystem;
        private Panel panel_surface;

        public VideoOpenGL(RegionFormat TvFormat, GLControl Surface)
        {
            if (Surface == null)
                return;
            surface = Surface;
            this.TvFormat = TvFormat;

            surface.MakeCurrent();
        }

        public VideoOpenGL(RegionFormat tVSystem, Panel panel_surface)
        {
            this.tVSystem = tVSystem;
            this.panel_surface = panel_surface;
        }

        void InitializeOpenGL()
        {
            ApplySettings();
            if (CutLinesInNTSC)
            {
                switch (TvFormat)
                {
                    case RegionFormat.NTSC:
                        scanlines = 224;
                        linesToSkip = 8;
                        break;
                    case RegionFormat.PAL:
                        scanlines = 240;
                        linesToSkip = 0;
                        break;
                }
            }
            else
            {
                scanlines = 240;
                linesToSkip = 0;
            }

            try
            {
                surface.Context.LoadAll(); // Load all OpenGL functions

                surface.MakeCurrent();
                GL.Viewport(0, 0, 256, scanlines);
                GL.ClearColor(Color.Black);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Ortho(0, 256, 0, scanlines, -1, 1);

                displayRect = new Rectangle(0, 0, 256, scanlines);
                BuffSize = (256 * scanlines * 4);
                displayData = new byte[BuffSize];
                CreateDisplayObjects();

                Initialized = true;
                disposed = false;
                Console.WriteLine("OpenTK Initialized OK");
            }
            catch (Exception ex)
            {
                Initialized = false;
                Console.WriteLine("Can't initialize OpenGL mode, system message:\n" + ex.Message);
            }
        }

        private void CreateDisplayObjects()
        {
            backBuffer = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, backBuffer);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 256, scanlines, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

            nesDisplay = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, nesDisplay);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 256, scanlines, 0, PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
        }

        public string Name
        {
            get { return "OpenGL"; }
        }

        public string Desc
        {
            get { return "Render the video using OpenGL via OpenTK.\nYou can switch into fullscreen using this mode with the resolution you set in the settings of this mode. This mode is accurate and fast."; }
        }

        public void Begin()
        {
            if (canRender)
            {
                isRendering = true;
            }
        }

        public void BlankPixel(int X, int Y)
        {
        }

        public unsafe void RenderFrame(int[] ScreenBuffer)
        {
            if (surface != null && canRender && !disposed && Initialized)
            {
                fixed (byte* lpDst = displayData)
                {
                    var pixelsToSkip = (linesToSkip << 8);
                    var lpD = (int*)lpDst;

                    for (int i = pixelsToSkip; i < ScreenBuffer.Length - pixelsToSkip; i++)
                    {
                        *(lpD + (i - pixelsToSkip)) = ScreenBuffer[i];
                    }
                }

                // Clear the back buffer
                GL.BindTexture(TextureTarget.Texture2D, backBuffer);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.BindTexture(TextureTarget.Texture2D, 0);

                // Update the texture with display data
                GL.BindTexture(TextureTarget.Texture2D, backBuffer);
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, 256, scanlines, PixelFormat.Bgra, PixelType.UnsignedByte, displayData);
                GL.BindTexture(TextureTarget.Texture2D, 0);

                // Render the texture
                GL.BindTexture(TextureTarget.Texture2D, nesDisplay);
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0f, 0f); GL.Vertex2(0, 0);
                GL.TexCoord2(1f, 0f); GL.Vertex2(256, 0);
                GL.TexCoord2(1f, 1f); GL.Vertex2(256, scanlines);
                GL.TexCoord2(0f, 1f); GL.Vertex2(0, scanlines);
                GL.End();
                GL.BindTexture(TextureTarget.Texture2D, 0);

                surface.SwapBuffers();

                isRendering = false;
            }
        }

        void DisposeDisplayObjects()
        {
            GL.DeleteTexture(backBuffer);
            GL.DeleteTexture(nesDisplay);
        }

        public void Dispose()
        {
            disposed = true;
            surface.Dispose();
            DisposeDisplayObjects();
        }

        public unsafe void TakeSnapshot(string SnapPath, string Format)
        {
            int[] pixels = new int[256 * scanlines];
            GL.ReadPixels(0, 0, 256, scanlines, PixelFormat.Bgra, PixelType.UnsignedByte, pixels);

            Bitmap bmp = new Bitmap(256, scanlines);
            BitmapData bmpData32 = bmp.LockBits(new Rectangle(0, 0, 256, scanlines), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int* numPtr32 = (int*)bmpData32.Scan0;
            int j = 0;
            for (int i = (linesToSkip * 256); i < 61440 - (linesToSkip * 256); i++)
            {
                byte VALBLUE = (byte)(pixels[j]);
                j++;
                byte VALGREEN = (byte)(pixels[j]);
                j++;
                byte VALRED = (byte)(pixels[j]);
                j++;
                byte VALALPHA = (byte)(pixels[j]);
                j++;
                numPtr32[i - (linesToSkip * 256)] =
                   (VALALPHA << 24 | // Alpha
                   VALRED << 16 |   // Red
                   VALGREEN << 8 |  // Green
                   VALBLUE);        // Blue
            }
            bmp.UnlockBits(bmpData32);

            switch (Format)
            {
                case ".bmp":
                    bmp.Save(SnapPath, ImageFormat.Bmp);
                    canRender = true;
                    break;
                case ".gif":
                    bmp.Save(SnapPath, ImageFormat.Gif);
                    canRender = true;
                    break;
                case ".jpg":
                    bmp.Save(SnapPath, ImageFormat.Jpeg);
                    canRender = true;
                    break;
                case ".png":
                    bmp.Save(SnapPath, ImageFormat.Png);
                    canRender = true;
                    break;
                case ".tiff":
                    bmp.Save(SnapPath, ImageFormat.Tiff);
                    canRender = true;
                    break;
            }
        }

        public bool IsSizable
        {
            get { return false; }
        }

        public bool IsRendering
        {
            get { return isRendering; }
        }

        public bool CanRender
        {
            get { return canRender; }
            set { canRender = value; }
        }

        public bool FullScreen
        {
            get { return fullScreen; }
            set
            {
                fullScreen = value;
                Dispose();
                InitializeOpenGL();
            }
        }

        public bool SupportFullScreen
        {
            get { return true; }
        }

        public void ApplySettings()
        {
            ModeIndex = Program.Settings.D3D_FullscreenResIndex;
            IsImmediate = Program.Settings.D3D_IsImmediate;
            CutLinesInNTSC = Program.Settings.D3D_HideUpperScanlines;
        }

        public void ChangeSettings()
        {
            MessageBox.Show("OpenGL Settings under construction.");
        }
    }
}
