using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using aMCLP2023;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace aMCLP2023
{
    internal class LocalSearch
    {
       /// <summary>
       ///  Locl Search
       /// </summary>
       /// <param name="rand"></param>
       /// <param name="coverMatrix"></param>
       /// <param name="population"></param>
       /// <param name="selectedSite"></param>
       /// <param name="originalObj"></param>
       /// <param name="NumPoSite"></param>
       /// <returns></returns>
        public static (List<int>, double) Localsearch(Random rand, bool[,] coverMatrix, List<double> population, List<double> populationSite, List<int> selectedSite, double originalObj, int NumPoSite) // 测试比9 好一些 但是
        {
            bool loop = true;
            while (loop == true)
            {
                loop = false;
                for (int k = 0; k < selectedSite.Count; k++)
                {

                    var uncoverNodes = Enumerable.Range(0, population.Count).ToList(); //
                    for (int i = 0; i < population.Count; i++) // 计算在移一个点后 还剩余全部的 尚未被覆盖的点
                    {
                        for (int j = 0; j < selectedSite.Count; j++)
                            if (coverMatrix[selectedSite[j], i] == true && j != k) // 如股是被除去 第k个点后的 剩余的点覆盖到
                            {
                                uncoverNodes.Remove(i);
                                break;
                            }

                    }


                    double max = 0; int selectNode = 0;
                    var a = Enumerable.Range(0, NumPoSite).ToList();
                    var b = a.Except(selectedSite).ToList();
                    int ini = 0;
                    for (int i = ini; i < selectedSite[k]; i++) // 在全部 备选点 挑选最大覆盖
                    {
                        double coverbyI = 0;
                        foreach (int j in uncoverNodes) // 计算i 覆盖多少未覆盖的 的所有node
                            if (coverMatrix[b[i], j] == true)
                                coverbyI += population[j];
                        if (coverbyI > max)
                        {
                            max = coverbyI;
                            selectNode = b[i];

                        }

                    }

                    List<int> NewselectedSite = new List<int>(selectedSite);
                    NewselectedSite[k] = selectNode;
                    double NewObj = Objective_Function.CalObj(coverMatrix, population, populationSite, NewselectedSite);


                    if (NewObj > originalObj)
                    {
                        selectedSite[k] = selectNode;
                        loop = true;
                        originalObj = NewObj;
                        ini = NewselectedSite[k];
                    }

                }

            }

            return (selectedSite, originalObj);


        }





    }

}
