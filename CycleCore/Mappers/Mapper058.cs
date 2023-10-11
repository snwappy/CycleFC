/*********************************************************************\r
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
namespace CycleCore.Nes
{
   
    class Mapper58 : Mapper
    {
        public Mapper58(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int address, byte data)
        {
            if ((address & 0x40) == 0x40)
            {
                cpuMemory.Switch16kPrgRom((address & 0x07) * 4, 0);
                cpuMemory.Switch16kPrgRom((address & 0x07) * 4, 1);
            }
            else
            {
                cpuMemory.Switch32kPrgRom(((address & 0x06) >> 1) * 8);
            }

            if (!cartridge.HasCharRam)
            {
                cpuMemory.Switch8kChrRom(((address & 0x38) >> 3) * 8);
            }

            if ((data & 0x02) == 0x02)
                cartridge.Mirroring = Mirroring.ModeVert;
            else
                cartridge.Mirroring = Mirroring.ModeHorz;
        }

        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch32kPrgRom(0);

            if (cartridge.PrgPages == 1)
                cpuMemory.Switch16kPrgRom(0, 1);

            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
