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
    class Mapper228 : Mapper
    {
        public Mapper228(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            byte prg = (byte)((Address & 0x0780) >> 7);

            switch ((Address & 0x1800) >> 11)
            {
                case 1:
                    prg |= 0x10;
                    break;
                case 3:
                    prg |= 0x20;
                    break;
            }

            if ((Address & 0x0020) == 0x0020)
            {
                prg <<= 1;
                if ((Address & 0x0040) == 0x0040)
                {
                    prg++;
                }
                cpuMemory.Switch8kPrgRom((prg * 4 + 0) * 2, 0);
                cpuMemory.Switch8kPrgRom((prg * 4 + 1) * 2, 1);
                cpuMemory.Switch8kPrgRom((prg * 4 + 0) * 2, 2);
                cpuMemory.Switch8kPrgRom((prg * 4 + 1) * 2, 3);


            }
            else
            {
                cpuMemory.Switch8kPrgRom((prg * 4 + 0) * 2, 0);
                cpuMemory.Switch8kPrgRom((prg * 4 + 1) * 2, 1);
                cpuMemory.Switch8kPrgRom((prg * 4 + 2) * 2, 2);
                cpuMemory.Switch8kPrgRom((prg * 4 + 3) * 2, 3);
            }

            cpuMemory.Switch8kChrRom((((Address & 0x000F) << 2) | (data & 0x03)) * 8);

            if ((Address & 0x2000) == 0x2000)
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
