using MCLP2023;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLP2023
{
    internal class CEmethod
    {

        public static (List<int>, double) CE_method(Random rand,  bool[,] coverMatrix, List<double> population, double r, int NumSite, int PopSize, int LSSize, int EliteSize, double alpha, int Cmax)
        {

            //////////////////////////////1 Runs//////////////////
            List<string> BestSolution = new List<string>();

            Stopwatch Totaltime = new Stopwatch();
            Totaltime.Start();
            double Timenow = 0;


            List<double> prob = new List<double>(Sampling.GenIniProb(population, NumSite));
            List<int> BestSolu = new List<int>();
            double BestObj = 0;

            int Iter = 0; int IterKeep = 0;

            //MatrixComputing.OutputNSolution(PopSize, prob, population, rand, NumSite, coverMatrix);
            List<(List<int> loc, double obj)> SlutionList = new List<(List<int> loc, double obj)>(PopSize);
            while (Totaltime.ElapsedMilliseconds < 600000 && IterKeep <= 50) //Totaltime.ElapsedMilliseconds < numNode * NumSite * CpuPara      && IterKeep <= 80   Totaltime.ElapsedMilliseconds < numNode * NumSite * 2
            {
                Stopwatch Onetime = new Stopwatch();
                Onetime.Start();
                // build solution population
                for (int i = 0; i < PopSize; i++)
                {
                    List<int> oneSol = Sampling.PPS_SamplingForK(prob, rand, NumSite);// Sample one solution
                    double oneSolObj = Objective_Function.CalObj(coverMatrix, population, oneSol);
                    if (Iter == 0)
                        SlutionList.Add((oneSol, oneSolObj));
                    else
                        SlutionList[i] = (oneSol, oneSolObj);
                };


                //SlutionList = MatrixComputing.OutputNSolution(PopSize, prob, population, rand, NumSite, CoverMatrix01); // ，
                SlutionList.Sort((y, x) => x.obj.CompareTo(y.obj));
                //SlutionList.Distinct();


                ///////////////////////////////// Local Search  ///////////////////////////////////
                int nLS = 0;
                for (int i = 0; i < PopSize && nLS < LSSize; i++)
                    if (SlutionList[i].obj != BestObj)
                    {
                        SlutionList[i] = LocalSearch.Localsearch(rand, coverMatrix, population, SlutionList[i].loc, SlutionList[i].obj); // LocalSearch.SwapLocalSearch(rand, coverMatrix, population, SlutionList[i].loc, thisSlut.obj); // 
                        nLS++;
                    }
                SlutionList.Sort((y, x) => x.obj.CompareTo(y.obj)); // 




                ///////////////////////////////// Update Probability ///////////////////////////////////
                var ElitePop = SlutionList.Take(Math.Min(EliteSize, SlutionList.Count)).ToList();
                prob = Sampling.UpdateProb(rand, ElitePop, prob, alpha, NumSite, IterKeep, Cmax);// update the probality based on the select site population


                if (SlutionList[0].obj > BestObj)
                {
                    (BestSolu, BestObj) = SlutionList[0];
                    IterKeep = 0;
                    Timenow = Totaltime.ElapsedMilliseconds;
                }
                Console.WriteLine($"Iter{Iter} -{IterKeep} -- best CoverRate is {BestObj / population.Sum() * 100} -- current CoverRate is {SlutionList[0].obj / population.Sum()} -- Obj:{BestObj} ----time {Totaltime.ElapsedMilliseconds / 1000}s");

                Iter++; IterKeep++;
                if (BestObj / population.Sum() == 1)
                    break;

            }


            //Console.WriteLine(SumPrint);


            //WriteToTxtForSrting(BestSolution.ToArray(), $"./Results/Solution_{NumSite}_{radius}_{args[2]}");

            return (BestSolu, BestObj);
        }

    }
}
