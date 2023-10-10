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
    
    class Mapper34 : Mapper
    {
        public Mapper34(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int address, byte data)
        {
            if (address == 0x7ffd)
            {
                cpuMemory.Switch32kPrgRom(data * 8);
            }
            else if (address == 0x7ffe)
            {
                cpuMemory.Switch4kChrRom(data * 4, 0);
            }
            else if (address == 0x7fff)
            {
                cpuMemory.Switch4kChrRom(data * 4, 1);
            }
            else if (address >= 0x8000)
            {
                cpuMemory.Switch32kPrgRom(data * 8);
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Map(0x7ffd, 0xFFFF, Poke);
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
