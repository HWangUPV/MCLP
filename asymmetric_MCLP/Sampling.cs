using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aMCLP2023
{
    internal class Sampling
    {
        /// <summary>
        /// Parato sampling without replacement
        /// </summary>
        /// <param name="ProbList"></param> ProbList
        /// <param name="r"></param> value of radius
        /// <param name="K"></param> number of selected sites
        /// <returns></returns>
        public static List<int> PPS_SamplingForK(List<double> ProbList, Random r, int K)// without replacement for K numbers
        {
            int N = ProbList.Count;
            List<int> selectedElement = new List<int>();
            double[] OldsRatio = new double[N];
            for (int i = 0; i < N; i++)
            {
                double RandDouble = r.NextDouble(); // radomness is required
                OldsRatio[i] = (double)(Math.Exp(Math.Log(RandDouble) - Math.Log(1 - RandDouble) + Math.Log(1 - ProbList[i]) - Math.Log(ProbList[i])));
            }

            var OddsRatioOrder = OldsRatio.OrderBy(c => c).ToArray();
            for (int i = 0; selectedElement.Count < K; i++)
            {
                var temp = Array.IndexOf(OldsRatio, OddsRatioOrder[i]);
                OldsRatio[temp] = double.MaxValue;
                selectedElement.Add(temp);
            }
            return selectedElement;
        }








        /// <summary>
        /// Update the probability based on the selected elite popualation
        /// </summary>
        /// <param name="rand"></param> randomness
        /// <param name="ElitePop"></param> selected elite population
        /// <param name="prob_Ori"></param> previous probability
        /// <param name="alpha"></param> parameter alpha
        /// <param name="K"></param> number of selected sites
        /// <param name="itkeep"></param> How many iterations the best solution kept
        /// <param name="Cmax"></param> parameter Cmax
        /// <returns></returns>
        public static List<double> UpdateProb(Random rand, List<(List<int> loc, double obj)> SlutionList, List<double> prob_Ori, double alpha, int K, int selectedNum, int itkeep, int Cmax)// update probability
        {
            double[] Prob = new double[prob_Ori.Count];
            for (int i = 0; i < selectedNum; i++)
            {
                for (int j = 0; j < K; j++)
                {
                    Prob[SlutionList[i].loc[j]]++;
                }
            }

            for (int i = 0; i < prob_Ori.Count; i++)
                Prob[i] = (double)Prob[i] / selectedNum;

            return InitializationAlpha(rand, Prob, prob_Ori, alpha, itkeep, Cmax).ToList();

        }



        /// <summary>
        /// Initialize the probability
        /// </summary>
        /// <param name="Prob"></param>
        /// <returns></returns>
        public static double[] Initialization(double[] Prob)// 
        {
            double sum = Prob.Sum();
            for (int i = 0; i < Prob.Length; i++)
            {
                Prob[i] = Prob[i] / sum;
            }
            return Prob;
        }





        /// <summary>
        /// Generate the intial probability
        /// </summary>
        /// <param name="popution"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static List<double> GenIniProb(List<double> popution, int k)
        {
            List<double> prob = new List<double>();
            var sum = popution.Sum();
            for (int i = 0; i < popution.Count; i++)
            {
                //prob.Add((double)k / popution.Count);
                prob.Add(Math.Min(k * popution[i] / sum * k, 1));
                //prob.Add((double)50 / popution.Count);
            }

            return prob;   //Initialization(prob.ToArray()).ToList();
        }














        public static double[] InitializationAlpha(Random rand, double[] Prob, List<double> prob_Ori, double alpha, int itkeep, int Cmax)// initiallize with alpha
        {
            double sum = Prob.Sum();


            for (int i = 0; i < Prob.Length; i++)
            {
                Prob[i] = alpha * Prob[i] + (1 - alpha) * prob_Ori[i];// prob_Ori[i];
            }
            if (itkeep > Cmax) // if (Math.Log10(Prob.Min()) < repNum*(Math.Log10(alpha*10)-1)- Math.Log10(Prob.Length)) //  Prob.Min() < 10e-8)
            {
                var r = rand.Next(Prob.Length); var temp = Prob[r];
                var r2 = rand.Next(Prob.Length);
                Prob[r] = Prob[r2];
                Prob[r2] = temp;
            }
            //Prob[rand.Next(Prob.Length)] = 1;

            return Prob;
        }








        public static List<double> GenIniProb(List<double> popution, int k, int NumPoSite)
        {
            List<double> prob = new List<double>();
            var sum = popution.Sum();
            for (int i = 0; i < NumPoSite; i++)
            {
                prob.Add((double)k / NumPoSite);
                //prob.Add(popution[i] / sum*k);
                //prob.Add((double)50 / popution.Count);
            }

            return prob;   //Initialization(prob.ToArray()).ToList();
        }






    }
}
