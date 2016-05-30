using System;
using System.Collections.Generic;
using System.Linq;

namespace KasLab.Tasks
{
    public static class AddendsFinder
    {
        public static List<string> FindAddends(long sum, params long[] numbers)
        {
            if (!numbers.Any())
                return _GetAddends(sum, _GenerateNumbersCollection(sum)).ToList();

            return _GetAddends(sum, numbers.AsEnumerable()).ToList();
        }

        private static IEnumerable<string> _GetAddends(long sum, IEnumerable<long> collection)
        {
            var pairs = new List<Tuple<long, long>>();
            
            for (int i = 0; i < collection.Count(); i++)
            {
                for (int j = 0; j < collection.Count(); j++)
                {
                    var addend1 = collection.ElementAt(i);
                    var addend2 = collection.ElementAt(j);

                    if (!_IsPairsContains(addend1, addend2, pairs) && addend1 + addend2 == sum)
                        yield return
                            string.Format("{0} + {1} = {2}", _AddToPairs(addend1, addend2, pairs, ref sum));
                }
            }
        }

        private static object[] _AddToPairs(long addend1, long addend2, List<Tuple<long, long>> pairs, ref long sum)
        {
            pairs.Add(new Tuple<long, long>(addend1, addend2));
            return new object[3] {addend1, addend2, sum};
        }

        private static bool _IsPairsContains(long addend1, long addend2, List<Tuple<long, long>> pairs)
        {
            return pairs.Any(p => p.Item1 == addend2 && p.Item2 == addend1);
        }

        private static IEnumerable<long> _GenerateNumbersCollection(long count)
        {
            for (int i = 1; i < count; i++)
                yield return i;
        }
    }
}
