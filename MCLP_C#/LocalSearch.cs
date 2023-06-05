using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MCLP2023;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace MCLP2023
{
    internal class LocalSearch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="coverMatrix"></param> cover Matrix
        /// <param name="population"></param> population
        /// <param name="selectedSite"></param> selected Site
        /// <param name="originalObj"></param> original Obj
        /// <returns></returns>
        public static (List<int>, double) Localsearch(Random rand, bool[,] coverMatrix, List<double> population, List<int> selectedSite, double originalObj) // 测试比9 好一些 但是
        {  
            Stopwatch watch = new Stopwatch();
            watch.Start();
            bool loop = true;
            while (loop == true)
            {
                loop = false;
                for(int k = 0; k < selectedSite.Count; k++)
                {

                    var uncoverNodes = new List<int>();
                    for (int i = 0; i < population.Count; i++) // 计算在移一个点后 还剩未覆盖的点集
                    {
                        uncoverNodes.Add(i);
                        for (int j = 0; j < selectedSite.Count; j++)
                            if (coverMatrix[i, selectedSite[j]] == true && j != k) // 如股是被除去 第k个点后的 剩余的点覆盖到
                            {
                                uncoverNodes.Remove(i);
                                break;
                            }
                    }



                    double max = 0; int selectNode = 0;

                    foreach (int i in uncoverNodes) // 在全部 未覆盖中 挑选最大覆盖
                    {
                        double coverbyI = 0;
                        foreach (int j in uncoverNodes) // 计算i 覆盖多少未覆盖的
                            if (coverMatrix[i, j] == true)
                                coverbyI += population[j];
                        if (coverbyI > max)
                        {
                            max = coverbyI;
                            selectNode = i;

                        }

                    }
                    List<int> NewselectedSite = new List<int>(selectedSite);
                    NewselectedSite[k] = selectNode;
                    double NewObj = Objective_Function.CalObj(coverMatrix, population, NewselectedSite);


                    if (NewObj > originalObj)
                    {
                        selectedSite[k] = selectNode;
                        loop = true;
                        originalObj = NewObj;
                    }

                }
                var t1 = watch.ElapsedMilliseconds;
                watch.Reset(); watch.Restart();

            }

            return (selectedSite, originalObj);


        }

       



      
    }

}
