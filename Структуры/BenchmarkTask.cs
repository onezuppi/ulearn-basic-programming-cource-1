using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        private Stopwatch stopWatch = new Stopwatch();

        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            task.Run();
            stopWatch.Reset();
            stopWatch.Start();
            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            stopWatch.Stop();
            return (double) stopWatch.ElapsedMilliseconds / repetitionCount;
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        private int repetitionCount = 13000;
        private Benchmark benchmark = new Benchmark();
        private int charCount = 10000;

        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var stringBuilderTest = new StringBuilderTest(charCount);
            var stringTest = new StringTest(charCount);
            Assert.Less(benchmark.MeasureDurationInMs(stringTest, repetitionCount),
                benchmark.MeasureDurationInMs(stringBuilderTest, repetitionCount));
        }
    }

    public class StringBuilderTest : ITask
    {
		private int charCount;

        public StringBuilderTest(int charCount)
        {
			this.charCount = charCount;
        }

        public void Run()
        {
            var str = new StringBuilder();
            for (var i = 0; i < charCount; i++)
                str.Append('a');
            str.ToString();
        }
    }

    public class StringTest : ITask
    {
		private int charCount;
	
        public StringTest(int charCount)
        {
			this.charCount = charCount;
        }

        public void Run()
        {
            new string('a', charCount);
        }
    }
}