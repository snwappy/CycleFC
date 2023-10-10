﻿/*********************************************************************\r
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
using System;

namespace CycleCore.Nes
{
    class Mapper188 : Mapper
    {
        public Mapper188(NesSystem nes)
            : base(nes)
        { }
        public override void Poke(int addr, byte data)
        {
            if (data != 0)
            {
                if ((data & 0x10) == 0x10)
                {
                    data &= 0x07;
                    cpuMemory.Switch16kPrgRom(data * 4, 0);
                }
                else
                {
                    cpuMemory.Switch16kPrgRom((data + 8) * 4, 0);
                }
            }
            else
            {
                if (cartridge.PRGSizeInKB == 0x10)
                {
                    cpuMemory.Switch16kPrgRom(7 * 4, 0);
                }
                else
                {
                    cpuMemory.Switch16kPrgRom(8 * 4, 0);
                }
            }
            base.Poke(addr, data);
        }
        protected override void Initialize(bool initializing)
        {
            if (cartridge.PrgPages > 8)
            {
                cpuMemory.Switch16kPrgRom(0, 0);
                cpuMemory.Switch16kPrgRom(14 * 4, 1);
            }
            else
            {
                cpuMemory.Switch16kPrgRom(0, 0);
                cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);
            }
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
            base.Initialize(initializing);
        }
    }
}
