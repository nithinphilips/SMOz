/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyright (C) 2006 Nithin Philips
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License
 *  as published by the Free Software Foundation; either version 2
 *  of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation,Inc.,59 Temple Place - Suite 330,Boston,MA 02111-1307, USA.
 *
 *  Author            :  Nithin Philips <spikiermonkey@users.sourceforge.net>
 *  Original FileName :  Writer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LibSmoz.Ini
{
    public class IniWriter
    {

        public IniWriter()
        {
            sections = new Dictionary<string, HashSet<string>>();
        }

        string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        Dictionary<string, HashSet<string>> sections;

        public void AddValue(string value, string section)
        {
            HashSet<string> sec;
            if (!sections.TryGetValue(section, out sec))
            {
                sec = new HashSet<string>();
                sections.Add(section, sec);
            }

            sec.Add(value);
        }


        public string Build()
        {
            if (string.IsNullOrEmpty(this.comment))
            {
                return Build(string.Format("Generated by {0} v{1}. {2} {3} UTC.", "LibSMOz", "1.0.0.0", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString()));
            }
            else
            {
                return Build(this.comment);
            }
        }

        private string Build(string comments)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("# " + comments + Environment.NewLine + Environment.NewLine);
            foreach (var pair in sections)
            {
                builder.Append("[" + pair.Key + "]" + Environment.NewLine);
                foreach (var value in pair.Value)
                {
                    builder.Append(value + Environment.NewLine);
                }

                builder.Append(Environment.NewLine);
            }
            return builder.ToString();
        }

        public void Save(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(stream);
                stream.Flush();
            }
        }

        public void Save(Stream stream)
        {
            string data = Build();
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
