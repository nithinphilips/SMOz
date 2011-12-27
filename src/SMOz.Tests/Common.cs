using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SMOz.Tests
{
    static class Common
    {
        public static string GetTestDataFullPath(string fileName)
        {
            String codebase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            codebase = codebase.Replace(@"file:///", "");
 
            String strPath = System.IO.Path.GetDirectoryName(codebase);
#if RELEASE
            return Path.Combine(strPath, "..", "..", "test-data", fileName);
#else
            return Path.Combine(strPath, fileName);
#endif
        }
    }
}
