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
   
    class Mapper212 : Mapper
    {
        public Mapper212(NesSystem nesSystem)
            : base(nesSystem) { }

        public override void Poke(int address, byte data)
        {
            cartridge.Mirroring = ((address & 0x8) != 0) ? Mirroring.ModeHorz : Mirroring.ModeVert;
            cpuMemory.Switch8kChrRom((address & 0x7) * 8);
            if ((address & 0x4000) != 0)
            {
                cpuMemory.Switch32kPrgRom(((address & 0x6) >> 1) * 8);
            }
            else
            {
                cpuMemory.Switch16kPrgRom((address & 0x7) * 4, 0);
                cpuMemory.Switch16kPrgRom((address & 0x7) * 4, 1);
            }
        }

        protected override void Initialize(bool initializing)
        {
            //cpuMemory.Map(0x4018, 0x7FFF, Poke);
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
