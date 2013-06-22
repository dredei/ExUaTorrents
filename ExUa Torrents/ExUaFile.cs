using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExUa_Torrents
{
    public class ExUaFile
    {
        public string name = string.Empty;
        public string url = string.Empty;
        public string torrentUrl = string.Empty;
        public long size = 0;
        public long fileId = -1;
        public long arrIndex = -1;
        public bool check = false;
    }
}
