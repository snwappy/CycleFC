/*********************************************************************\r
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
using System.Collections.Generic;
namespace CycleCore.Nes
{
    public static class MapperManager
    {
        private static Type[] mappers = new Type[256];

        public static void Cache()
        {
            var assembly = typeof(MapperManager).Assembly;

            for (int i = 0; i < 256; i++)
            {
                mappers[i] = assembly.GetType("CycleCore.Nes.Mapper" + i.ToString("D2"), false, false);
            }
        }
        public static Type Fetch(byte number)
        {
            return mappers[number];
        }
        public static bool Valid(byte number)
        {
            return mappers[number] != null;
        }
        public static string[] SupportedMappers
        {
            get 
            {
                List<string> list = new List<string>();
                for (int i = 0; i < mappers.Length; i++)
                {
                    if (mappers[i] != null)
                    {
                        Mapper mapper = Activator.CreateInstance(mappers[i],new NesSystem (null)) as Mapper;
                        list.Add("Mapper # " + i.ToString() + " [ " + mapper.Name + " ]");
                    }
                }
                return list.ToArray();
            }
        }
    }
}