using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedingtonExam.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedingtonExam.BusinessObjects.Tests
{
    [TestClass()]
    public class ProbablilityTests
    {
        [TestMethod()]
        public void CombinedWithTest()
        {
            const double expected = 0.72;
            var actual = Probablility.CombinedWith(0.9, 0.8);
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod()]
        public void EitherOrTest()
        {
            const double expected = 0.98;
            var actual = Probablility.EitherOr(0.9, 0.8);
            Assert.AreEqual(expected, actual, 0.0001);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidInputTest1()
        {
            Probablility.CombinedWith(0.5, -1);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidInputTest2()
        {
            Probablility.EitherOr(0.5, 1.001);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ValidInputTest3()
        {
            Probablility.CombinedWith(-0.000001, 0.5);
        }
    }
}