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
    class Mapper185 : Mapper
    {
        byte patch = 0;
        public Mapper185(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            if (((patch == 0) && ((data & 0x03) == 0x03)) || ((patch == 1) && data == 0x21))
            {
                cpuMemory.Switch8kChrRom(0);
            }
            else
            {
                byte fill = 2;
                cpuMemory.Switch1kChrRom(fill, 0);
                cpuMemory.Switch1kChrRom(fill, 1);
                cpuMemory.Switch1kChrRom(fill, 2);
                cpuMemory.Switch1kChrRom(fill, 3);
                cpuMemory.Switch1kChrRom(fill, 4);
                cpuMemory.Switch1kChrRom(fill, 5);
                cpuMemory.Switch1kChrRom(fill, 6);
                cpuMemory.Switch1kChrRom(fill, 7);
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
            if (cartridge.PrgPages == 1)
            {
                cpuMemory.Switch16kPrgRom(0, 1);
            }
            for (int i = 0; i < 1024; i++)
            {
                
            }

          //  if (cartridge.Hash == -1208487807)
          //  {
         //       patch = 1;
          //  }
        }
    }
}