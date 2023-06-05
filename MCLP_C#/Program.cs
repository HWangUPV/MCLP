///
/// Main Program 
/// Created on 5th/06/2023
/// Author: Hongtao Wang
///
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MCLP2023
{
    internal class Program
    {
        /// <summary>
        /// This is the main method of this project
        /// Arguments are required.
        /// Arguements input sample: CE Instances SJC708.txt Results 6 800 2000 0.05 20 0.8 50 0
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false); // The clusters use es-ES
            string InstancePath = $"./{args[1]}/{args[2]}";
            int numNode = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(args[2], @"[^0-9]+", ""));
            int NumSite = Convert.ToInt32(args[4]);  //args[2].Contains("SJC") == true ? Convert.ToInt32(args[4]):25;
            double radius = Convert.ToDouble(args[5]);// args[2].Contains("SJC") == true ? Convert.ToInt32(args[5]):3.75;

            int PopSize = Convert.ToInt32(args[6]);
            int EliteSize = (int)(Convert.ToDouble(args[7]) * PopSize);
            int LSSize = Convert.ToInt32(args[8]);
            double alpha = (double)Convert.ToDouble(args[9]);
            int Cmax = Convert.ToInt32(args[10]);
            Random rand = new Random(Convert.ToInt32(args[11]));


            //////////////////////////////Read Data//////////////////
            (var distMatrix, var coverMatrix, var population) = ReadingFile.Read(InstancePath, numNode, radius);

            //////////////////////////////Run CE method//////////////////
            CEmethod.CE_method(rand, coverMatrix, population, radius, NumSite, PopSize, LSSize, EliteSize, alpha, Cmax);
          
        }


     


    }


}