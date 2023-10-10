using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using MyNes.Nes;
using MyNes.Nes.Output.Video;
using SlimDX;
using SlimDX.Direct3D9;

namespace MyNes
{
    public class VideoD3D : IVideoDevice, IDisposable
    {
        RegionFormat TvFormat;
        bool disposed;
        bool isRendering = false;
        bool canRender = true;
        int scanlines = 240;
        int linesToSkip = 0;
        Control surface;
        Texture backBuffer;
        bool deviceLost;
        byte[] displayData = new byte[245760];
        int BuffSize = 229376;
        Device displayDevice;
        Format displayFormat;
        Rectangle displayRect;
        Sprite displaySprite;
        Texture nesDisplay;
        public Direct3D d3d = new Direct3D();
        PresentParameters presentParams;
        bool Initialized = false;
        bool fullScreen = false;
        Vector3 _Position;

        int ModeIndex = 0;
        DisplayMode mode;
        bool IsImmediate = true;
        bool CutLinesInNTSC = true;
        DisplayMode currentMode;

        public VideoD3D(RegionFormat TvFormat, Control Surface)
        {
            if (Surface == null)
                return;
            surface = Surface;
            this.TvFormat = TvFormat;
        }
        void InitializeDirect3D()
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
            currentMode = d3d.Adapters[0].CurrentDisplayMode;
            try
            {
                if (!fullScreen)
                {
                    CONSOLE.WriteLine(this, "Initializing SlimDX Direct3D (windowed)....", DebugStatus.None);
                    d3d = new Direct3D();
                    presentParams = new PresentParameters();
                    presentParams.BackBufferWidth = 256;
                    presentParams.BackBufferHeight = scanlines;
                    presentParams.Windowed = true;
                    presentParams.SwapEffect = SwapEffect.Discard;
                    presentParams.Multisample = MultisampleType.None;
                    if (IsImmediate)
                        presentParams.PresentationInterval = PresentInterval.Immediate;
                    else
                        presentParams.PresentationInterval = PresentInterval.Default;
                    displayFormat = currentMode.Format;
                    displayDevice = new Device(d3d, 0, DeviceType.Hardware, surface.Handle, CreateFlags.SoftwareVertexProcessing, presentParams);
                    displayRect = new Rectangle(0, 0, 256, scanlines);
                    BuffSize = (256 * scanlines * 4);
                    displayData = new byte[BuffSize];
                    CreateDisplayObjects(displayDevice);
                    Initialized = true;
                    disposed = false;

                    CONSOLE.WriteLine(this, "SlimDX Direct3D Initialized OK.", DebugStatus.Cool);
                }
                else
                {
                    CONSOLE.WriteLine(this, "Initializing SlimDX Direct3D (fullscreen)....", DebugStatus.None);
                    d3d = new Direct3D();
                    mode = this.FindSupportedMode();
                    presentParams = new PresentParameters();
                    presentParams.BackBufferFormat = mode.Format;
                    presentParams.BackBufferCount = 1;
                    presentParams.BackBufferWidth = mode.Width;
                    presentParams.BackBufferHeight = mode.Height;
                    presentParams.FullScreenRefreshRateInHertz = mode.RefreshRate;
                    presentParams.Windowed = false;
                    presentParams.SwapEffect = SwapEffect.Discard;
                    presentParams.Multisample = MultisampleType.None;
                    if (IsImmediate)
                        presentParams.PresentationInterval = PresentInterval.Immediate;
                    else
                        presentParams.PresentationInterval = PresentInterval.Default;
                    displayFormat = mode.Format;
                    displayDevice = new Device(d3d, 0, DeviceType.Hardware, surface.Parent.Parent.Handle, CreateFlags.SoftwareVertexProcessing, new PresentParameters[] { presentParams });
                    displayDevice.ShowCursor = false;
                    displayRect = new Rectangle(0, 0, 256, scanlines);
                    _Position = new Vector3((mode.Width - 256) / 2, (mode.Height - scanlines) / 2, 0);
                    BuffSize = (256 * scanlines) * 4;
                    displayData = new byte[BuffSize];
                    CreateDisplayObjects(displayDevice);
                    Initialized = true;
                    disposed = false;
                    CONSOLE.WriteLine(this, "SlimDX Direct3D Initialized OK.", DebugStatus.Cool);
                }
            }
            catch (Exception EX)
            {
                Initialized = false; CONSOLE.WriteLine(this, "Can't initialize D3D video mode, system message:\n" + EX.Message, DebugStatus.Error);
            }
        }
        DisplayMode FindSupportedMode()
        {
            DisplayModeCollection supportedDisplayModes = d3d.Adapters[0].GetDisplayModes(Format.X8R8G8B8);
            DisplayMode mode = supportedDisplayModes[ModeIndex];
            return mode;
        }
        private void CreateDisplayObjects(Device device)
        {
            displaySprite = new Sprite(device);
            backBuffer = new Texture(device, 256, scanlines, 1, Usage.Dynamic, Format.A8R8G8B8, Pool.SystemMemory);
            nesDisplay = new Texture(device, 256, scanlines, 1, Usage.Dynamic, Format.A8R8G8B8, Pool.Default);
        }
        public string Name
        {
            get { return "D3D"; }
        }
        public string Desc
        {
            get { return "Render the video using SlimDX library.\nYou can switch into fullscreen using this mode with the resolution you set in the settings of this mode. This mode is accurate and fast."; }
        }
        public void Begin()
        {
            // displayData = new byte[BuffSize];

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
            if (surface != null & canRender & !disposed & Initialized)
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

                var result = displayDevice.TestCooperativeLevel();

                switch (result.Code)
                {
                    case -2005530510:
                    case -2005530520: deviceLost = true; break;

                    case -2005530519:
                        ResetDirect3D();
                        deviceLost = false;
                        break;
                }

                if (!deviceLost)
                {
                    if (displayData != null)
                    {
                        DataRectangle rect = backBuffer.LockRectangle(0, LockFlags.DoNotWait);
                        rect.Data.Write(displayData, 0, BuffSize);
                        rect.Data.Close();
                        backBuffer.UnlockRectangle(0);

                        if (canRender && !disposed)
                            displayDevice.UpdateTexture(backBuffer, nesDisplay);
                    }

                    displayDevice.BeginScene();
                    displayDevice.Clear(ClearFlags.Target, Color.Black, 0f, 0);
                    displaySprite.Begin(SpriteFlags.None);

                    displaySprite.Draw(nesDisplay, displayRect, Color.White);

                    displaySprite.End();
                    displayDevice.EndScene();
                    displayDevice.Present();
                }

                isRendering = false;
            }
        }

        void ResetDirect3D()
        {
            if (!Initialized)
                return;
            this.DisposeDisplayObjects();
            try
            {
                displayDevice.Reset(presentParams);
                this.CreateDisplayObjects(displayDevice);
            }
            catch
            {
                displayDevice.Dispose();
                this.InitializeDirect3D();
            }
        }
        void DisposeDisplayObjects()
        {
            if (displaySprite != null)
            {
                displaySprite.Dispose();
            }
            if (nesDisplay != null)
            {
                nesDisplay.Dispose();
            }
            if (backBuffer != null)
            {
                backBuffer.Dispose();
            }
        }
        public void Dispose()
        {
            disposed = true;
            if (displayDevice != null)
            {
                displayDevice.Dispose();
            }
            this.DisposeDisplayObjects();
        }
        public unsafe void TakeSnapshot(string SnapPath, string Format)
        {
            Bitmap bmp = new Bitmap(256, scanlines);
            BitmapData bmpData32 = bmp.LockBits(new Rectangle(0, 0, 256, scanlines), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
            int* numPtr32 = (int*)bmpData32.Scan0;
            int j = 0;
            for (int i = (linesToSkip * 256); i < 61440 - (linesToSkip * 256); i++)
            {
                byte VALBLUE = (byte)(displayData[j]);
                j++;
                byte VALGREEN = (byte)(displayData[j]);
                j++;
                byte VALRED = (byte)(displayData[j]);
                j++;
                byte VALALHA = (byte)(displayData[j]);
                j++;
                numPtr32[i - (linesToSkip * 256)] =
                   (VALALHA << 24 |//Alpha
                   VALRED << 16 |//Red
                  VALGREEN << 8 |//Green
                  VALBLUE)//Blue
                             ;
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
            get
            {
                return canRender;
            }
            set
            {
                canRender = value;
            }
        }
        public bool FullScreen
        {
            get
            {
                return fullScreen;
            }
            set
            {
                fullScreen = value;
                Dispose();
                InitializeDirect3D();
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
            Frm_D3DOptions Op = new Frm_D3DOptions(this);
            Op.ShowDialog();
        }
    }
}
