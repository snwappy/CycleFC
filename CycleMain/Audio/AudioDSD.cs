using System;
using System.Windows.Forms;
using CycleCore.Nes;
using CycleCore.Nes.Output.Audio;
using SlimDX.DirectSound;
using SlimDX.Multimedia;

namespace CycleMain
{
    public class AudioDSD : IAudioDevice
    {
        IntPtr _Handle;
        NesApu apu;
        public Control _Control;
        //slimdx
        public DirectSound _SoundDevice;
        public SecondarySoundBuffer buffer;
        WaveRecorder recorder = new WaveRecorder();

        bool IsPaused;
        public bool IsRendering = false;
        bool _FirstRender = true;
        public byte[] DATA = new byte[88200];
        int BufferSize = 88200;
        int W_Pos = 0;//Write position
        int L_Pos = 0;//Last position
        int D_Pos = 0;//Data position
        public void Initialize(IntPtr handle, NesSystem nes)
        {
            apu = nes.Apu;
            _Handle = handle;
            //Create the device
            CONSOLE.WriteLine(this, "Initializing SlimDX DirectSound for APU....", DebugStatus.None);
            _SoundDevice = new DirectSound();
            _SoundDevice.SetCooperativeLevel(_Handle, CooperativeLevel.Normal);
            //Create the wav format
            WaveFormat wav = new WaveFormat();
            wav.FormatTag = WaveFormatTag.Pcm;
            wav.SamplesPerSecond = 44100;
            wav.Channels = 1;
            wav.BitsPerSample = 16;
            wav.AverageBytesPerSecond = wav.SamplesPerSecond * wav.Channels * (wav.BitsPerSample / 8);
            wav.BlockAlignment = (short)(wav.Channels * wav.BitsPerSample / 8);
            BufferSize = wav.AverageBytesPerSecond;
            //Description
            SoundBufferDescription des = new SoundBufferDescription();
            des.Format = wav;
            des.SizeInBytes = BufferSize;
            des.Flags = BufferFlags.ControlVolume | BufferFlags.ControlFrequency | BufferFlags.ControlPan;
            //buffer
            DATA = new byte[BufferSize];
            buffer = new SecondarySoundBuffer(_SoundDevice, des);
            buffer.Play(0, PlayFlags.Looping);
            CONSOLE.WriteLine(this, "SlimDX DirectSound initialized OK.", DebugStatus.Cool);
        }
        public IntPtr Handle
        {
            get { return _Handle; }
        }
        public void UpdateBuffer()
        {
            if (buffer == null | buffer.Disposed)
            { IsRendering = false; return; }
            W_Pos = buffer.CurrentWritePosition;
            if (_FirstRender)
            {
                _FirstRender = false;
                D_Pos = buffer.CurrentWritePosition + 0x1000;
                L_Pos = buffer.CurrentWritePosition;
            }
            int po = W_Pos - L_Pos;
            if (po < 0)
            {
                po = (BufferSize - L_Pos) + W_Pos;
            }
            if (po != 0)
            {
                for (int i = 0; i < po; i += 2)
                {
                    //Get the sample from the apu
                    short OUT = (short)apu.PullSample();
                    //RECORD
                    if (recorder != null)
                        if (recorder.IsRecording)
                            recorder.AddSample(OUT);
                    if (D_Pos < DATA.Length)
                    {
                        DATA[D_Pos] = (byte)((OUT & 0xFF00) >> 8);
                        DATA[D_Pos + 1] = (byte)(OUT & 0xFF);
                    }
                    D_Pos += 2;
                    D_Pos = D_Pos % BufferSize;
                }
                buffer.Write(DATA, 0, LockFlags.None);
                L_Pos = W_Pos;
            }
            IsRendering = false;
        }
        public void Play()
        {
            if (!IsPaused)
            { return; }
            if (buffer != null && !buffer.Disposed & !IsRendering)
            {
                IsPaused = false;
                try//Sometimes this line thorws an exception for unkown reason !!
                {
                    buffer.Play(0, PlayFlags.Looping);
                }
                catch {
                    MessageBox.Show("Unexpected Error in buffer data", "DirectSound Exception in CycleFC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        public void Stop()
        {
            if (IsPaused)
            { return; }
            if (buffer != null && !buffer.Disposed & !IsRendering)
            {
                buffer.Stop();
                IsPaused = true;
            }
        }
        public void Shutdown()
        {
            IsPaused = true;
            while (IsRendering)
            { }
            if (recorder.IsRecording)
                recorder.Stop();
            buffer.Stop();
            buffer.Dispose();
            _SoundDevice.Dispose();
        }
        public void SetVolume(int Vol)
        {
            if (buffer != null && !buffer.Disposed & !IsRendering)
            {
                buffer.Volume = Vol;
            }
        }
        public void SetPan(int Pan)
        {
            if (buffer != null && !buffer.Disposed & !IsRendering)
            {
                buffer.Pan = Pan;
            }
        }
        public NesApu APU
        {
            get
            {
                return apu;
            }
            set
            {
                apu = value;
            }
        }
        public IWaveRecorder Recorder
        {
            get { return recorder; }
        }
        public void Initialize()
        {

        }
        public int Volume
        {
            get
            {
                if (buffer != null && !buffer.Disposed & !IsRendering)
                    return buffer.Volume;

                return 0;
            }
        }
    }
}