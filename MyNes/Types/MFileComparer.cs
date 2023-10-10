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
    public class MFileComparer : IComparer<MFile>
    {
        bool AtoZ = true;
        CompareMode sortMode = CompareMode.Name;
        public MFileComparer(bool AtoZ, CompareMode sortMode)
        {
            this.sortMode = sortMode;
            this.AtoZ = AtoZ;
        }
        public int Compare(MFile x, MFile y)
        {
            switch (sortMode)
            {
                case CompareMode.Name:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Name, y.Name);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Name, y.Name));
                case CompareMode.IsBatteryBacked:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsBatteryBacked, y.IsBatteryBacked);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsBatteryBacked, y.IsBatteryBacked));
                case CompareMode.IsTrainer:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsTrainer, y.IsTrainer);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsTrainer, y.IsTrainer));
                case CompareMode.Mapper:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Mapper, y.Mapper);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Mapper, y.Mapper));
                case CompareMode.PC10:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsPC10, y.IsPC10);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsPC10, y.IsPC10));
                case CompareMode.Size:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Size, y.Size);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Size, y.Size));
                case CompareMode.VSUnisystem:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsVSUnisystem, y.IsVSUnisystem);
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.IsVSUnisystem, y.IsVSUnisystem));
                case CompareMode.Rating:
                    if (AtoZ)
                        return (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Rating.ToString(), y.Rating.ToString());
                    else
                        return (-1 * (StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentCulture, false)).Compare(x.Rating.ToString(), y.Rating.ToString()));
            }
            return 0;
        }
    }
    public enum CompareMode
    {
        None, Name, Size, Mapper, IsBatteryBacked, IsTrainer, PC10, VSUnisystem, Rating
    }
}
