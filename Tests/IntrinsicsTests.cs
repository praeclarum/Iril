using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IntrinsicsTests : TestsBase
    {
        [Test]
        public void DeclareEmptyVAArgs ()
        {
            var ir = CToIR (@"
void *myprint(const char *format, ...) {
}");
            //Console.WriteLine (ir);
        }
    }
}
