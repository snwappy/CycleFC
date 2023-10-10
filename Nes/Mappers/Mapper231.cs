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
    class Mapper231 : Mapper
    {
        public Mapper231(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            if ((Address & 0x0020) == 0x0020)
            {
                cpuMemory.Switch32kPrgRom((byte)((Address >> 1) * 8));
            }
            else
            {
                byte bank = (byte)(Address & 0x1E);
                cpuMemory.Switch8kPrgRom((bank * 2 + 0) * 2, 0);
                cpuMemory.Switch8kPrgRom((bank * 2 + 1) * 2, 1);
                cpuMemory.Switch8kPrgRom((bank * 2 + 0) * 2, 2);
                cpuMemory.Switch8kPrgRom((bank * 2 + 1) * 2, 3);
            }

            if ((Address & 0x0080) == 0x0080)
            {
                cartridge.Mirroring = Mirroring.ModeHorz;
            }
            else
            {
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
