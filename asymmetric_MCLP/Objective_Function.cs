using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aMCLP2023
{
    internal class Objective_Function
    {

        /// <summary>
        /// Calculte the objective function
        /// </summary>
        /// <param name="coverMartix"></param>
        /// <param name="population"></param>
        /// <param name="populationSite"></param>
        /// <param name="StationList"></param>
        /// <returns></returns>
        public static double CalObj(bool[,] coverMartix, List<double> population, List<double> populationSite, List<int> StationList)// 
        {
            double obj = 0;
            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < StationList.Count; j++)
                {
                    if (coverMartix[StationList[j], i] == true)
                    {
                        obj += population[i];
                        break;
                    }
                }
            }
            for (int j = 0; j < StationList.Count; j++)
                obj += populationSite[StationList[j]];


            return obj;

        }


        public static bool ifCover(List<(double, double)> coodinate, double radius, int i, int j) //if cover
        {
            bool cover = false;
            if ((coodinate[i].Item1 - coodinate[j].Item1) * (coodinate[i].Item1 - coodinate[j].Item1) + (coodinate[i].Item2 - coodinate[j].Item2) * (coodinate[i].Item2 - coodinate[j].Item2) <= radius * radius)
            {
                cover = true;
            }
            //if(i==j) cover = false;
            return cover;
        }


        public static bool ifCover(double[,] distance, double radius, int i, int j) //if cover
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
