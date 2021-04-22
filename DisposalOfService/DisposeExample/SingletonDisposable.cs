using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposalOfService.DisposeExample
{
    public class SingletonDisposable:IDisposable
    {
        public void Dispose() => Console.WriteLine($"{typeof(SingletonDisposable)} is disposed.");
    }
}
