using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteveHemond.MusicSheetViewer.Data
{
    public class MenuItem
    {
        public string Name { get; set; }

        public string Icon { get; set; }

        public Type Target { get; set; }
    }
}