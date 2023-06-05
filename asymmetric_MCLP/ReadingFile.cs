using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aMCLP2023
{
    internal class ReadingFile
    {
        public static (bool[,], List<double>, List<double>, int) Read(string path, int n, double r)
        {

            (var distArry, var coverArry, var population, var populationsite, int PoNodes) = ReadingFile.ReadBDS(path, n, r);
            return (coverArry, population, populationsite, PoNodes);

        }


        /// <summary>
        /// Read BDS node with Node clustering
        /// </summary>
        /// <param name="path"></param> file path 
        /// <param name="n"></param> number of nodes
        /// <param name="r"></param> value of radius
        /// <returns></returns>
        public static (double[,], bool[,], List<double>, List<double>, int) ReadBDS(string path, int n, double r)
        {
            List<(double, double)> coodinateSite = new List<(double, double)>();
            List<(double, double)> coodinatenNode = new List<(double, double)>();
            List<double> populationSite = new List<double>();
            List<double> populationNode = new List<double>();


            using (StreamReader fileread = new StreamReader(path))// 
            {
                int line = 0;

                while (fileread.EndOfStream != true)
                { //Console.Write (Convert.ToInt32(fileread.Read())*100);

                    string[] data = fileread.ReadLine().Split("\t");//
                    if (line >= 1)//by content not line number
                    {
                        if (data[0] == "F")
                        {
                            coodinateSite.Add((double.Parse(data[2]), double.Parse(data[3])));
                            populationSite.Add(0);//data.Length-1
                        }
                        else if ((data[0] == "C"))
                        {
                            coodinatenNode.Add((double.Parse(data[2]), double.Parse(data[3])));
                            populationNode.Add(Convert.ToDouble(data[4]));//data.Length-1
                        }

                    }

                    line++;
                }

                fileread.Close();

            }
            double[,] distArry = new double[coodinateSite.Count, coodinatenNode.Count];


            List<int> Nodes = new List<int>();
            for (int i = 0; i < coodinatenNode.Count; i++)
            {
                int NumOfCover = 0;
                int coverBySite = 0;
                for (int j = 0; j < coodinateSite.Count; j++)
                {
                    distArry[j, i] = disCoodinate(coodinateSite[j], coodinatenNode[i]);
                    if (distArry[j, i] <= r)
                    {
                        coverBySite = j;
                        NumOfCover++;
                    }

                }
                if (NumOfCover == 1)  // 
                    populationSite[coverBySite] += populationNode[i];//
                else if (NumOfCover > 1) // 
                    Nodes.Add(i);
            }
            //Console.WriteLine($"{coodinate[0].Item1}, {coodinate[0].Item2}, {string.Join(" ", data1.ToArray())}, {(double)coodinate[0].Item1 * Math.PI / 180} , {distArry[0, 1]} ");

            int Num_PossibleSite = populationSite.Count; int Num_nodes = Nodes.Count;
            bool[,] coverArry = new bool[Num_PossibleSite, Num_nodes];
            List<double> population = new List<double>();
            for (int i = 0; i < Num_PossibleSite; i++) // 
            {

                //for (int j = 0; j < Num_PossibleSite; j++)
                //{
                //    var distance = distArry[j, i] = disCoodinate(coodinateSite[j], coodinateSite[i]);
                //    coverArry[i, j] = distance <= r ? true : false;
                //}
                for (int j = 0; j < Num_nodes; j++)
                {
                    coverArry[i, j] = distArry[i, Nodes[j]] <= r ? true : false;
                }
            }
            //for (int j = 0; j < Num_PossibleSite; j++)
            //    population.Add(populationSite[j]);
            for (int j = 0; j < Num_nodes; j++)
                population.Add(populationNode[Nodes[j]]);


            // Console.WriteLine($"Read containers successfully, totoaly we have {ListCon.Count - 1} containers \n");
            return (distArry, coverArry, population, populationSite, Num_PossibleSite);

        }

        public static double disCoodinate((double, double) cood1, (double, double) cood2)
        {
            return Math.Sqrt(Math.Pow(cood1.Item1 - cood2.Item1, 2) + Math.Pow(cood1.Item2 - cood2.Item2, 2));
        }


    }
}
