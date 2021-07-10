using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxSumOfSinglePathInPyramid
{
    class Program
    {
        static void Main(string[] args)
        {
            string pyramid = @"215
                               193 124
                               117 237 442
                               218 935 347 235
                               320 804 522 417 345
                               229 601 723 835 133 124
                               248 202 277 433 207 263 257
                               359 464 504 528 516 716 871 182
                               461 441 426 656 863 560 380 171 923
                               381 348 573 533 447 632 387 176 975 449
                               223 711 445 645 245 543 931 532 937 541 444
                               330 131 333 928 377 733 017 778 839 168 197 197
                               131 171 522 137 217 224 291 413 528 520 227 229 928
                               223 626 034 683 839 053 627 310 713 999 629 817 410 121
                               924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

            var pyramidMatrix = FormattingPyramidIntoMatrixWithZeroValuesMapping(pyramid);
            var originMatrix = (int[,])pyramidMatrix.Clone();
            var matrixWithSumValues = FindSumMatrix(pyramidMatrix);
            var pathOfMaxSum = FindPathOfMaxSum(originMatrix, matrixWithSumValues);
            Console.WriteLine($" Maximum sum: {matrixWithSumValues[0, 0]} \n");
            DrawThePath(pyramid, pathOfMaxSum);
        }

        //Formatting the pyramid as a matrix that has a value of zero where the number is prime. Row completion elements will also remain zero.
        static int[,] FormattingPyramidIntoMatrixWithZeroValuesMapping(string pyramid)
        {
            var formattedPyramid = pyramid.Split("\n").Select(p => p.Trim()).Select(p => p.Split()).ToArray();
            int len = formattedPyramid.Length;
            var pyramidMatrix = new int[len, len];

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (j < formattedPyramid[i].Length && !CheckPrime(int.Parse(formattedPyramid[i][j])))//If it's an element of pyramid and not prime at the same time,
                    {                                                                                    //we can pass it to matrix as it is.
                        pyramidMatrix[i, j] = int.Parse(formattedPyramid[i][j]);
                    }
                }
            }

            return pyramidMatrix;
        }

        //Finding the maximum sum.
        static int[,] FindSumMatrix(int[,] matrix)
        {
            int len = matrix.GetLength(0);

            for (int i = len - 2; i >= 0; i--)
            {
                for (int j = 0; j < len; j++)
                {
                    int parent = matrix[i, j], leftChild = matrix[i + 1, j];
                    int rightChild = j == len - 1 ? 0 : matrix[i + 1, j + 1];
                    int maxChild = Math.Max(leftChild, rightChild);

                    if (parent != 0 && (leftChild != 0 || rightChild != 0))
                    {
                        matrix[i, j] = parent + maxChild;
                    }
                }
            }

            return matrix;
        }

        //Finding the path that gives maximum sum.
        static List<int> FindPathOfMaxSum(int[,] pyramidMatrix, int[,] sumMatrix)
        {
            int maxSum, diff, index = 0;
            int len = pyramidMatrix.GetLength(0);
            List<int> path = new List<int>() { 0 };

            for (int i = 0; i < len - 1; i++)
            {
                maxSum = sumMatrix[i, index];
                diff = maxSum - pyramidMatrix[i, index];

                if (diff != 0)
                {
                    if (sumMatrix[i + 1, index] == diff)
                    {
                        path.Add(index);
                        maxSum = sumMatrix[i + 1, index];
                    }
                    else
                    {
                        path.Add(index + 1);
                        maxSum = sumMatrix[i + 1, index + 1];
                        index++;
                    }
                }
                else
                {
                    break;
                }
            }

            return path;
        }

        //Displat the path.
        static void DrawThePath(string pyramid, List<int> path)
        {
            var formattedPyramid = pyramid.Split("\n").Select(p => p.Trim()).Select(p => p.Split()).ToArray();

            for (int i = 0; i < path.Count; i++)
            {
                formattedPyramid[i][path[i]] += "*";
            }

            Console.WriteLine(" The path that gives maximum sum: \n");
            for (int i = 0; i < formattedPyramid.Length; i++)
            {
                for (int j = 0; j < formattedPyramid[i].Length; j++)
                {
                    Console.Write(" " + formattedPyramid[i][j] + "\t");
                }
                Console.WriteLine();
            }
        }

        //Checking the number if it's prime or not.
        static bool CheckPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }
            else if (n == 2)
            {
                return true;
            }
            else
            {
                for (int i = 2; i < n / 2; i++)
                {
                    if (n % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
