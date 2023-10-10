/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code � Ala Hadid 2009 - 2011                                *
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
namespace MyNes.Nes
{
    class Mapper76 : Mapper
    {
        byte index = 0;
        public Mapper76(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            switch (Address)
            {
                case 0x8000:
                    index = data;
                    break;
                case 0x8001:
                    switch (index & 0x07)
                    {
                        case 2:
                            cpuMemory.Switch2kChrRom(data * 2, 0);
                            break;
                        case 3:
                            cpuMemory.Switch2kChrRom(data * 2, 1);
                            break;
                        case 4:
                            cpuMemory.Switch2kChrRom(data * 2, 2);
                            break;
                        case 5:
                            cpuMemory.Switch2kChrRom(data * 2, 3);
                            break;
                        case 6:
                            cpuMemory.Switch8kPrgRom(data * 2, 0);
                            break;
                        case 7:
                            cpuMemory.Switch8kPrgRom(data * 2, 1);
                            break;
                    }
                    break;
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch16kPrgRom(0, 0);
            cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
