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
    class Mapper70 : Mapper
    {
        public Mapper70(NesSystem nesSystem)
            : base(nesSystem) { }

        public override void Poke(int Address, byte data)
        {
            cpuMemory.Switch16kPrgRom(((data & 0x70) >> 4) * 4, 0);
            cpuMemory.Switch8kChrRom((data & 0x0F) * 8);

            if ((data & 0x80) != 0)
                cartridge.Mirroring = Mirroring.Mode1ScB;
            else
                cartridge.Mirroring = Mirroring.Mode1ScA;
        }
        protected override void Initialize(bool initializing)
        {
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);

            cpuMemory.Switch8kChrRom(0);
            cpuMemory.Switch16kPrgRom(0, 0);
            cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);
        }
    }
}