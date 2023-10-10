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
    
    class Mapper32 : Mapper
    {
        bool mapper32SwitchingMode = false;
        public Mapper32(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int address, byte data)
        {
            switch (address & 0xF000)
            {
                case 0x8000:
                    if (mapper32SwitchingMode)
                    {
                        cpuMemory.Switch8kPrgRom(data * 2, 2);
                    }
                    else
                    {
                        cpuMemory.Switch8kPrgRom(data * 2, 0);
                    }
                    break;

                case 0x9000:
                    mapper32SwitchingMode = ((data & 0x02) == 0x02);
                    cartridge.Mirroring = ((data & 0x01) == 0) ? Mirroring.ModeVert : Mirroring.ModeHorz;
                    break;

                case 0xA000:
                    cpuMemory.Switch8kPrgRom(data * 2, 1);
                    break;
            }
            switch (address & 0xF007)
            {
                case 0xB000:
                case 0xB001:
                case 0xB002:
                case 0xB003:
                case 0xB004:
                case 0xB005:
                    cpuMemory.Switch1kChrRom(data , address & 0x0007);
                    break;
                case 0xB006:
                    cpuMemory.Switch1kChrRom(data, 6);
                    break;
                case 0xB007:
                    cpuMemory.Switch1kChrRom(data, 7);
                    break;
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch16kPrgRom(0, 0);
            cpuMemory.Switch16kPrgRom((cartridge.PrgPages - 1) * 4, 1);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(mapper32SwitchingMode);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            mapper32SwitchingMode = stateStream.ReadBooleans()[0];
            base.LoadState(stateStream);
        }
    }
}
