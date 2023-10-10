﻿/*********************************************************************\
*This file is part of My Nes                                          *
*A Nintendo Entertainment System Emulator.                            *
*                                                                     *
*Copyright © Ala Hadid 2009 - 2011                                    *
*E-mail: mailto:ahdsoftwares@hotmail.com                              *
*                                                                     *
*My Nes is free software: you can redistribute it and/or modify       *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*My Nes is distributed in the hope that it will be useful,            *
H:\Snappy Codes\NES\CyclesFC*but WITHOUT ANY WARRANTY; without even the implied warranty of       *
*MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the        *
*GNU General Public License for more details.                         *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
using System;

namespace MyNes.Nes
{
    public class ChannelDpm : NesApuChannel
    {
        private static readonly int[] FrequencyEu = 
        { 
            0x018E, 0x0161, 0x013C, 0x0129, 0x010A, 0x00EC, 0x00D2, 0x00C7,
            0x00B1, 0x0095, 0x0084, 0x0077, 0x0062, 0x004E, 0x0043, 0x0032,
        };
        private static readonly int[] FrequencyUs = 
        { 
            0x01AC, 0x017C, 0x0154, 0x0140, 0x011E, 0x00FE, 0x00E2, 0x00D6,
            0x00BE, 0x00A0, 0x008E, 0x0080, 0x006A, 0x0054, 0x0048, 0x0036,
        };

        bool dmaEnabled = false;
        bool dmaLooping = false;
        int dmaAddr = 0;
        int dmaAddrRefresh = 0;
        int dmaBits = 0;
        int dmaByte = 0;
        int dmaSize = 0;
        int dmaSizeRefresh = 0;
        int output = 0;
        public bool IsPAL = false;
        public bool IrqEnabled = false;

        public bool Enabled
        {
            get
            {
                return dmaSize != 0;
            }
            set
            {
                if (dmaEnabled = value)
                {
                    if (dmaSize <= 0)
                    {
                        dmaSize = dmaSizeRefresh;
                        dmaBits = 7;
                        dmaAddr = dmaAddrRefresh;
                    }
                }
                else
                {
                    dmaSize = 0;
                }
            }
        }

        public ChannelDpm(NesSystem nes)
            : base(nes)
        {
        }

        void UpdateFrequency()
        {
            frequency = nesSystem.Apu.ClockFreq / (freqTimer + 1);
            sampleDelay = 44100.00 / (frequency);
        }

        public override void ClockHalf()
        {
            throw new NotImplementedException();
        }
        public override void ClockQuad()
        {
            throw new NotImplementedException();
        }
        public override void Poke1(int addr, byte data)
        {
            IrqEnabled = (data & 0x80) != 0;
            dmaLooping = (data & 0x40) != 0;

            this.nesSystem.Apu.DeltaIrqPending &= IrqEnabled;

            if (!IrqEnabled)
                nesSystem.Cpu.Interrupt(NesCpu.IsrType.Delta, false);

            freqTimer = IsPAL ? FrequencyEu[data & 0x0F] : FrequencyUs[data & 0x0F];
            UpdateFrequency();
        }
        public override void Poke2(int addr, byte data)
        {
            output = (data & 0x7F);
        }
        public override void Poke3(int addr, byte data)
        {
            dmaAddrRefresh = (data << 6) | 0xC000;
        }
        public override void Poke4(int addr, byte data)
        {
            dmaSizeRefresh = (data << 4) | 0x0001;
        }
        public override int RenderSample()
        {
            if (dmaEnabled)
            {
                sampleTimer++;

                if (sampleTimer > sampleDelay)
                {
                    sampleTimer -= sampleDelay;

                    if (dmaBits == 7)
                    {
                        if (dmaSize != 0)
                        {
                            dmaBits = 0;
                            dmaByte = nesSystem.CpuMemory[addr: (ushort)dmaAddr];

                            dmaAddr = ((dmaAddr + 1) & 0x7FFF) | 0x8000;
                            dmaSize--;

                            if (dmaSize == 0)
                            {
                                if (dmaLooping)
                                {
                                    dmaAddr = dmaAddrRefresh;
                                    dmaSize = dmaSizeRefresh;
                                }
                                else if (IrqEnabled)
                                {
                                    nesSystem.Apu.DeltaIrqPending = true;
                                }
                            }
                        }
                        else
                        {
                            dmaEnabled = false;
                        }
                    }
                    else
                    {
                        dmaBits = (dmaBits + 1);
                        dmaByte = (dmaByte / 2);
                    }

                    if (dmaEnabled)
                    {
                        if ((dmaByte & 0x01) != 0)
                        {
                            output = Math.Min(output + 2, 0x7F);
                        }
                        else
                        {
                            output = Math.Max(output - 2, 0x00);
                        }
                    }
                }
            }

            if (nesSystem.Apu.DeltaIrqPending)
                nesSystem.Cpu.Interrupt(NesCpu.IsrType.Delta, true);

            return output;
        }

        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(dmaEnabled, dmaLooping, IrqEnabled);
            stateStream.Write(dmaAddr);
            stateStream.Write(dmaAddrRefresh);
            stateStream.Write(dmaBits);
            stateStream.Write(dmaByte);
            stateStream.Write(dmaSize);
            stateStream.Write(dmaSizeRefresh);
            stateStream.Write(output);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            bool[] status = stateStream.ReadBooleans();
            dmaEnabled = status[0];
            dmaLooping = status[1];
            IrqEnabled = status[2];

            dmaAddr = stateStream.ReadInt32();
            dmaAddrRefresh = stateStream.ReadInt32();
            dmaBits = stateStream.ReadInt32();
            dmaByte = stateStream.ReadInt32();
            dmaSize = stateStream.ReadInt32();
            dmaSizeRefresh = stateStream.ReadInt32();
            output = stateStream.ReadInt32();
            base.LoadState(stateStream);
        }
    }
}