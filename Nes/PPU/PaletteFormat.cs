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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CycleCore.Nes
{
    public class PaletteFormat
    {
        float saturation = 1.977f;
        float hue_tweak = 0.187f;
        float contrast = 0.878f;
        float brightness = 1.149f;
        float gamma = 1.697f;

        bool _UseInternalPalette = true;
        string _ExternalPalettePath = "";
        UseInternalPaletteMode _UseInternalPaletteMode = UseInternalPaletteMode.Auto;
        /// <summary>
        /// Get or set a value indecate wether to use internal palette
        /// </summary>
        public bool UseInternalPalette
        { get { return _UseInternalPalette; } set { _UseInternalPalette = value; } }
        /// <summary>
        /// Use Internal Palette Mode
        /// </summary>
        public UseInternalPaletteMode UseInternalPaletteMode
        { get { return _UseInternalPaletteMode; } set { _UseInternalPaletteMode = value; } }
        /// <summary>
        /// External Palette Path
        /// </summary>
        public string ExternalPalettePath
        { get { return _ExternalPalettePath; } set { _ExternalPalettePath = value; } }

        /// <summary>
        /// Get or set the Saturation value, this value apply only on NTSC INTERNAL palette
        /// </summary>
        public float Saturation
        { get { return saturation; } set { saturation = value; } }
        /// <summary>
        /// Get or set the Hue value, this value apply only on NTSC INTERNAL palette
        /// </summary>
        public float Hue
        { get { return hue_tweak; } set { hue_tweak = value; } }
        /// <summary>
        /// Get or set the Contrast value, this value apply only on NTSC INTERNAL palette
        /// </summary>
        public float Contrast
        { get { return contrast; } set { contrast = value; } }
        /// <summary>
        /// Get or set the Brightness value, this value apply only on NTSC INTERNAL palette
        /// </summary>
        public float Brightness
        { get { return brightness; } set { brightness = value; } }
        /// <summary>
        /// Get or set the Gamma value, this value apply only on NTSC INTERNAL palette
        /// </summary>
        public float Gamma
        { get { return gamma; } set { gamma = value; } }
    }
    public enum UseInternalPaletteMode
    { Auto, PAL, NTSC }
}