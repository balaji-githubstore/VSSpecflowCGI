using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace OpenEmrApplication
{
    class SampleTest
    {
        [Test]
        public void sample()
        {
            
            Assert.Warn("hello");
            Assert.Multiple(() =>
            {
                Assert.Fail("hi 1");
                Assert.Fail("hi 2");
            }
            );
            Console.WriteLine("kkk");

          
        }
    }
}
