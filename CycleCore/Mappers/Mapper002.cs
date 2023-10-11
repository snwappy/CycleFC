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
   
    class Mapper02 : Mapper
    {
        public Mapper02(NesSystem nesSystem)
            : base(nesSystem) { }
        public override string Name
        {
            get
            {
                return "UNROM";
            }
        }
        public override void Poke(int address, byte data)
        {
            if ((address >= 0x8000) && (address <= 0xFFFF))
            {
                cpuMemory.Switch16kPrgRom(data * 4, 0);
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch16kPrgRom(0, 0);
            cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);
            if (cartridge.HasCharRam)
            {
                cpuMemory.FillChr(16);
            }
            cpuMemory.Switch8kChrRom(0);
        }
    }
}
