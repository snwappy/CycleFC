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
namespace CycleCore.Nes
{
    public class Vrc6ExternalComponent : ExternalComponent
    {
        public ChannelVrc6Saw ChannelSaw;
        public ChannelVrc6Sqr ChannelSq1;
        public ChannelVrc6Sqr ChannelSq2;

        public Vrc6ExternalComponent(NesSystem nes)
        {
            ChannelSaw = new ChannelVrc6Saw(nes);
            ChannelSq1 = new ChannelVrc6Sqr(nes);
            ChannelSq2 = new ChannelVrc6Sqr(nes);
        }

        public override void ClockHalf() { }
        public override void ClockQuad() { }
        public override int RenderSample(float rate)
        {
            int sample = 0;

            if (ChannelSaw.Audible) sample += ChannelSaw.RenderSample();
            if (ChannelSq1.Audible) sample += ChannelSq1.RenderSample();
            if (ChannelSq2.Audible) sample += ChannelSq2.RenderSample();

            return sample;
        }
        public override void SaveState(StateStream stateStream)
        {
            ChannelSaw.SaveState(stateStream);
            ChannelSq1.SaveState(stateStream);
            ChannelSq2.SaveState(stateStream);
            base.SaveState(stateStream);
        }
        public override void LoadState(StateStream stateStream)
        {
            ChannelSaw.LoadState(stateStream);
            ChannelSq1.LoadState(stateStream);
            ChannelSq2.LoadState(stateStream);
            base.LoadState(stateStream);
        }
    }
}