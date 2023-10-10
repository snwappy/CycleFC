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

namespace MyNes.Nes
{
    public class ChannelNoi : NesApuChannel
    {
        private static readonly int[] FrequencyTable = 
        { 
            0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0060, 0x0080, 0x00A0,
            0x00CA, 0x00FE, 0x017C, 0x01FC, 0x02FA, 0x03F8, 0x07F2, 0x0FE4,
        };

        private NesApuDuration duration;
        private NesApuEnvelope envelope;
        private bool active;
        private int enabled;
        private int shiftBit = 8;
        private int shiftReg = 1;
        private float delay = FrequencyTable[0];
        private float timer;

        public bool Enabled
        {
            get { return duration.Counter != 0; }
            set
            {
                enabled = value ? ~0 : 0;
                duration.Counter &= enabled;
                active = CanOutput();
            }
        }

        public ChannelNoi(NesSystem nes)
            : base(nes) { }

        private bool CanOutput()
        {
            return duration.Counter != 0 && envelope.Sound != 0;
        }

        public override void ClockHalf()
        {
            duration.Clock();
            active = CanOutput();
        }
        public override void ClockQuad()
        {
            envelope.Clock();
            active = CanOutput();
        }
        public override void Poke1(int addr, byte data)
        {
            duration.Enabled = (data & 0x20) == 0;
            envelope.Looping = (data & 0x20) != 0;
            envelope.Enabled = (data & 0x10) != 0;
            envelope.Sound = (data & 0x0F);
        }
        public override void Poke2(int addr, byte data) { }
        public override void Poke3(int addr, byte data)
        {
            delay = FrequencyTable[data & 0x0F];
            shiftBit = (data & 0x80) != 0 ? 8 : 13;
        }
        public override void Poke4(int addr, byte data)
        {
            duration.Counter = DurationTable[data >> 3] & enabled;
            envelope.Refresh = true;
            active = CanOutput();
        }
        public override int RenderSample(float rate)
        {
            float sum = timer;
            timer -= rate;

            if (active)
            {
                if (timer >= 0)
                {
                    if ((shiftReg & 0x01) == 0)
                        return envelope.Sound;

                }
                else
                {
                    if ((shiftReg & 0x01) != 0)
                        sum = 0;

                    do
                    {
                        shiftReg = (shiftReg >> 1) | ((shiftReg << 14 ^ shiftReg << shiftBit) & 0x4000);

                        if ((shiftReg & 0x01) == 0)
                            sum += Math.Min(-timer, delay);

                        timer += delay;
                    }
                    while (timer < 0);

                    return (int)((sum * envelope.Sound) / rate);
                }
            }
            else
            {
                while (timer < 0)
                {
                    shiftReg = (shiftReg >> 1) | ((shiftReg << 14 ^ shiftReg << shiftBit) & 0x4000);
                    timer += delay;
                }
            }

            return 0;
        }

        public override void SaveState(StateStream stateStream)
        {
            duration.SaveState(stateStream);
            envelope.SaveState(stateStream);
            stateStream.Write(shiftBit);
            stateStream.Write(shiftReg);
            stateStream.Write(delay);
            stateStream.Write(timer);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            duration.LoadState(stateStream);
            envelope.LoadState(stateStream);
            shiftBit = stateStream.ReadInt32();
            shiftReg = stateStream.ReadInt32();
            delay = stateStream.ReadFloat();
            timer = stateStream.ReadFloat();
            base.LoadState(stateStream);
        }
    }
}