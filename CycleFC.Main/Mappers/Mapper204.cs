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
namespace MyNes.Nes
{
    class Mapper204 : Mapper
    {
        public Mapper204(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            if ((Address & 0x10) == 0x10)
                cartridge.Mirroring = Mirroring.ModeHorz;
            else
                cartridge.Mirroring = Mirroring.ModeVert;

            data = (byte)(Address >> 1 & Address >> 2 & 0x1);
            cpuMemory.Switch8kChrRom((Address & (byte)~data) * 8);
            cpuMemory.Switch16kPrgRom((Address & (byte)~data) * 4, 0);
            cpuMemory.Switch16kPrgRom((Address | data) * 4, 1);

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
