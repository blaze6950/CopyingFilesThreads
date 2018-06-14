using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyingFilesThreads.Model
{
    public class CopyPathes
    {
        private String _fromPath;
        private String _toPath;

        public CopyPathes()
        {
        }

        public CopyPathes(string fromPath, string toPath)
        {
            FromPath = fromPath;
            ToPath = toPath;
        }

        public string FromPath
        {
            get { return _fromPath; }
            set { _fromPath = value; }
        }

        public string ToPath
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder(_toPath.Length + Path.GetFileName(_fromPath).Length);
                stringBuilder.Append(_toPath);
                stringBuilder.Append("\\" + Path.GetFileName(_fromPath));
                return stringBuilder.ToString();
            }
            set
            {
                _toPath = value;
            }
        }
    }
}
