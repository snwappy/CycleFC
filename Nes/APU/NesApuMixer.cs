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
    public class NesApuMixer
    {
        private const float NLN_SQR_0 = 95.52f;
        private const float NLN_SQR_1 = 8128.00f;

        private const float NLN_TND_0 = 163.67f;
        private const float NLN_TND_1 = 24329.00f;

        public static float MixSamples(float sqrSample, float tndSample)
        {
            return
                (NLN_SQR_0 / (NLN_SQR_1 / sqrSample + 100)) +
                (NLN_TND_0 / (NLN_TND_1 / tndSample + 100));
        }
    }
}