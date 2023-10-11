﻿/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code © Ala Hadid 2009 - 2015                                *
*Code maintainance by Snappy Pupper (@snappypupper on Twitter)        *
*Code updated: October 2023                                           *
*                                                                     *                                                                     
*CycleFC is a fork of the original MyNES,                             *
*which is free software: you can redistribute it and/or modify        *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
using System;

namespace CycleCore.Nes
{
    class Mapper230 : Mapper
    {
        byte rom_sw = 0;
        public Mapper230(NesSystem nes)
            : base(nes)
        { }
        public override void Poke(int addr, byte data)
        {
            if (rom_sw == 1)
            {
                cpuMemory.Switch8kPrgRom(((data & 0x07) * 2 + 0) * 2, 0);
                cpuMemory.Switch8kPrgRom(((data & 0x07) * 2 + 1) * 2, 1);
            }
            else
            {
                if ((data & 0x20) == 0x20)
                {
                    cpuMemory.Switch8kPrgRom(((data & 0x1F) * 2 + 16) * 2, 0);
                    cpuMemory.Switch8kPrgRom(((data & 0x1F) * 2 + 17) * 2, 1);
                    cpuMemory.Switch8kPrgRom(((data & 0x1F) * 2 + 16) * 2, 2);
                    cpuMemory.Switch8kPrgRom(((data & 0x1F) * 2 + 17) * 2, 3);
                }
                else
                {
                    cpuMemory.Switch8kPrgRom(((data & 0x1E) * 2 + 16) * 2, 0);
                    cpuMemory.Switch8kPrgRom(((data & 0x1E) * 2 + 17) * 2, 1);
                    cpuMemory.Switch8kPrgRom(((data & 0x1E) * 2 + 18) * 2, 2);
                    cpuMemory.Switch8kPrgRom(((data & 0x1E) * 2 + 19) * 2, 3);
                }
                if ((data & 0x40) == 0x40)
                {
                    cartridge.Mirroring = Mirroring.ModeVert;
                }
                else
                {
                    cartridge.Mirroring = Mirroring.ModeHorz;
                }
            }
            base.Poke(addr, data);
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.ResetTrigger = true;
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);

            if (rom_sw == 1)
            {
                cpuMemory.Switch16kPrgRom(0, 0);
                cpuMemory.Switch16kPrgRom(14 * 4, 1);
            }
            else
            {
                cpuMemory.Switch16kPrgRom(16 * 4, 0);
                cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);
            }
            base.Initialize(initializing);
        }
        public override void SoftReset()
        {
            if (rom_sw == 1)
            {
                rom_sw = 0;
            }
            else
            {
                rom_sw = 1;
            }
            base.SoftReset();
        }
    }
}
