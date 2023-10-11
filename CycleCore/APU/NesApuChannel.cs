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
using System.IO;

namespace CycleCore.Nes
{
    public abstract class NesApuChannel : Component
    {
        protected static byte[] DurationTable = 
        {
            0x0A, 0xFE, 0x14, 0x02, 0x28, 0x04, 0x50, 0x06, 0xA0, 0x08, 0x3C, 0x0A, 0x0E, 0x0C, 0x1A, 0x0E,
            0x0C, 0x10, 0x18, 0x12, 0x30, 0x14, 0x60, 0x16, 0xC0, 0x18, 0x48, 0x1A, 0x10, 0x1C, 0x20, 0x1E,
        };

        protected int freqTimer;
        protected double frequency;
        protected double sampleDelay;
        protected double sampleTimer;

        public bool Audible { get; set; }

        public NesApuChannel(NesSystem nesSystem)
            : base(nesSystem)
        {
            Audible = true;
        }

        public abstract void ClockHalf();
        public abstract void ClockQuad();
        public abstract void Poke1(int addr, byte data);
        public abstract void Poke2(int addr, byte data);
        public abstract void Poke3(int addr, byte data);
        public abstract void Poke4(int addr, byte data);

        public virtual int RenderSample() { return 0; }
        public virtual int RenderSample(float rate) { return 0; }

        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(freqTimer);
            stateStream.Write(frequency);
            stateStream.Write(sampleDelay);
            stateStream.Write(sampleTimer);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            freqTimer = stateStream.ReadInt32();
            frequency = stateStream.ReadDouble();
            sampleDelay = stateStream.ReadDouble();
            sampleTimer = stateStream.ReadDouble();
            base.LoadState(stateStream);
        }
    }
}