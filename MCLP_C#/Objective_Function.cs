using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLP2023
{
    internal class Objective_Function
    {

        /// <summary>
        /// Calculate the objective function using original distance matrix
        /// </summary>
        /// <param name="distanceMartix"></param>
        /// <param name="population"></param>
        /// <param name="StationList"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static double CalObj(double[,] distanceMartix, List<double> population, List<int> StationList, double radius)// 计算给定坐标下的覆盖量
        {
            double obj = 0;
            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < StationList.Count; j++)
                {
                    if (ifCover(distanceMartix, radius, i, StationList[j]) == true)
                    {
                        obj += population[i];
                        break;
                    }
                }
            }
            return obj;

        }



        /// <summary>
        /// calculate the objective function using bool covering maxtrix
        /// </summary>
        /// <param name="coverMartix"></param>
        /// <param name="population"></param>
        /// <param name="StationList"></param>
        /// <returns></returns>
        public static double CalObj(bool[,] coverMartix, List<double> population, List<int> StationList)// 
        {
            double obj = 0;
            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < StationList.Count; j++)
                {
                    if (coverMartix[i, StationList[j]] == true)
                    {
                        obj += population[i];
                        break;
                    }
                }
            }
            return obj;

        }



        /// <summary>
        /// Determine if two points overlap each other using bool covering matrix
        /// </summary>
        /// <param name="coodinate"></param>
        /// <param name="radius"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static bool ifCover(List<(double, double)> coodinate, double radius, int i, int j) //
        {
            bool cover = false;
            if ((coodinate[i].Item1 - coodinate[j].Item1) * (coodinate[i].Item1 - coodinate[j].Item1) + (coodinate[i].Item2 - coodinate[j].Item2) * (coodinate[i].Item2 - coodinate[j].Item2) <= radius * radius)
            {
                cover = true;
            }
            //if(i==j) cover = false;
            return cover;
        }

        /// <summary>
        /// Determine if two points overlap each other using distance matrix
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="radius"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static bool ifCover(double[,] distance, double radius, int i, int j) //
        {
            bool cover = false;
            if (distance[i, j] <= radius)
            {
                cover = true;
            }
            //if(i==j) cover = false;


            return cover;
        }

    }
}
