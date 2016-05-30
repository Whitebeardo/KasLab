using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KasLab.Tasks;

namespace KasLab.TestConsole
{
    class Program
    {
        private static int _id;
        private static readonly object _lock = new object();

        public static int GetId
        {
            get
            {
                lock (_lock)
                {
                    return _id++;
                }
            }
        }


        static void Main(string[] args)
        {
            var queue = new SyncQueue<string>();

            Task.WaitAll(StartPopTasks(queue, 10).Union(StartPushTasks(queue, 10)).ToArray());

            Console.WriteLine("\nPress any key\n");
            Console.ReadKey();

            AddendsFinder.FindAddends(20).ForEach(Console.WriteLine);

            Console.WriteLine("\nPress any key\n");
            Console.ReadKey();

            AddendsFinder.FindAddends(20, 1, 19, 7, 4).ForEach(Console.WriteLine);

            Console.WriteLine("\nPress any key\n");
            Console.ReadKey();
        }

        private static IEnumerable<Task> StartPopTasks<T>(SyncQueue<T> queue, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var task = new Task(() =>
                {
                    Console.WriteLine("Start new pop task");
                    Console.WriteLine("Output from task {0} poped", queue.Pop());
                });

                task.Start();

                yield return task;
            }
        }

        private static IEnumerable<Task> StartPushTasks(SyncQueue<string> queue, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var id = GetId;
                var task = new Task(() =>
                {
                    Console.WriteLine("Start push task {0}", id);
                    queue.Push(id.ToString());
                });
                task.Start();

                yield return task;
            }
        }
    }
}
