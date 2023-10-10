﻿/*********************************************************************\r
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
namespace MyNes.Nes
{
    public struct NesApuEnvelope
    {
        public bool Enabled;
        public bool Looping;
        public bool Refresh;
        public int Count;
        public int Delay;
        public int Timer;
        public int Volume;

        public int Sound
        {
            get
            {
                if (this.Enabled)
                {
                    return this.Volume;
                }
                else
                {
                    return this.Count;
                }
            }
            set
            {
                Delay = value;
                if (this.Enabled)
                    Volume = (byte)Delay;
                else
                    Volume = Count;
            }
        }
        public void Clock()
        {
            if (Refresh)
            {
                Refresh = false;
                Timer = Delay;
                Count = 0x0F;
            }
            else
            {
                if (Timer != 0)
                {
                    Timer--;
                }
                else
                {
                    Timer = Delay;

                    if (Looping || Count != 0)
                        Count = (Count - 1) & 0x0F;
                }
            }
        }

        public void SaveState(StateStream stateStream)
        {
            stateStream.Write(Enabled, Looping, Refresh);
            stateStream.Write(Count);
            stateStream.Write(Delay);
            stateStream.Write(Timer);
            stateStream.Write(Volume);
        }
        public void LoadState(StateStream stateStream)
        {
            bool[] status = stateStream.ReadBooleans();
            Enabled = status[0];
            Looping = status[1];
            Refresh = status[2];
            Count = stateStream.ReadInt32();
            Delay = stateStream.ReadInt32();
            Timer = stateStream.ReadInt32();
            Volume = stateStream.ReadInt32();
        }
    }
}