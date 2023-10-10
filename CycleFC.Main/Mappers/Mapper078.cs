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

namespace MyNes.Nes
{
    class Mapper78 : Mapper
    {
        public Mapper78(NesSystem nes)
            : base(nes)
        { }
        public override void Poke(int addr, byte data)
        {
            cpuMemory.Switch16kPrgRom((data & 0x7) * 4, 0);
            cpuMemory.Switch8kChrRom(((data & 0xF0) >> 4) * 8);
            if ((addr & 0xFE00) != 0xFE00)
            {
                if ((data & 0x8) != 0)
                    cartridge.Mirroring = Mirroring.Mode1ScA;
                else
                    cartridge.Mirroring = Mirroring.Mode1ScB;
            }
            base.Poke(addr, data);
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch16kPrgRom(0, 0);
            cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);

            if (cartridge.HasCharRam)
                cpuMemory.FillChr(8);
            cpuMemory.Switch8kChrRom(0);
            base.Initialize(initializing);
        }
    }
}
