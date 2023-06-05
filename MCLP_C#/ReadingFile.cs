using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLP2023
{
    internal class ReadingFile
    {
        public static (double[,], bool[,], List<double>) Read(string path, int n, double r)
        {
            List<double> population = new List<double>();
            double[,] distArry = new double[n, n];
            bool[,] coverArry = new bool[n, n];
            if (path.Contains("SJC") == true)
                (distArry, coverArry, population) = ReadingFile.ReadCood(path, n, r, true);
            else if (path.Contains("ZDS") == true || path.Contains("PCB") == true)
                (distArry, coverArry, population) = ReadingFile.ReadCood(path, n, r, true); // ReadingFile.GenZDS( n, r);
            else if (path.Contains("Bronx") == true || path.Contains("Kings") == true || path.Contains("Manhattan") == true || path.Contains("SanFrancisco") == true)
                (distArry, coverArry, population) = ReadingFile.ReadCood(path, n, r, false);

            else if (path.Contains("B700") || path.Contains("B900") == true || path.Contains("GR150") || path.Contains("GR100") == true)
                (distArry, coverArry, population) = ReadingFile.ReadB700900(path, n, r);
            
            else if (path.Contains("BDS") == true)
                (distArry, coverArry, population) = ReadingFile.ReadBDS(path, n, r);
            else
                (distArry, coverArry, population) = ReadingFile.ReadMatrix(path, n, r);

            return (distArry, coverArry, population);

        }



        public static (double[,], bool[,], List<double>) ReadCood(string path, int n, double r, bool cood)
        {
            List<(double, double)> coodinate = new List<(double, double)>();
            List<double> population = new List<double>();
            double[,] distArry = new double[n, n];
            bool[,] coverArry = new bool[n, n];
            using (StreamReader fileread = new StreamReader(path))// 读取文件将文件数据转换为container，注意读的时候分隔符
            {
                int line = 0;

                while (fileread.EndOfStream != true)
                { //Console.Write (Convert.ToInt32(fileread.Read())*100);

                    string[] data = fileread.ReadLine().Split("\t");//数据按照空格分离
                    if (line >= 1)//by content not line number
                    {
                        coodinate.Add((double.Parse(data[0]), double.Parse(data[1])));
                        population.Add(Convert.ToDouble(data[2]));//data.Length-1
                    }

                    line++;
                }

                fileread.Close();


                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (cood == true)
                            distArry[i, j] = Math.Sqrt((double)((coodinate[i].Item1 - coodinate[j].Item1) * (coodinate[i].Item1 - coodinate[j].Item1) + (coodinate[i].Item2 - coodinate[j].Item2) * (coodinate[i].Item2 - coodinate[j].Item2)));
                        else
                        {
                            //distArry[i, j] = GetDistance(coodinate[i].Item1, coodinate[i].Item2, coodinate[j].Item1, coodinate[j].Item2);
                            var lat1 = (double)coodinate[i].Item1 * Math.PI / 180;
                            var lat2 = (double)coodinate[j].Item1 * Math.PI / 180;
                            var lng1 = (double)coodinate[i].Item2 * Math.PI / 180;
                            var lng2 = (double)coodinate[j].Item2 * Math.PI / 180;
                            var a = lat1 - lat2;
                            var b = lng1 - lng2;


                            //distArry[i, j] = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(b / 2), 2)))* 6378100;
                            distArry[i, j] = Math.Acos(Math.Cos((double)lat1) * Math.Cos((double)lat2) * Math.Cos((double)-b) + Math.Sin((double)lat1) * Math.Sin((double)lat2)) * 6378100;

                        }
                        // 
                        coverArry[i, j] = distArry[i, j] <= r ? true : false;
                        coverArry[j, i] = coverArry[i, j];

                    }
                    coverArry[i, i] = true;

                }
                //Console.WriteLine($"{coodinate[0].Item1}, {coodinate[0].Item2}, {string.Join(" ", data1.ToArray())}, {(double)coodinate[0].Item1 * Math.PI / 180} , {distArry[0, 1]} ");


            }
            // Console.WriteLine($"Read containers successfully, totoaly we have {ListCon.Count - 1} containers \n");
            return (distArry, coverArry, population);
        }

        public static (double[,], bool[,], List<double>) ReadMatrix(string path, int n, double r)
        {
            double[,] distArry = new double[n, n];
            bool[,] coverArry = new bool[n, n];
            List<double> population = new List<double>();

            using (StreamReader fileread = new StreamReader(path))// 注意读的时候分隔符
            {
                int line = 0;

                while (fileread.EndOfStream != true)
                { //Console.Write (Convert.ToInt32(fileread.Read())*100);

                    string[] data = fileread.ReadLine().Split('\t');//数据按照空格分离
                    if (line >= 1)//by content not line number
                    {
                        distArry[Convert.ToInt32(data[0]) - 1, Convert.ToInt32(data[1]) - 1] = Convert.ToDouble(data[2]);
                        distArry[Convert.ToInt32(data[1]) - 1, Convert.ToInt32(data[0]) - 1] = Convert.ToDouble(data[2]);
                        coverArry[Convert.ToInt32(data[0]) - 1, Convert.ToInt32(data[1]) - 1] = Convert.ToDouble(data[2]) <= r ? true : false;
                        coverArry[Convert.ToInt32(data[1]) - 1, Convert.ToInt32(data[0]) - 1] = Convert.ToDouble(data[2]) <= r ? true : false;

                        if (data[3] != "")
                            population.Add(Convert.ToDouble(data[3]));//data.Length-1
                    }

                    line++;
                }


                fileread.Close();
                //Console.WriteLine($"crane number{NumCrane}");
                //Console.WriteLine($"number of Container {Con_List.ListCon[2].YardNum}");
            }
            // Console.WriteLine($"Read containers successfully, totoaly we have {ListCon.Count - 1} containers \n");
            return (distArry, coverArry, population);
        }

        public static (double[,], bool[,], List<double>) GenZDS(int n, double r)
        {
            Random rand = new Random(8);
            List<(double, double)> coodinate = new List<(double, double)>();
            double[,] distArry = new double[n, n];
            bool[,] coverArry = new bool[n, n];
            List<double> population = new List<double>();


            using (StreamWriter sw = new StreamWriter("names.txt"))
            {
                sw.WriteLine($"{n} {n} 0");
                for (int i = 0; i < n; i++)
                {
                    population.Add(rand.Next(0, 100));
                    coodinate.Add((rand.Next(0, 30), rand.Next(0, 30)));
                    sw.WriteLine($" {coodinate[i].Item1} {coodinate[i].Item2} {population[i]} ");
                }
                sw.Close();
            }



            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    distArry[i, j] = Math.Sqrt((double)((coodinate[i].Item1 - coodinate[j].Item1) * (coodinate[i].Item1 - coodinate[j].Item1) + (coodinate[i].Item2 - coodinate[j].Item2) * (coodinate[i].Item2 - coodinate[j].Item2)));
                    coverArry[i, j] = distArry[i, j] <= r ? true : false;
                    coverArry[j, i] = coverArry[i, j];
                }




            return (distArry, coverArry, population);


        }

        public static (double[,], bool[,], List<double>) ReadB700900(string path, int n, double r)
        {
            List<double> population = new List<double>();
            double[,] distArry = new double[n, n];
            bool[,] coverArry = new bool[n, n];

            int[,] graph = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i == j)
                        graph[i, j] = 0;
                    else
                        graph[i, j] = 999;
            using (StreamReader fileread = new StreamReader(path))//
            {
                int line = 0;
                while (fileread.EndOfStream != true)
                {
                    string[] data = fileread.ReadLine().Split("\t");//数据按照空格分离
                    if (line >= 1)//by content not line number
                    {
                        graph[Convert.ToInt16(data[0]) - 1, Convert.ToInt16(data[1]) - 1] = Convert.ToInt16(data[2]);
                        graph[Convert.ToInt16(data[1]) - 1, Convert.ToInt16(data[0]) - 1] = Convert.ToInt16(data[2]);
                        if (line <= n)
                            population.Add(Convert.ToDouble(data[3]));//data.Length-1
                    }
                    line++;
                }
                fileread.Close();


                distArry = FloydWarshall(graph, n);

                for (int i = 0; i < n; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        coverArry[i, j] = distArry[i, j] <= r ? true : false;
                        coverArry[j, i] = coverArry[i, j];
                        coverArry[i, i] = true;

                    }

                }
                //Console.WriteLine($"{coodinate[0].Item1}, {coodinate[0].Item2}, {string.Join(" ", data1.ToArray())}, {(double)coodinate[0].Item1 * Math.PI / 180} , {distArry[0, 1]} ");


            }
            // Console.WriteLine($"Read containers successfully, totoaly we have {ListCon.Count - 1} containers \n");
            return (distArry, coverArry, population);

        }

        public static (double[,], bool[,], List<double>) ReadBDS(string path, int n, double r)
        {
            List<(double, double)> coodinateSite = new List<(double, double)>();
            List<(double, double)> coodinatenNode = new List<(double, double)>();

            List<double> populationSite = new List<double>();
            List<double> populationNode = new List<double>();
            double[,] distArry = new double[n, n];

            using (StreamReader fileread = new StreamReader(path))// 
            {
                int line = 0;

                while (fileread.EndOfStream != true)
                { //Console.Write (Convert.ToInt32(fileread.Read())*100);

                    string[] data = fileread.ReadLine().Split("\t");//数据按照空格分离
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
            List<int> Nodes = new List<int>();
            for (int i = 0; i < coodinatenNode.Count; i++)
            {
                int NumOfCover = 0; int coverBySite = 0;
                for (int j = 0; j < coodinateSite.Count; j++)
                {
                    var distance = Math.Sqrt((double)((coodinatenNode[i].Item1 - coodinateSite[j].Item1) * (coodinatenNode[i].Item1 - coodinateSite[j].Item1) + (coodinatenNode[i].Item2 - coodinateSite[j].Item2) * (coodinatenNode[i].Item2 - coodinateSite[j].Item2)));
                    distArry[i, j] = distance;
                    if (distance <= r)
                    {
                        coverBySite = j;
                        NumOfCover++;
                    }

                }
                if (NumOfCover == 1)  // 如果仅能被一个site 备选点覆盖那么 直接把这个点 归结到覆盖他的点上去， 如果覆盖为0 直接不管。
                    populationSite[coverBySite] += populationNode[i];//把原来的点集中
                else if (NumOfCover > 1) // 如果仅能被一个site 备选点覆盖那么 直接把这个点 归结到覆盖他的点上去
                    Nodes.Add(i);
            }
            //Console.WriteLine($"{coodinate[0].Item1}, {coodinate[0].Item2}, {string.Join(" ", data1.ToArray())}, {(double)coodinate[0].Item1 * Math.PI / 180} , {distArry[0, 1]} ");

            int Num_PossibleSite = populationSite.Count; int Num_nodes = Nodes.Count;
            bool[,] coverArry = new bool[Num_PossibleSite, Num_nodes + Num_PossibleSite];
            List<double> population = new List<double>();
            for (int i = 0; i < Num_PossibleSite; i++) // 计算覆盖关系
            {
                for (int j = 0; j < Num_nodes; j++)
                {
                    coverArry[i, j] = distArry[i, Nodes[j]] <= r ? true : false;
                }
                for (int j = 0; j < Num_PossibleSite; j++)
                {
                    var distance = Math.Sqrt((double)((coodinateSite[i].Item1 - coodinateSite[j].Item1) * (coodinateSite[i].Item1 - coodinateSite[j].Item1) + (coodinateSite[i].Item2 - coodinateSite[j].Item2) * (coodinateSite[i].Item2 - coodinateSite[j].Item2)));
                    coverArry[i, j + Num_nodes] = distance <= r ? true : false;
                }
            }

            for (int j = 0; j < Num_nodes; j++)
                population.Add(population[Nodes[j]]);
            for (int j = 0; j < Num_PossibleSite; j++)
                population.Add(populationSite[j]);
       
            // Console.WriteLine($"Read containers successfully, totoaly we have {ListCon.Count - 1} containers \n");
            return (distArry, coverArry, population);

        }











    private const double EARTH_RADIUS = 6378100; private static double rad(double d) { return d * Math.PI / 180.0; }
    public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
    {
        double radLat1 = rad(lat1);
        double radLat2 = rad(lat2);
        double a = radLat1 - radLat2;
        double b = rad(lng1) - rad(lng2);
        double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
        // Math.Acos(Math.Cos((double)lat1) * Math.Cos((double)lat2) * Math.Cos((double)-b) + Math.Sin((double)lat1) * Math.Sin((double)lat2)) * 6378100;


        s = s * EARTH_RADIUS;
        s = Math.Round(s * 10000) / 10000;
        return s;//单位：米
                 //
    }
    public static double[,] FloydWarshall(int[,] graph, int verticesCount)
    {
        double[,] distance = new double[verticesCount, verticesCount];

        for (int i = 0; i < verticesCount; ++i)
            for (int j = 0; j < verticesCount; ++j)
                distance[i, j] = graph[i, j];

        for (int k = 0; k < verticesCount; ++k)
        {
            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (distance[i, k] + distance[k, j] < distance[i, j])
                        distance[i, j] = distance[i, k] + distance[k, j];
                }
            }
        }

        return distance;
    }


}
}
