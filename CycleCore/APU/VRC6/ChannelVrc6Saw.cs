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
using System;

namespace CycleCore.Nes
{
    public class ChannelVrc6Saw : NesApuChannel
    {
        bool enabled = false;
        int accum = 0;
        int accumRate = 0;
        int accumStep = 0;

        public ChannelVrc6Saw(NesSystem nes)
            : base(nes)
        {
        }

        void UpdateFrequency()
        {
            frequency = nesSystem.Apu.ClockFreq / (freqTimer + 1);
            sampleDelay = 44100.00 / (frequency);
        }

        public override void ClockHalf() { }
        public override void ClockQuad() { }
        public override void Poke1(int addr, byte data)
        {
            accumRate = (byte)(data & 0x3F);
        }
        public override void Poke2(int addr, byte data)
        {
            freqTimer = (freqTimer & 0x0F00) | ((data & 0xFF) << 0);
            UpdateFrequency();
        }
        public override void Poke3(int addr, byte data)
        {
            freqTimer = (freqTimer & 0x00FF) | ((data & 0x0F) << 8);
            UpdateFrequency();

            enabled = (data & 0x80) != 0;
        }
        public override void Poke4(int addr, byte data) { }
        public override int RenderSample()
        {
            if (enabled)
            {
                sampleTimer++;

                if (sampleTimer >= sampleDelay)
                {
                    sampleTimer -= sampleDelay;

                    switch (++accumStep)
                    {
                    case 2:
                    case 4:
                    case 6:
                    case 8:
                    case 10:
                    case 12:
                        accum += accumRate;
                        break;

                    case 14:
                        accum = 0;
                        accumStep = 0;
                        break;
                    }
                }

                return (accum >> 3) & 0x1F;
            }

            return 0;
        }
        public override void SaveState(StateStream stateStream)
        {
            stateStream.Write(accum);
            stateStream.Write(accumRate);
            stateStream.Write(accumStep);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            accum = stateStream.ReadInt32();
            accumRate = stateStream.ReadInt32();
            accumStep = stateStream.ReadInt32();
            base.LoadState(stateStream);
        }
    }
}