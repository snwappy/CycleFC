﻿/*********************************************************************\r
*This file is part of CycleFC                                         *               
*                                                                     *
*Original code © Ala Hadid 2009 - 2011                                *
*Code maintainance by Snappy Pupper (@snappypupper on Twitter)        *
*Code updated: October 2023                                           *
*                                                                     *                                                                     *
*CycleFC is a fork of the original CycleMain,                             *
*which is free software: you can redistribute it and/or modify        *
*it under the terms of the GNU General Public License as published by *
*the Free Software Foundation, either version 3 of the License, or    *
*(at your option) any later version.                                  *
*                                                                     *
*You should have received a copy of the GNU General Public License    *
*along with this program.  If not, see <http://www.gnu.org/licenses/>.*
\*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CycleCore.Nes.Output.Audio;
namespace CycleMain
{
    public class WaveRecorder : IWaveRecorder
    {
        Stream STR;
        bool _IsRecording = false;
        int SIZE = 0;
        int NoOfSamples = 0;
        int _Time = 0;
        int TimeSamples = 0;
        bool STEREO = false;
        public void Record(string FilePath, bool Stereo)
        {
            STEREO = Stereo;
            _Time = 0;
            //Create the stream first
            STR = new FileStream(FilePath, FileMode.Create);
            ASCIIEncoding ASCII = new ASCIIEncoding();
            //1 Write the header "RIFF"
            STR.Write(ASCII.GetBytes("RIFF"), 0, 4);
            //2 Write Chunck Size (0 for now)
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            //3 Write WAVE
            STR.Write(ASCII.GetBytes("WAVE"), 0, 4);
            //4 Write "fmt "
            STR.Write(ASCII.GetBytes("fmt "), 0, 4);
            //5 Write Chunck Size (16 for PCM)
            STR.WriteByte(0x10);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            //6 Write audio format (1 = PCM)
            STR.WriteByte(0x01);
            STR.WriteByte(0x00);
            //7 Number of channels
            if (STEREO)
            {
                STR.WriteByte(0x02);
                STR.WriteByte(0x00);
            }
            else
            {
                STR.WriteByte(0x01);
                STR.WriteByte(0x00);
            }
            //8 Sample Rate (44100)
            STR.WriteByte(0x44);
            STR.WriteByte(0xAC);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            //9 Byte Rate 
            if (STEREO)//(176400)
            {
                STR.WriteByte(0x00);
                STR.WriteByte(0xEE);
                STR.WriteByte(0x02);
                STR.WriteByte(0x00);
            }
            else//(88200)
            {
                STR.WriteByte(0x88);
                STR.WriteByte(0x58);
                STR.WriteByte(0x01);
                STR.WriteByte(0x00);
            }
            //10 Block Align
            if (STEREO)//(4)
            {
                STR.WriteByte(0x04);
                STR.WriteByte(0x00);
            }
            else //(2)
            {
                STR.WriteByte(0x02);
                STR.WriteByte(0x00);
            }
            //11 Bits Per Sample (16)
            STR.WriteByte(0x10);
            STR.WriteByte(0x00);
            //12 Write "data"
            STR.Write(ASCII.GetBytes("data"), 0, 4);
            //13 Write Chunck Size (0 for now)
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            STR.WriteByte(0x00);
            //Confirm
            _IsRecording = true;
        }
        public void AddSample(short Sample)
        {
            if (!_IsRecording)
                return;
            STR.WriteByte((byte)((Sample & 0xFF00) >> 8));
            STR.WriteByte((byte)(Sample & 0xFF));
            if (STEREO)//Add the same sample to the left channel
            {
                STR.WriteByte((byte)((Sample & 0xFF00) >> 8));
                STR.WriteByte((byte)(Sample & 0xFF));
            }
            NoOfSamples++;
            TimeSamples++;
            if (TimeSamples >= 44100)
            {
                _Time++;
                TimeSamples = 0;
            }
        }
        public void Stop()
        {
            if (_IsRecording & STR != null)
            {
                if (STEREO)
                {
                    NoOfSamples *= 4;
                }
                else
                {
                    NoOfSamples *= 2;
                }
                SIZE = NoOfSamples + 36;
                byte[] buff_size = new byte[4];
                byte[] buff_NoOfSammples = new byte[4];
                buff_size = BitConverter.GetBytes(SIZE);
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(buff_size);
                buff_NoOfSammples = BitConverter.GetBytes(NoOfSamples);
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(buff_NoOfSammples);
                _IsRecording = false;
                STR.Position = 4;
                STR.Write(buff_size, 0, 4);
                STR.Position = 40;
                STR.Write(buff_NoOfSammples, 0, 4);
                STR.Close();
            }
        }
        public int Time
        {
            get { return _Time; }
        }
        public bool IsRecording
        {
            get { return _IsRecording; }
        }
    }
}