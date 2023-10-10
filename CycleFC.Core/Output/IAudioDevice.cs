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

namespace MyNes.Nes.Output.Audio
{
    public interface IAudioDevice
    {
        IntPtr Handle { get; }
        NesApu APU { get; set; }
        IWaveRecorder Recorder { get; }
        int Volume { get; }

        void Initialize();
        void Play();
        void SetPan(int pan);
        void SetVolume(int volume);
        void Shutdown();
        void Stop();
        void UpdateBuffer();
    }
}