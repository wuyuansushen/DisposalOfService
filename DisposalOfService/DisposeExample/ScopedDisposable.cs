using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposalOfService.DisposeExample
{
    public class ScopedDisposable:IDisposable
    {
        public void Dispose() => Console.WriteLine($"{typeof(ScopedDisposable)} is disposed.");
    }
}
