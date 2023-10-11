﻿/*********************************************************************\r
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
    public abstract class Mapper : Component
    {
        public Mapper(NesSystem nesSystem)
            : base(nesSystem) { }

        /// <summary>
        /// Write a value into this mapper
        /// </summary>
        /// <param name="addr">The address to write into</param>
        /// <param name="data">The data to write</param>
        public virtual void Poke(int addr, byte data) { }
        /// <summary>
        /// Read a value from this mapper
        /// </summary>
        /// <param name="addr">The address to read from</param>
        /// <returns></returns>
        public virtual byte Peek(int addr) { return 0; }
        /// <summary>
        /// Soft reset mapper
        /// </summary>
        public virtual void SoftReset() { }
        /// <summary>
        /// Tick timer which need to tick each cpu cycle
        /// </summary>
        /// <param name="cycles">How many cycles ?</param>
        public virtual void TickCycleTimer(int cycles) { }
        /// <summary>
        /// Tick timer which need to tick each scanline, use "ScanlineTimerAlwaysActive" property to 
        /// control if this timer should pause in vblank period
        /// </summary>
        public virtual void TickScanlineTimer() { }
        /// <summary>
        /// Tick timer which need to tick each ppu cycle = 3*cpu cylcle
        /// </summary>
        public virtual void TickPPUCylceTimer() { }
        /// <summary>
        /// The mapper name
        /// </summary>
        public virtual string Name { get { return "Unknown"; } }
        /// <summary>
        /// Control if scanline timer should pause in vblank period
        /// </summary>
        public virtual bool ScanlineTimerAlwaysActive { get { return false; } }
        /// <summary>
        /// Mapper.ToString()
        /// </summary>
        /// <returns>Mapper name</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}