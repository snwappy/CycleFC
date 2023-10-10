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
namespace MyNes.Nes
{
    public class Ss5BExternalComponent : ExternalComponent
    {
        public ChannelSs5BVol ChannelVW1;
        public ChannelSs5BVol ChannelVW2;
        public ChannelSs5BVol ChannelVW3;

        public Ss5BExternalComponent(NesSystem nes)
        {
            ChannelVW1 = new ChannelSs5BVol(nes);
            ChannelVW2 = new ChannelSs5BVol(nes);
            ChannelVW3 = new ChannelSs5BVol(nes);

            MaxOutput = (15 + 15 + 15);
        }

        public override void ClockHalf() { }
        public override void ClockQuad() { }
        public override int RenderSample(float rate)
        {
            int sample = 0;

            if (ChannelVW1.Audible) sample += ChannelVW1.RenderSample(rate);
            if (ChannelVW2.Audible) sample += ChannelVW2.RenderSample(rate);
            if (ChannelVW3.Audible) sample += ChannelVW3.RenderSample(rate);

            return sample;
        }
        public override void SaveState(StateStream stateStream)
        {
            ChannelVW1.SaveState(stateStream);
            ChannelVW2.SaveState(stateStream);
            ChannelVW3.SaveState(stateStream);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            ChannelVW1.LoadState(stateStream);
            ChannelVW2.LoadState(stateStream);
            ChannelVW3.LoadState(stateStream);
            base.LoadState(stateStream);
        }
    }
}