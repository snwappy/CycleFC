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
namespace CycleCore.Nes
{
    class Mapper62 : Mapper
    {
        public Mapper62(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int address, byte data)
        {
            if (address >= 0x8000 & address <= 0xBFFF)
            {
                cpuMemory.Switch8kChrRom(((address << 2) | (data & 0x3)) * 8);

                int data1 = ((address & 0x40) | (address >> 8 & 0x3F));
                int address1 = ~address >> 6 & 0x1;
                cpuMemory.Switch16kPrgRom((data1 & ~address1) * 4, 0);
                cpuMemory.Switch16kPrgRom((data1 | address1) * 4, 1);

                if ((address & 0x80) == 0x80)
                    cartridge.Mirroring = Mirroring.ModeHorz;
                else
                    cartridge.Mirroring = Mirroring.ModeVert;
            }
        }

        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
