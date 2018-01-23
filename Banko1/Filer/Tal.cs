using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banko1 {
    class Tal {

        internal void TalViserGenerator() {
            List<int> talList = new List<int>();
            Random rnd = new Random();
            int Value;

            for (int i = 0; i < 11; i++) {
                talList.Add(i);
                }


            for (int i = 0; i < 10; i++) {
                Value = rnd.Next(1, talList.Count);
                Console.WriteLine(talList[Value]);

                talList.RemoveAt(Value);
                Console.ReadLine();
                }
            }
        }
    }
