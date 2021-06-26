using System.Collections.Generic;

namespace StructBenchmarking
{
    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount)
        {
            var (classesTimes, structuresTimes) = GetTimes(benchmark, new ArrayCreation(), repetitionsCount);
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount)
        {
            var (classesTimes, structuresTimes) = GetTimes(benchmark, new MethodCall(), repetitionsCount);
            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        private static (List<ExperimentResult>, List<ExperimentResult> ) GetTimes(IBenchmark benchmark,
            ITaskFactory taskFactory,
            int repetitionsCount)
        {
            var structuresTimes = new List<ExperimentResult>();
            var classesTimes = new List<ExperimentResult>();
            foreach (var fieldsCount in Constants.FieldCounts)
            {
                var (structEssence, classEssence) = taskFactory.Create(fieldsCount);
                structuresTimes.Add(new ExperimentResult(fieldsCount,
                    benchmark.MeasureDurationInMs(structEssence, repetitionsCount)));
                classesTimes.Add(new ExperimentResult(fieldsCount,
                    benchmark.MeasureDurationInMs(classEssence, repetitionsCount)));
            }

            return (classesTimes, structuresTimes);
        }
    }

    public interface ITaskFactory
    {
        (ITask, ITask) Create(int fieldsCount);
    }

    public class ArrayCreation : ITaskFactory
    {
        public (ITask, ITask) Create(int fieldsCount)
        {
            return (new StructArrayCreationTask(fieldsCount), new ClassArrayCreationTask(fieldsCount));
        }
    }

    public class MethodCall : ITaskFactory
    {
        public (ITask, ITask) Create(int fieldsCount)
        {
            return (new MethodCallWithStructArgumentTask(fieldsCount),
                new MethodCallWithClassArgumentTask(fieldsCount));
        }
    }
}