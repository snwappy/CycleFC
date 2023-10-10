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
using word = System.Int32;

namespace CycleCore.Nes
{
    public class NesMemory : Component
    {
        private PeekRegister[] peek;
        private PokeRegister[] poke;
        private word mask;

        public byte this[int addr]
        {
            get { return peek[addr &= mask](addr); }
            set { poke[addr &= mask](addr, value); }
        }

        public NesMemory(NesSystem nesSystem, int capacity)
            : base(nesSystem)
        {
#if DEBUG
            // Only allow powers of two to be used
            global::System.Diagnostics.Debug.Assert(
                (capacity != 0) &&
                (capacity & (capacity - 1)) == 0);
#endif
            this.peek = new PeekRegister[capacity];
            this.poke = new PokeRegister[capacity];
            this.mask = (capacity - 1);

            this.Map(0x0000, mask, PeekStub, PokeStub);
        }

        private byte PeekStub(int addr)
        {
            return 0;
        }
        private void PokeStub(int addr, byte data)
        {
        }

        public void Map(int addr, PeekRegister peek)
        {
            this.peek[addr] = peek;
        }
        public void Map(int addr, PokeRegister poke)
        {
            this.poke[addr] = poke;
        }
        public void Map(int addr, PeekRegister peek, PokeRegister poke)
        {
            this.peek[addr] = peek;
            this.poke[addr] = poke;
        }
        public void Map(int addr, int last, PeekRegister peek)
        {
            do
            {
                this.peek[addr] = peek;
            }
            while (addr++ != last);
        }
        public void Map(int addr, int last, PokeRegister poke)
        {
            do
            {
                this.poke[addr] = poke;
            }
            while (addr++ != last);
        }
        public void Map(int addr, int last, PeekRegister peek, PokeRegister poke)
        {
            do
            {
                this.peek[addr] = peek;
                this.poke[addr] = poke;
            }
            while (addr++ != last);
        }
    }
}