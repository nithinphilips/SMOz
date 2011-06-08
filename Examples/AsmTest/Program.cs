using System;
using System.Collections.Generic;
using System.Text;
using O2S.Components.PDFRender4NET;

namespace AsmTest
{
    [NotifyPropertyChanged]
    class Program
    {
        static void Main( string[] args )
        {
            using (var pdf = PDFFile.Open( "Test.pdf" ))
            {
                //
            }
        }
    }
}
