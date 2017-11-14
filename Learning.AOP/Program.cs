using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.AOP
{
    public class DescribitionAttribute : Attribute
    {
        public string Describition { get; set; }
        public DescribitionAttribute(string describition)
        {
            Describition = describition;
        }
    }

    public enum Test
    {
        [Describition("nihao")]
        你好
    }

    class Program
	{

		static void Main(string[] args)
		{
            //var type = typeof(Test);
            //var name = type.GetEnumName((int)Test.你好);
            //var attr = type.GetField(name).GetCustomAttributes(typeof(DescribitionAttribute), false)?.FirstOrDefault() as DescribitionAttribute;

            //Console.WriteLine(attr.Describition);

            var list = new List<int>();

            if (list?.Any() == false)
            {
                Console.WriteLine("1");
            }
            list = null;
            if (list?.Any() == false)
            {
                Console.WriteLine("3");
            }

            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(i);
            //}
            //Parallel.ForEach(list, async l => await Do2(l));

            Console.ReadKey();
        }

        public static async Task Do2(int num)
        {
            var result= await Do3(num);
            Console.WriteLine(result);
        }

        public static async Task<int> Do3(int num)
        {
            await Task.Delay(1000);
            return num*10;
        }

    }


}
