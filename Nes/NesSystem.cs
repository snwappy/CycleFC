/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code © Ala Hadid 2009 - 2011                                *
*Code maintainance by Snappy Pupper (@snappypupper on Twitter)        *
*Code updated: October 2023                                           *
*                                                                     *                                                                     *
*CycleFC is a fork of the original MyNES,                             *
*which is free software: you can redistribute it and/or modify        *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
using System.IO;
using System.Threading;
using CycleCore.Nes.Input;
using CycleCore.Nes.Output.Audio;
using CycleCore.Nes.Output.Video;

namespace CycleCore.Nes
{
    public class NesSystem
    {
        private bool stateSaveRequest;
        private bool stateLoadRequest;
        private int euAddCycle;
        private double currFrameTime;
        private double lastFrameTime;
        private string SaveStatePath = "";

        public Mapper Mapper;
        public ITimer Timer;
        public NesApu Apu;
        public NesCpu Cpu;
        public NesPpu Ppu;
        public NesCartridge Cartridge;
        public NesCpuMemory CpuMemory;
        public NesPpuMemory PpuMemory;
        public Stream StateStream;
        public Stream SaveStream;
        public bool Active;
        public bool PAUSE;
        public bool Paused;
        public bool SoundEnabled = true;
        public bool AutoSaveSRAM = true;
        public bool NoLimiter;
        public ushort FPS;
        public double FramePeriod = (1.0 / 60.0);

        public NesSystem(ITimer timer)
        {
            Apu = new NesApu(this);
            Cpu = new NesCpu(this);
            Ppu = new NesPpu(this);
            CpuMemory = new NesCpuMemory(this);
            PpuMemory = new NesPpuMemory(this);
            Cartridge = new NesCartridge(this);
            Timer = timer;
        }

        public void SetupAudio(IAudioDevice audioDevice)
        {
            Apu.Output = audioDevice;
        }
        public void SetupVideo(IVideoDevice videoDevice)
        {
            Ppu.Output = videoDevice;
        }
        public void SetupInput(IInputDevice inputDevice, IJoypad joyPad1, IJoypad joyPad2)
        {
            CpuMemory.Joypad1 = joyPad1;
            CpuMemory.Joypad2 = joyPad2;
            CpuMemory.InputDevice = inputDevice;
        }
        public void SetTVFormat(RegionFormat tvFormat, PaletteFormat paletteFormat)
        {
            //Apply ntsc palette
            NTSCPaletteGenerator.brightness = paletteFormat.Brightness;
            NTSCPaletteGenerator.contrast = paletteFormat.Contrast;
            NTSCPaletteGenerator.gamma = paletteFormat.Gamma;
            NTSCPaletteGenerator.hue_tweak = paletteFormat.Hue;
            NTSCPaletteGenerator.saturation = paletteFormat.Saturation;
            NesPalette.NTSCPalette = NTSCPaletteGenerator.GeneratePalette();
            //Set palette
            switch (tvFormat)
            {
                case RegionFormat.NTSC:
                    Ppu.scanlinesPerFrame = 261;
                    FramePeriod = 1.0 / 60.0988;
                    Apu.SetNesFrequency(1789772.5f);
                    try
                    {
                        Apu.ChannelDpm.IsPAL = false;
                    }
                    catch { }
                    if (paletteFormat.UseInternalPalette)
                    {
                        switch (paletteFormat.UseInternalPaletteMode)
                        {
                            case UseInternalPaletteMode.Auto:
                                Ppu.Palette = NesPalette.NTSCPalette;
                                break;
                            case UseInternalPaletteMode.NTSC:
                                Ppu.Palette = NesPalette.NTSCPalette;
                                break;
                            case UseInternalPaletteMode.PAL:
                                Ppu.Palette = NesPalette.PALPalette;
                                break;
                        }

                    }
                    else
                    {
                        if (NesPalette.LoadPalette(new FileStream(paletteFormat.ExternalPalettePath, FileMode.Open, FileAccess.Read)) != null)
                        {
                            Ppu.Palette = NesPalette.LoadPalette(new FileStream(paletteFormat.ExternalPalettePath, FileMode.Open, FileAccess.Read));
                        }
                        else
                        {
                            Ppu.Palette = NesPalette.NTSCPalette;
                        }
                    }
                    break;
                case RegionFormat.PAL:
                    Ppu.scanlinesPerFrame = 311;
                    FramePeriod = 1.0 / 50.0070;
                    Apu.SetNesFrequency(1773447.4f);
                    try
                    {
                        Apu.ChannelDpm.IsPAL = true;
                    }
                    catch { }
                    if (paletteFormat.UseInternalPalette)
                    {
                        switch (paletteFormat.UseInternalPaletteMode)
                        {
                            case UseInternalPaletteMode.Auto:
                                Ppu.Palette = NesPalette.PALPalette;
                                break;
                            case UseInternalPaletteMode.NTSC:
                                Ppu.Palette = NesPalette.NTSCPalette;
                                break;
                            case UseInternalPaletteMode.PAL:
                                Ppu.Palette = NesPalette.PALPalette;
                                break;
                        }

                    }
                    else
                    {
                        if (NesPalette.LoadPalette(new FileStream(paletteFormat.ExternalPalettePath, FileMode.Open, FileAccess.Read)) != null)
                        {
                            Ppu.Palette = NesPalette.LoadPalette(new FileStream(paletteFormat.ExternalPalettePath, FileMode.Open, FileAccess.Read));
                        }
                        else
                        {
                            Ppu.Palette = NesPalette.PALPalette;
                        }
                    }
                    break;
            }
        }

        public void TurnOn()
        {
            Active = true;
        }

        public void Execute()
        {
            CpuMemory.Initialize();
            PpuMemory.Initialize();
            Cartridge.Initialize();
            Apu.Initialize();
            Cpu.Initialize();
            Ppu.Initialize();

            while (Active)
            {
                if (!PAUSE)
                {
                    Paused = false;
                    int cycles = Cpu.Execute();

                    while (cycles-- != 0)
                    {
                        ClockComponents();
                    }

                    Apu.Play();//When paused
                }
                else
                {
                    Paused = true;
                    Apu.Stop();
                    if (stateSaveRequest)
                        SaveState();
                    if (stateLoadRequest)
                        LoadState();
                }
            }
        }
        public void ClockComponents()
        {
            Apu.Execute();

            Mapper.TickCycleTimer(1);

            for (int i = 0; i < 3; i++)
                Ppu.Execute();

            if (Ppu.Region == RegionFormat.PAL)
            {
                euAddCycle++;
                if (euAddCycle == 5)
                {
                    Ppu.Execute();
                    euAddCycle = 0;
                }
            }
            if (Ppu.FrameDone)
            {
                if (SoundEnabled)
                    Apu.UpdateBuffer();
                Ppu.FrameDone = false;

                if (NoLimiter)
                {
                    Apu.ClearBuffer();
                }
                else
                {
                    currFrameTime = Timer.GetCurrentTime() - lastFrameTime;

                    while (currFrameTime < FramePeriod)
                    {
                        if ((Timer.GetCurrentTime() - lastFrameTime) >= FramePeriod)
                        {
                            break;
                        }
                    }
                }

                lastFrameTime = Timer.GetCurrentTime();
                FPS++;
            }
        }

        public void ShutDown()
        {
            PAUSE = true;
            Apu.Shutdown();
            Ppu.Output.Dispose();
            if (Cartridge.HasSaveRam & AutoSaveSRAM)
                SaveSRAM(Cartridge.SavePath);
            SaveStream.Close();
            Active = false;
            CONSOLE.WriteLine(this,"SHUTDOWN", DebugStatus.Error);
            CONSOLE.WriteSeparateLine(this, DebugStatus.None);
        }
        public void SaveSRAM(string FilePath)
        {
            //If we have save RAM, try to save it
            try
            {
                SaveStream.Position = 0;
                SaveStream.Write(CpuMemory.srm, 0, 0x2000);
                CONSOLE.WriteLine(this, "SRAM saved !!", DebugStatus.Cool);
            }
            catch
            {
                CONSOLE.WriteLine(this, "Could not save S-RAM.", DebugStatus.Error);
            }
        }
        public void SaveStateRequest(string fileName, Stream stateStream)
        {
            this.StateStream = stateStream;
            PAUSE = true;
            SaveStatePath = fileName;
            stateSaveRequest = true;
        }
        public void LoadStateRequest(string fileName, Stream stateStream)
        {
            this.StateStream = stateStream;
            PAUSE = true;
            SaveStatePath = fileName;
            stateLoadRequest = true;
        }

        void LoadState()
        {
            StateStream st = new StateStream(this.StateStream);
            //check header
            if (!st.ReadHeader(Cartridge.Sha1))//23
            {
                stateLoadRequest = false;
                PAUSE = false;
                CONSOLE.WriteLine(this, "Unable to load state file, unknown file format", DebugStatus.Notification);
                CONSOLE.WriteLine(this, "Unable to load state file, unknown file format", DebugStatus.Error);
                st.Close();
                return;
            }
            //load
            Cpu.LoadState(st);//
            Cartridge.LoadState(st);
            CpuMemory.LoadState(st);
            PpuMemory.LoadState(st);
            Ppu.LoadState(st);
            Apu.LoadState(st);
            Mapper.LoadState(st);
            //close   
            CONSOLE.WriteLine(this, "State loaded", DebugStatus.Notification);
            st.Close();
            PAUSE = false;
            stateLoadRequest = false;
        }
        void SaveState()
        {
            StateStream st = new StateStream(this.StateStream);
            st.WriteHeader(Cartridge.Sha1);//23

            //save 
            Cpu.SaveState(st);//56
            Cartridge.SaveState(st);//??
            CpuMemory.SaveState(st);//10363
            PpuMemory.SaveState(st);//14751
            Ppu.SaveState(st);//16961
            Apu.SaveState(st);//17361
            Mapper.SaveState(st);//

            //close
            CONSOLE.WriteLine(this, "State saved", DebugStatus.Notification);
            st.Close();
            PAUSE = false;
            stateSaveRequest = false;
        }

        public LoadRomStatus LoadRom(string fileName, Stream fileStream, Stream saveStream)
        {
            this.SaveStream = saveStream;
            return Cartridge.Load(fileName, fileStream, saveStream, false);
        }
    }
    public enum RegionFormat
    {
        PAL,
        NTSC
    }
}