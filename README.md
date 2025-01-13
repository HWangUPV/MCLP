# Cross Entropy Method for the Maximal Covering Location Problem (MCLP)
Cross Entropy Method for the Maximal Covering Location Problem

Programming language: C#

All data involved in this work are attached and Ziped (see, MCLP_Benchmark_Instances.zip). It includes benchmark instances of 8 popular data sets, including random sets GR , Beasley , ZDS, PCB , real-world sets SJC, and Maximo, as well as benchmark data sets BDS and extended data BDS1000 for the asymmetric MCLP.

Some of the original data links are also available at the website http://www.lac.inpe.br/~lorena/instancias.html (SJC), http://www.https://sites.google.com/site/nascimentomcv/downloads/mclp (Maximo), https://github.com/fabiofurini/LocationCovering (BDS).




[![INFORMS Journal on Computing Logo](https://INFORMSJoC.github.io/logos/INFORMS_Journal_on_Computing_Header.jpg)](https://pubsonline.informs.org/journal/ijoc)


# Cross-entropy Method for the Maximal Covering Location Problem

This archive is distributed in association with the [INFORMS Journal on
Computing](https://pubsonline.informs.org/journal/ijoc) under the [CC BY-NC-SA 4.0 License](LICENSE).

The software and data in this repository are a snapshot of the software and data that were used in the research reported on in the paper [Cross-entropy Method for the Maximal Covering Location Problem](https://doi.org/10.1287/ijoc.2024.0611) by Hongtao Wang and Jian Zhou.

The snapshot is based on this [GitHub repository](https://github.com/HWangUPV/MCLP).


## Cite
To cite the contents of this repository, please cite both the paper and this repo, using their respective DOIs.

https://doi.org/10.1287/ijoc.2024.0611

https://doi.org/10.1287/ijoc.2024.0611.cd

Below is the BibTex for citing this snapshot of the repository.

```
@misc{iio2024,
  author =        {Wang, Hongtao and Zhou, Jian}
  publisher=      {INFORMS Journal on Computing},
  title =         {Cross-entropy Method for the Maximal Covering Location Problem},
  year =          {2024},
  doi =           {10.1287/ijoc.2024.0611.cd},
  url =           {https://github.com/INFORMSJoC/2024.0611},
  note =          {Available for download at https://github.com/INFORMSJoC/2024.0611}
}
```

## Description
This repository provides the core codes and data of Cross-entropy Method for the Maximal Covering Location Problem (MCLP).


## Content of the repository
This repository includes 

* the original codes in C# as shown in directory [src](src/), where two independent programs are uploaded for the two MCLP settings ($I=J$ and  $I \neq J$) in this paper [Cross-entropy Method for the Maximal Covering Location Problem](https://doi.org/10.1287/ijoc.2024.0611).
* the complete instances of the paper, directory [data](data/), from 8 benchmark data including G&R ([Galvão and ReVelle 1996](https://doi.org/10.1016/S0966-8349%2897%2983342-6), [Galvão et al. 2000](https://doi.org/10.1016/S0377-2217%2899%2900171-X)), Beasley ([Beasley 1990](https://www.tandfonline.com/doi/abs/10.1057/jors.1990.166)), ZDS ([Zarandi et al. 2011](https://www.sciencedirect.com/science/article/pii/S1026309811002100)), PCB ([Reinelt 1994](http://dx.doi.org/10.5772/5583)), and real-world benchmark data SJC ([Lorena and Pereira 2002](https://citeseerx.ist.psu.edu/document?repid=rep1&type=pdf&doi=75515a186e951958f8ed4a90362338f6b0646746)), Máximo ([Máximo et al. 2017](https://www.sciencedirect.com/science/article/abs/pii/S0305054816302131)), as well as massive benchmark data BDS ([Cordeau et al. 2019](https://www.sciencedirect.com/science/article/abs/pii/S0377221718310737)) and data BDS1000 extended in this research.
* the configuration files to launch the executable program, directory [cfgs](cfgs/),



## Benchmark instances
All the benchmark instances we generated for the paper experiments are also available at this [GitHub repository](https://github.com/HWangUPV/MCLP). 

All data involved in this work are also attached and Ziped (see MCLP_Benchmark_Instances.zip). It includes benchmark instances of 8 popular data sets, including random sets GR , Beasley , ZDS, PCB , real-world sets SJC, and Máximo, as well as benchmark data sets BDS and extended data BDS1000 for the MCLP.

Some of these benchmark data are also available at websites  [http://www.lac.inpe.br/~lorena/instancias.html](http://www.lac.inpe.br/~lorena/instancias.html)  (SJC),  [http://www.https://sites.google.com/site/nascimentomcv/downloads/mclp](http://www.https//sites.google.com/site/nascimentomcv/downloads/mclp)  (Máximo),  [https://github.com/fabiofurini/LocationCovering](https://github.com/fabiofurini/LocationCovering)  (BDS).

Each instance file contains information on nodes and is named with the data set and number of nodes, e.g., SJC708.txt. The first line of the instance file includes the basic information about the instances. The number of chosen facilities and service radius are not specified in the instance file but are defined as a part of the parameter arguments.

## Instruction to run the program
### Step1. Compile the codes

Compilation is required to test the corresponding codes. We compile the program with Visio Studio 2019 using .Net 6.0, after which the new executable file will be shown in the subdirectory
```
./bin/Release/.Net6.0/MCLP2023.exe
```
### Step2. Run with arguments
Arguments are required to run the executable program. Basically, the arguments consist of the instances and required parameters of the algorithm. 
```
Method InstanceFolder InstanceName ResultFolder NumOfFacililty(p) Radius(r) CE-parameters(N ρ nLS α Cmax) RandomSeed
```
A configuration example:
```
CE Instances SJC708.txt Results 6 800 2000 0.05 20 0.8 50 0
```
- Method: Cross-entropy method (CE)
- InstanceFolder: folder name of the corresponding single instance
- InstanceName:  SJC708.txt is the name of the chosen benchmark instance where SJC represents the data set, and 708 is the number of customers.
- ResultFolder : folder name of the results (Results)
- $p$: number of chosen facility
- $r$:  service radius, 800
- $N$: CE parameter, 2000 
- $\rho$ : CE parameter, 0.05
- $nLS$: CE parameter, 20
- $\alpha$: CE parameter, 0.8
- $Cmax$: CE parameter, 50
- $Seed$: the seed of randomness, 0

Compilation with the example configuration has been successfully tested on a machine running Windows operating system.


##  Development and support

We warmly welcome any corrections, suggestions, or collaborations from the community to improve this work further.

You may want to check out the code main developer's [GitHub site](https://github.com/HWangUPV).

For any development, support and suggestions, please submit an
[issue](https://github.com/HWangUPV/MCLP/issues/new), or [send us emails](mailto:hwang8@doctor.upv.es;zhou_jian@shu.edu.cn?cc=htwang.upv@gmail.com&subject=MCLP%20Repo%20-%20Question).


## License
Repository license file [LICENSE](LICENSE).

Shield: [![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg
