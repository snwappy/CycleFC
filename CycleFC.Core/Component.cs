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
using System;
using System.IO;

namespace MyNes.Nes
{
    public class Component : IDisposable
    {
        protected NesSystem nesSystem;
        protected NesApu apu;
        protected NesCpu cpu;
        protected NesPpu ppu;
        protected NesCartridge cartridge;
        protected NesCpuMemory cpuMemory;
        protected NesPpuMemory ppuMemory;

        public Component(NesSystem nesSystem)
        {
            this.nesSystem = nesSystem;
        }

        protected virtual void Dispose(bool disposing) { }
        protected virtual void Initialize(bool initializing) { }

        public virtual void LoadState(StateStream stateStream) { }
        public virtual void SaveState(StateStream stateStream) { }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
        public void Initialize()
        {
            this.apu = nesSystem.Apu;
            this.cpu = nesSystem.Cpu;
            this.ppu = nesSystem.Ppu;
            this.cartridge = nesSystem.Cartridge;
            this.cpuMemory = nesSystem.CpuMemory;
            this.ppuMemory = nesSystem.PpuMemory;

            this.Initialize(true);
        }
    }
}