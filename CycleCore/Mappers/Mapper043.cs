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
   
    class Mapper43 : Mapper
    {
        public Mapper43(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int address, byte data)
        {
            bool mode = (address & 0x0800) == 0x0800;
            
            if (((address & 0x0300) >> 8) == 0)
            {
                if ((address & 0x0800) == 0)
                    cpuMemory.Switch32kPrgRom((address & 0xFF) * 8);
                else
                    cpuMemory.Switch16kPrgRom((address & 0xFF) * 4, ((address & 0x1000) >> 12));
            }
            else if (((address & 0x0300) >> 8) == 1)
            {
                if ((address & 0x0800) == 0)
                    cpuMemory.Switch32kPrgRom(255 + ((address & 0xF) * 8));
                else
                    cpuMemory.Switch16kPrgRom(255 + ((address & 0xF) * 4), ((address & 0x1000) >> 12));
            }
            
            if ((address & 0x2000) == 0x2000)
                cartridge.Mirroring = Mirroring.ModeHorz;
            else
                cartridge.Mirroring = Mirroring.ModeVert;
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(32);
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
