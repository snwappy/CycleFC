/*********************************************************************\r
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

namespace CycleMain
{
    public class ControlProfile
    {
        public string Name = "";
        public string Player1_A = "Keyboard.X";
        public string Player1_B = "Keyboard.Z";
        public string Player1_Left = "Keyboard.LeftArrow";
        public string Player1_Right = "Keyboard.RightArrow";
        public string Player1_Up = "Keyboard.UpArrow";
        public string Player1_Down = "Keyboard.DownArrow";
        public string Player1_Start = "Keyboard.V";
        public string Player1_Select = "Keyboard.C";

        public string Player2_A = "Keyboard.K";
        public string Player2_B = "Keyboard.J";
        public string Player2_Left = "Keyboard.A";
        public string Player2_Right = "Keyboard.D";
        public string Player2_Up = "Keyboard.W";
        public string Player2_Down = "Keyboard.S";
        public string Player2_Start = "Keyboard.E";
        public string Player2_Select = "Keyboard.Q";
    }
    public class ControlProfilesCollection : List<ControlProfile>//For settings
    {

    }
}
