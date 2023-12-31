/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code � Ala Hadid 2009 - 2015                                *
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
   
    class Mapper15 : Mapper
    {
        public Mapper15(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int address, byte data)
        {
            switch (address)
            {
                case 0x8000:
                    if ((data & 0x80) == 0x80)
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 0);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 1);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 3) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 2) * 2, 3);
                    }
                    else
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 0);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 1);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 2) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 3) * 2, 3);
                    }
                    if ((data & 0x40) == 0x40)
                        cartridge.Mirroring = Mirroring.ModeHorz;
                    else
                        cartridge.Mirroring = Mirroring.ModeVert;
                    break;
                case 0x8001:
                    if ((data & 0x80) == 0x80)
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 3);
                    }
                    else
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 3);
                    }
                    break;
                case 0x8002:
                    if ((data & 0x80) == 0x80)
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 0);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 1);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 3);
                    }
                    else
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 0);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 1);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 3);
                    }
                    break;
                case 0x8003:
                    if ((data & 0x80) == 0x80)
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 3);
                    }
                    else
                    {
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 0) * 2, 2);
                        cpuMemory.Switch8kPrgRom(((data & 0x3F) * 2 + 1) * 2, 3);
                    }
                    if ((data & 0x40) == 0x40)
                        cartridge.Mirroring = Mirroring.ModeHorz;
                    else
                        cartridge.Mirroring = Mirroring.ModeVert;
                    break;
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(8);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
