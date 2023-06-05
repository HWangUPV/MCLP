using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aMCLP2023
{
    internal class Global_Function
    {
        public static double GetMedian(double[] arrSource)
        {
            // Check if the array has values        
            if (arrSource == null || arrSource.Length == 0)
                throw new ArgumentException("Array is empty.");

            // Sort the array
            double[] arrSorted = (double[])arrSource.Clone();
            Array.Sort(arrSorted);

            // Calculate the median
            int size = arrSorted.Length;
            int mid = size / 2;

            if (size % 2 != 0)
                return arrSorted[mid];

            dynamic value1 = arrSorted[mid];
            dynamic value2 = arrSorted[mid - 1];
            return (value1 + value2) / 2;
        }


        public static void WriteToTxtForSrting(string[] str, string path)
        {
            //if (!File.Exists(path))//判断文件是否存在，注释则每次都覆盖原文件
            {
                var fsc = new FileStream(path, FileMode.Create, FileAccess.Write);//若文件不存在，创建TXT文件
                fsc.Close();
            }
            System.IO.File.WriteAllLines(path, str);
        }
    }
}
