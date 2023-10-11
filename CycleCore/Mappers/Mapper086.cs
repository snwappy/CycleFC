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
    class Mapper86 : Mapper
    {
        byte reg = 0xFF;
        byte cnt = 0;
        public Mapper86(NesSystem nesSystem)
            : base(nesSystem) { }
        public override void Poke(int Address, byte data)
        {
            if (Address == 0x6000)
            {
                cpuMemory.Switch32kPrgRom(((data & 0x30) >> 4) * 8);

                cpuMemory.Switch8kChrRom(((data & 0x03) | ((data & 0x40) >> 4)) * 8);
            }
            else if (Address == 0x7000)
            {
                if ((reg & 0x10) == 0 && (data & 0x10) == 0x10 && cnt == 0)
                {

                    if ((data & 0x0F) == 0
                     || (data & 0x0F) == 5)
                    {
                        cnt = 60;
                    }
                }
                reg = data;
            }
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Map(0x6000, 0x7FFF, Poke);
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
                cpuMemory.FillChr(16);
            cpuMemory.Switch8kChrRom(0);
        }
        public override void TickScanlineTimer()
        {
            if (cnt > 0)
            {
                cnt--;
            }
        }
        public override bool ScanlineTimerAlwaysActive
        {
            get
            {
                return true;
            }
        }

        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(reg); 
            stateStream.Write(cnt);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            reg = stateStream.ReadByte();
            cnt = stateStream.ReadByte();
            base.LoadState(stateStream);
        }
    }
}
