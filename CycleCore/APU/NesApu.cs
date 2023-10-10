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
using CycleCore.Nes.Output.Audio;

namespace CycleCore.Nes
{
    public class NesApu : Component
    {
        private const float OutputMax = sbyte.MaxValue;
        private const float OutputMin = sbyte.MinValue;
        private const float OutputMul = 256;
        private const float OutputSub = 128;

        private int rPos;
        private int wPos;
        private int[] soundBuffer = new int[44100];
        private float sampleDelay = (1789772.72f / 44100.00f) - 0.2f;
        public float ClockFreq = 1789772.72f;
        private float sampleTimer;

        public IAudioDevice Output;
        public ChannelSqr ChannelSq1;
        public ChannelSqr ChannelSq2;
        public ChannelTri ChannelTri;
        public ChannelNoi ChannelNoi;
        public ChannelDpm ChannelDpm;
        public ExternalComponent External;
        public bool DeltaIrqPending;
        public bool FrameIrqEnabled;
        public bool FrameIrqPending;
        public bool SequencingMode;
        public int Cycles = 0;
        bool oddCycle = false;

        public NesApu(NesSystem nesSystem)
            : base(nesSystem)
        {
            ChannelSq1 = new ChannelSqr(nesSystem);
            ChannelSq2 = new ChannelSqr(nesSystem);
            ChannelTri = new ChannelTri(nesSystem);
            ChannelNoi = new ChannelNoi(nesSystem);
            ChannelDpm = new ChannelDpm(nesSystem);
        }

        public void SetNesFrequency(float frequency)
        {
            ClockFreq = frequency;
            sampleDelay = (frequency / 44100.00f) - 0.2f;
        }
        private void AddSample(float sampleRate)
        {
            var tndSample = 0;
            var sqrSample = 0;

            if (this.ChannelSq1.Audible) sqrSample += this.ChannelSq1.RenderSample(sampleRate);
            if (this.ChannelSq2.Audible) sqrSample += this.ChannelSq2.RenderSample(sampleRate);

            if (this.ChannelTri.Audible) tndSample += this.ChannelTri.RenderSample(sampleRate) * 3;
            if (this.ChannelNoi.Audible) tndSample += this.ChannelNoi.RenderSample(sampleRate) * 2;
            if (this.ChannelDpm.Audible) tndSample += this.ChannelDpm.RenderSample() * 1;

            var output = (NesApuMixer.MixSamples(sqrSample, tndSample) * OutputMul);

            if (this.External != null)
            {
                output += this.External.RenderSample(sampleRate);
                output *= (OutputMul / (OutputMul + External.MaxOutput));
            }

            if (output > OutputMul)
                output = OutputMul;
            if (output < 0)
                output = 0;

            this.soundBuffer[wPos++ % this.soundBuffer.Length] = (int)(output - OutputSub);
        }
        private void ClockHalf()
        {
            ChannelSq1.UpdateSweep(1);
            ChannelSq2.UpdateSweep(0);
            ChannelSq1.ClockHalf();
            ChannelSq2.ClockHalf();
            ChannelNoi.ClockHalf();
            ChannelTri.ClockHalf();

            if (External != null)
                External.ClockHalf();
        }
        private void ClockQuad()
        {
            ChannelSq1.ClockQuad();
            ChannelSq2.ClockQuad();
            ChannelNoi.ClockQuad();
            ChannelTri.ClockQuad();

            if (External != null)
                External.ClockQuad();
        }

        protected override void Initialize(bool initializing)
        {
            if (initializing)
            {
                CONSOLE.WriteLine(this, "Initializing pAPU...", DebugStatus.None);
                // Channel Sq1
                cpuMemory.Map(0x4000, ChannelSq1.Poke1);
                cpuMemory.Map(0x4001, ChannelSq1.Poke2);
                cpuMemory.Map(0x4002, ChannelSq1.Poke3);
                cpuMemory.Map(0x4003, ChannelSq1.Poke4);

                // Channel Sq2
                cpuMemory.Map(0x4004, ChannelSq2.Poke1);
                cpuMemory.Map(0x4005, ChannelSq2.Poke2);
                cpuMemory.Map(0x4006, ChannelSq2.Poke3);
                cpuMemory.Map(0x4007, ChannelSq2.Poke4);

                // Channel Tri
                cpuMemory.Map(0x4008, ChannelTri.Poke1);
                cpuMemory.Map(0x4009, ChannelTri.Poke2);
                cpuMemory.Map(0x400A, ChannelTri.Poke3);
                cpuMemory.Map(0x400B, ChannelTri.Poke4);

                // Channel Noi
                cpuMemory.Map(0x400C, ChannelNoi.Poke1);
                cpuMemory.Map(0x400D, ChannelNoi.Poke2);
                cpuMemory.Map(0x400E, ChannelNoi.Poke3);
                cpuMemory.Map(0x400F, ChannelNoi.Poke4);

                // Channel Dpm
                cpuMemory.Map(0x4010, ChannelDpm.Poke1);
                cpuMemory.Map(0x4011, ChannelDpm.Poke2);
                cpuMemory.Map(0x4012, ChannelDpm.Poke3);
                cpuMemory.Map(0x4013, ChannelDpm.Poke4);

                cpuMemory.Map(0x4015, Peek4015, Poke4015);
                cpuMemory.Map(0x4017, /*******/ Poke4017);
                CONSOLE.WriteLine(this, "pAPU initialized OK.", DebugStatus.Cool);
            }

            base.Initialize(initializing);
        }

        public void Execute()
        {
            Cycles++;
            oddCycle = !oddCycle;
            if (!SequencingMode)
            {
                //Step 1 | 3
                if (Cycles == 7459 | Cycles == 22373)
                    ClockQuad();

                //Step 2
                if (Cycles == 14916)
                {
                    ClockQuad();
                    ClockHalf();
                }

                if (Cycles == 29831)
                    if (FrameIrqEnabled)
                        FrameIrqPending = true;
                //Step 4
                if (Cycles == 29832)
                {
                    ClockQuad();
                    ClockHalf();
                }
                //SET FRAME IRQ FLAG
                if (Cycles == 29833)
                {
                    if (FrameIrqEnabled)
                        FrameIrqPending = true;
                    Cycles = 3;

                }
                //SET CPU IRQ
                if (FrameIrqPending & Cycles == 4)
                { cpu.Interrupt(NesCpu.IsrType.Frame, true); }
            }
            else
            {
                //Step 0 | 2
                if (Cycles == 2 | Cycles == 14916)
                {
                    ClockQuad();
                    ClockHalf();
                }
                //Step 1 | 3
                if (Cycles == 7459 | Cycles == 22373)
                    ClockQuad();
                //Step 4, do nothing
                if (Cycles == 37282)
                    Cycles = 0;
            }
            if (sampleTimer > 0)
            {
                sampleTimer--;
            }
            else
            {
                sampleTimer += sampleDelay;
                AddSample(sampleDelay);
            }
        }

        public void Play()
        {
            if (Output != null)
                Output.Play();
        }
        public void ClearBuffer()
        {
            wPos = 0;
            rPos = 0;
        }
        public void UpdateBuffer()
        {
            if (Output != null)
                Output.UpdateBuffer();
        }
        public int PullSample()
        {
            while (rPos >= wPos)
            {
                AddSample(sampleDelay);
            }

            return soundBuffer[rPos++ % soundBuffer.Length];
        }
        public void SetVolume(int volume)
        {
            Output.SetVolume(volume);
        }
        public void Shutdown()
        {
            wPos = 0;
            rPos = 0;
            sampleTimer = 0;

            if (Output != null)
            {
                Output.Shutdown();
                Output = null;
            }
        }
        public void SoftReset()
        {
            ChannelSq1.Enabled = false;
            ChannelSq2.Enabled = false;
            ChannelTri.Enabled = false;
            ChannelNoi.Enabled = false;
            ChannelDpm.Enabled = false;

            DeltaIrqPending = false;
            FrameIrqEnabled = false;
            FrameIrqPending = false;
        }
        public void Stop()
        {
            if (Output != null)
                Output.Stop();

            rPos = 0;
            wPos = 0;
        }

        public byte PeekNull(int addr)
        {
            return 0;
        }
        public byte Peek4015(int addr)
        {
            byte data = 0;

            if (ChannelSq1.Enabled) data |= 0x01;
            if (ChannelSq2.Enabled) data |= 0x02;
            if (ChannelTri.Enabled) data |= 0x04;
            if (ChannelNoi.Enabled) data |= 0x08;
            if (ChannelDpm.Enabled) data |= 0x10;
            if (FrameIrqPending) data |= 0x40;
            if (DeltaIrqPending) data |= 0x80;

            FrameIrqPending = false;
            cpu.Interrupt(NesCpu.IsrType.Frame, false);
            return data;
        }
        public void Poke4015(int addr, byte data)
        {
            ChannelSq1.Enabled = (data & 0x01) != 0;
            ChannelSq2.Enabled = (data & 0x02) != 0;
            ChannelTri.Enabled = (data & 0x04) != 0;
            ChannelNoi.Enabled = (data & 0x08) != 0;
            ChannelDpm.Enabled = (data & 0x10) != 0;
            DeltaIrqPending = false;
            cpu.Interrupt(NesCpu.IsrType.Delta, false);
        }
        public void Poke4017(int addr, byte data)
        {
            SequencingMode = (data & 0x80) != 0;
            FrameIrqEnabled = (data & 0x40) == 0;

            if (!oddCycle)
                Cycles = 0;
            else
                Cycles = -1;//delay cycle for jitter

            if (!FrameIrqEnabled)
                FrameIrqPending = false;
        }

        public void Poke5015(byte data)
        {
            if (External != null)
            {
                ((Mmc5ExternalComponent)External).ChannelSq1.Enabled = (data & 0x01) != 0;
                ((Mmc5ExternalComponent)External).ChannelSq2.Enabled = (data & 0x02) != 0;
            }
        }
        public byte Peek5015()
        {
            byte rt = 0;
            if (External != null)
            {
                if (((Mmc5ExternalComponent)External).ChannelSq1.Enabled)
                    rt |= 0x01;
                if (((Mmc5ExternalComponent)External).ChannelSq2.Enabled)
                    rt |= 0x02;
            }
            return rt;
        }

        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(sampleTimer);
            stateStream.Write(Cycles);
            stateStream.Write(DeltaIrqPending, FrameIrqEnabled, FrameIrqPending, SequencingMode, oddCycle);

            ChannelSq1.SaveState(stateStream);
            ChannelSq2.SaveState(stateStream);
            ChannelTri.SaveState(stateStream);
            ChannelNoi.SaveState(stateStream);
            ChannelDpm.SaveState(stateStream);
            if (External != null)
                External.SaveState(stateStream);

            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            sampleTimer = stateStream.ReadFloat();
            Cycles = stateStream.ReadInt32();

            bool[] status = stateStream.ReadBooleans();
            DeltaIrqPending = status[0];
            FrameIrqEnabled = status[1];
            FrameIrqPending = status[2];
            SequencingMode = status[3];
            oddCycle = status[4];

            ChannelSq1.LoadState(stateStream);
            ChannelSq2.LoadState(stateStream);
            ChannelTri.LoadState(stateStream);
            ChannelNoi.LoadState(stateStream);
            ChannelDpm.LoadState(stateStream);
            if (External != null)
                External.LoadState(stateStream);

            base.LoadState(stateStream);
        }
    }
}