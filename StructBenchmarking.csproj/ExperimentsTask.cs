using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace StructBenchmarking
{
    public interface ITaskFactory
    {
        ITask CreateTaskForClass(int size);
        ITask CreateTaskForStructure(int size);
    }

    public class ArrayCreationTaskFactory : ITaskFactory
    {
        public ITask CreateTaskForClass(int size)
        {
            return new ClassArrayCreationTask(size);
        }

        public ITask CreateTaskForStructure(int size)
        {
            return new StructArrayCreationTask(size);
        }
    }

    public class MethodCallCreationTaskFactory : ITaskFactory
    {
        public ITask CreateTaskForClass(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }

        public ITask CreateTaskForStructure(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }
    }

    public class Experiments
    {
        private static ChartData BuildData(
            string title, IBenchmark benchmark, int repetitionsCount, ITaskFactory taskFactory)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var size in Constants.FieldCounts)
            {
                classesTimes.Add(
                    new ExperimentResult(size, benchmark.MeasureDurationInMs(
                        taskFactory.CreateTaskForClass(size), repetitionsCount)));
                structuresTimes.Add(
                    new ExperimentResult(size, benchmark.MeasureDurationInMs(
                        taskFactory.CreateTaskForStructure(size), repetitionsCount)));
            }

            return new ChartData
            {
                Title = title,
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            ITaskFactory factory;
            factory = new ArrayCreationTaskFactory();
            return BuildData("Create array", benchmark, repetitionsCount, factory);
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();
            ITaskFactory factory;
            factory = new MethodCallCreationTaskFactory();
            return BuildData("Call method with argument", benchmark, repetitionsCount, factory);
        }
    }
}