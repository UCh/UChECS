namespace uchlab.benchmark {
    using System.Collections.Generic;
    using UnityEngine;

    public class TestScript : MonoBehaviour {
        private const int NumIterations = 100000;

        private class Counter {
            public int Value;
        }

        private string report;
        private int delegateSum;

        void Start() {
            report = "Size,Array For,Array While,Array Foreach,List For,List While,List Foreach\n";
            var sizes = new []{ 10, 100, 1000 };
            foreach (var size in sizes) {
                report += Test(size);
            }
        }

        private string Test(int size) {
            var stopwatch = new System.Diagnostics.Stopwatch();
            var sum = 0;
            var array = new Counter[size];
            for (int i = 0; i < size; ++i) {
                array[i] = new Counter() {
                    Value = i
                };
            }

            stopwatch.Reset();
            stopwatch.Start();
            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                for (int i = 0, len = array.Length; i < len; ++i) {
                    sum += array[i].Value;
                }
            }
            var arrayForTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                var i = 0;
                var len = array.Length;
                while (i < len) {
                    sum += array[i].Value;
                    ++i;
                }
            }
            var arrayWhileTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                foreach (var cur in array) {
                    sum += cur.Value;
                }
            }
            var arrayForeachTime = stopwatch.ElapsedMilliseconds;

            var list = new List<Counter>(size);
            list.AddRange(array);

            stopwatch.Reset();
            stopwatch.Start();
            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                for (int i = 0, len = list.Count; i < len; ++i) {
                    sum += list[i].Value;
                }
            }
            var listForTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                var i = 0;
                var len = list.Count;
                while (i < len) {
                    sum += list[i].Value;
                    ++i;
                }
            }
            var listWhileTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();
            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                foreach (var cur in list) {
                    sum += cur.Value;
                }
            }
            var listForeachTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();

            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                list.ForEach((c) => sum += c.Value);
            }

            var listForeachMethodTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();


            for (var iteration = 0; iteration < NumIterations; ++iteration) {
                list.ForEach(ForEachDelegate);
            }

            var listForeachMethodWithDelegateTime = stopwatch.ElapsedMilliseconds;

            return size + ","
            + arrayForTime + ","
            + arrayWhileTime + ","
            + arrayForeachTime + ","
            + listForTime + ","
            + listWhileTime + ","
            + listForeachTime + ","
            + listForeachMethodTime + ","
            + listForeachMethodWithDelegateTime + "\n";
        }

        void ForEachDelegate(Counter c){
            delegateSum += c.Value;
        }



        void OnGUI() {
            GUI.TextArea(new Rect(0, 0, Screen.width, Screen.height), report);
        }
    }
}