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
namespace CycleCore.Nes
{
    class Mapper201 : Mapper
    {
        public Mapper201(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            byte bank = (byte)(Address & 0x03);
            if ((Address & 0x08) == 0)
                bank = 0;
            cpuMemory.Switch32kPrgRom(bank * 8);
            cpuMemory.Switch8kChrRom(bank * 8);
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch16kPrgRom(0, 0);
            cpuMemory.Switch16kPrgRom(0, 1);

            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
