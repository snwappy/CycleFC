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
using MyNes.Nes.Input;

namespace MyNes.Nes
{
    public class NesCpuMemory : NesMemory
    {
        public const int AddrBusLines = 16;

        private byte inputData1;
        private byte inputData2;
        private byte inputStrobe;
        private byte[] ram = new byte[0x0800];
        public byte[] srm = new byte[0x2000];


        public IInputDevice InputDevice;
        public IJoypad Joypad1;
        public IJoypad Joypad2;
        public bool ResetTrigger;
        public bool SRamReadOnly;
        public int[] ChrBank = new int[0x08];
        public int[] PrgBank = new int[0x08];

        public NesCpuMemory(NesSystem nesSystem)
            : base(nesSystem, 1 << AddrBusLines) { }

        private byte PeekPad(int addr)
        {
            var data = (byte)(0x40 | (inputData1 & 1));
            inputData1 = (byte)(0x80 | (inputData1 >> 1));
            return data;
        }
        private void PokePad(int addr, byte data)
        {
            if (inputStrobe > (data & 0x01))
            {
                InputDevice.Update();
                inputData1 = Joypad1.GetData();
                inputData2 = Joypad2.GetData();
            }

            inputStrobe = (byte)(data & 0x01);
        }
        private byte PeekPrg(int addr)
        {
            return cartridge.Prg[PrgBank[addr >> 12 & 0x07]][addr & 0x0FFF];
        }
        private void PokePrg(int addr, byte data)
        {
            nesSystem.Mapper.Poke(addr, data);
        }
        private byte PeekRam(int addr)
        {
            return ram[addr & 0x07FF];
        }
        private void PokeRam(int addr, byte data)
        {
            ram[addr & 0x07FF] = data;
        }
        private byte PeekSrm(int addr)
        {
            return srm[addr & 0x1FFF];
        }
        private void PokeSrm(int addr, byte data)
        {
            if (!SRamReadOnly)
                srm[addr & 0x1FFF] = data;
        }

        protected override void Initialize(bool initializing)
        {
            if (initializing)
            {
                CONSOLE.WriteLine(this, "Initializing CPU Memory....", DebugStatus.None);
                base.Map(0x0000, 0x1FFF, PeekRam, PokeRam);
                base.Map(0x6000, 0x7FFF, PeekSrm, PokeSrm);
                base.Map(0x8000, 0xFFFF, PeekPrg, PokePrg);

                base.Map(0x4016, PeekPad, PokePad);
                CONSOLE.WriteLine(this, "CPU Memory Initialized OK.", DebugStatus.Cool);
            }

            base.Initialize(initializing);
        }

        public void FillChr(int count)
        {
            cartridge.Chr = new byte[count][];

            for (int i = 0; i < count; i++)
                cartridge.Chr[i] = new byte[1024];
        }

        public void Switch1kChrRom(int start, int area)
        {
            start = (start & (cartridge.Chr.Length - 1));

            ChrBank[area] = (start);
        }
        public void Switch2kChrRom(int start, int area)
        {
            start = (start & (cartridge.Chr.Length - 1));

            for (int i = 0; i < 2; i++)
                ChrBank[2 * area + i] = (start++);
        }
        public void Switch4kChrRom(int start, int area)
        {
            start = (start & (cartridge.Chr.Length - 1));

            for (int i = 0; i < 4; i++)
                ChrBank[4 * area + i] = (start++);
        }
        public void Switch8kChrRom(int start)
        {
            start = (start & (cartridge.Chr.Length - 1));

            for (int i = 0; i < 8; i++)
                ChrBank[i] = (start++);
        }

        public void Switch8kPrgRom(int start, int area)
        {
            start = (start & (cartridge.Prg.Length - 1));

            for (int i = 0; i < 2; i++)
                PrgBank[2 * area + i] = (start++);
        }
        public void Switch16kPrgRom(int start, int area)
        {
            start = (start & (cartridge.Prg.Length - 1));

            for (int i = 0; i < 4; i++)
                PrgBank[4 * area + i] = (start++);
        }
        public void Switch32kPrgRom(int start)
        {
            start = (start & (cartridge.Prg.Length - 1));

            for (int i = 0; i < 8; i++)
                PrgBank[i] = (start++);
        }
        public void Switch8kPrgRomToSRAM(int start)
        {
            switch (cartridge.PrgPages)
            {
                case (2): start = (start & 0x7); break;
                case (4): start = (start & 0xf); break;
                case (8): start = (start & 0x1f); break;
                case (16): start = (start & 0x3f); break;
                case (32): start = (start & 0x7f); break;
                case (64): start = (start & 0xff); break;
                case (128): start = (start & 0x1ff); break;
            }
            cartridge.Prg[start].CopyTo(srm, 0);
            for (int i = 0x1000; i < 0x2000; i++)
            {
                srm[i] = cartridge.Prg[start + 1][i - 0x1000];
            }
        }

        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(inputData1);
            stateStream.Write(inputData2);
            stateStream.Write(inputStrobe);
            stateStream.Write(ram);
            stateStream.Write(srm);
            stateStream.Write(ChrBank);
            stateStream.Write(PrgBank);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            inputData1 = stateStream.ReadByte();
            inputData2 = stateStream.ReadByte();
            inputStrobe = stateStream.ReadByte();
            stateStream.Read(ram);
            stateStream.Read(srm);
            stateStream.Read(ChrBank);
            stateStream.Read(PrgBank);
            base.LoadState(stateStream);
        }
    }
}