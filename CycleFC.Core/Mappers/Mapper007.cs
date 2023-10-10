/*********************************************************************\
*This file is part of My Nes                                          *
*A Nintendo Entertainment System Emulator.                            *
*                                                                     *
*Copyright � Ala Hadid 2009 - 2011                                    *
*E-mail: mailto:ahdsoftwares@hotmail.com                              *
*                                                                     *
*My Nes is free software: you can redistribute it and/or modify       *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*My Nes is distributed in the hope that it will be useful,            *
*but WITHOUT ANY WARRANTY; without even the implied warranty of       *
*MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the        *
*GNU General Public License for more details.                         *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
namespace MyNes.Nes
{

    class Mapper07 : Mapper
    {
        public Mapper07(NesSystem nesSystem)
            : base(nesSystem) { }
        public override string Name
        {
            get
            {
                return "AOROM/AMROM";
            }
        }
        public override void Poke(int address, byte data)
        {
            cpuMemory.Switch32kPrgRom((data & 0x07) * 8);
            if ((data & 0x10) != 0)
                cartridge.Mirroring = Mirroring.Mode1ScB;
            else
                cartridge.Mirroring = Mirroring.Mode1ScA;
        }
        protected override void Initialize(bool initializing)
        {
            cpuMemory.Switch32kPrgRom(0);
            if (cartridge.HasCharRam)
            {
                cpuMemory.FillChr(16);
            }
            cpuMemory.Switch8kChrRom(0);
            cartridge.Mirroring = Mirroring.Mode1ScA;
        }
    }
}