using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Colour[,] Image2 = new Colour[8, 8];
            Colour[,] Image = new Colour[8, 8];

            //initializing the Image array
            #region
            Image[0, 0] = Colour.WHITE;
            Image[0, 1] = Colour.WHITE;
            Image[0, 2] = Colour.WHITE;
            Image[0, 3] = Colour.WHITE;
            Image[0, 4] = Colour.BLACK;
            Image[0, 5] = Colour.BLACK;
            Image[0, 6] = Colour.WHITE;
            Image[0, 7] = Colour.WHITE;
            Image[1, 0] = Colour.WHITE;
            Image[1, 1] = Colour.WHITE;
            Image[1, 2] = Colour.WHITE;
            Image[1, 3] = Colour.WHITE;
            Image[1, 4] = Colour.BLACK;
            Image[1, 5] = Colour.BLACK;
            Image[1, 6] = Colour.WHITE;
            Image[1, 7] = Colour.WHITE;
            Image[2, 0] = Colour.WHITE;
            Image[2, 1] = Colour.WHITE;
            Image[2, 2] = Colour.WHITE;
            Image[2, 3] = Colour.WHITE;
            Image[2, 4] = Colour.WHITE;
            Image[2, 5] = Colour.WHITE;
            Image[2, 6] = Colour.WHITE;
            Image[2, 7] = Colour.WHITE;
            Image[3, 0] = Colour.WHITE;
            Image[3, 1] = Colour.WHITE;
            Image[3, 2] = Colour.WHITE;
            Image[3, 3] = Colour.WHITE;
            Image[3, 4] = Colour.WHITE;
            Image[3, 5] = Colour.WHITE;
            Image[3, 6] = Colour.WHITE;
            Image[3, 7] = Colour.WHITE;
            Image[4, 0] = Colour.WHITE;
            Image[4, 1] = Colour.WHITE;
            Image[4, 2] = Colour.WHITE;
            Image[4, 3] = Colour.WHITE;
            Image[4, 4] = Colour.WHITE;
            Image[4, 5] = Colour.WHITE;
            Image[4, 6] = Colour.WHITE;
            Image[4, 7] = Colour.WHITE;
            Image[5, 0] = Colour.WHITE;
            Image[5, 1] = Colour.WHITE;
            Image[5, 2] = Colour.WHITE;
            Image[5, 3] = Colour.WHITE;
            Image[5, 4] = Colour.WHITE;
            Image[5, 5] = Colour.WHITE;
            Image[5, 6] = Colour.WHITE;
            Image[5, 7] = Colour.WHITE;
            Image[6, 0] = Colour.BLACK;
            Image[6, 1] = Colour.BLACK;
            Image[6, 2] = Colour.WHITE;
            Image[6, 3] = Colour.WHITE;
            Image[6, 4] = Colour.WHITE;
            Image[6, 5] = Colour.WHITE;
            Image[6, 6] = Colour.WHITE;
            Image[6, 7] = Colour.BLACK;
            Image[7, 0] = Colour.BLACK;
            Image[7, 1] = Colour.BLACK;
            Image[7, 2] = Colour.WHITE;
            Image[7, 3] = Colour.WHITE;
            Image[7, 4] = Colour.WHITE;
            Image[7, 5] = Colour.WHITE;
            Image[7, 6] = Colour.BLACK;
            Image[7, 7] = Colour.WHITE;
            #endregion


            //initiallizing the Image2 array
            #region
            Image2[0, 0] = Colour.WHITE;
            Image2[0, 1] = Colour.WHITE;
            Image2[0, 2] = Colour.WHITE;
            Image2[0, 3] = Colour.WHITE;
            Image2[0, 4] = Colour.BLACK;
            Image2[0, 5] = Colour.BLACK;
            Image2[0, 6] = Colour.WHITE;
            Image2[0, 7] = Colour.WHITE;
            Image2[1, 0] = Colour.WHITE;
            Image2[1, 1] = Colour.WHITE;
            Image2[1, 2] = Colour.WHITE;
            Image2[1, 3] = Colour.WHITE;
            Image2[1, 4] = Colour.BLACK;
            Image2[1, 5] = Colour.BLACK;
            Image2[1, 6] = Colour.WHITE;
            Image2[1, 7] = Colour.WHITE;
            Image2[2, 0] = Colour.WHITE;
            Image2[2, 1] = Colour.WHITE;
            Image2[2, 2] = Colour.WHITE;
            Image2[2, 3] = Colour.WHITE;
            Image2[2, 4] = Colour.WHITE;
            Image2[2, 5] = Colour.WHITE;
            Image2[2, 6] = Colour.WHITE;
            Image2[2, 7] = Colour.WHITE;
            Image2[3, 0] = Colour.WHITE;
            Image2[3, 1] = Colour.WHITE;
            Image2[3, 2] = Colour.WHITE;
            Image2[3, 3] = Colour.WHITE;
            Image2[3, 4] = Colour.WHITE;
            Image2[3, 5] = Colour.WHITE;
            Image2[3, 6] = Colour.WHITE;
            Image2[3, 7] = Colour.WHITE;
            Image2[4, 0] = Colour.WHITE;
            Image2[4, 1] = Colour.WHITE;
            Image2[4, 2] = Colour.WHITE;
            Image2[4, 3] = Colour.WHITE;
            Image2[4, 4] = Colour.WHITE;
            Image2[4, 5] = Colour.WHITE;
            Image2[4, 6] = Colour.WHITE;
            Image2[4, 7] = Colour.WHITE;
            Image2[5, 0] = Colour.WHITE;
            Image2[5, 1] = Colour.WHITE;
            Image2[5, 2] = Colour.WHITE;
            Image2[5, 3] = Colour.WHITE;
            Image2[5, 4] = Colour.WHITE;
            Image2[5, 5] = Colour.WHITE;
            Image2[5, 6] = Colour.WHITE;
            Image2[5, 7] = Colour.WHITE;
            Image2[6, 0] = Colour.BLACK;
            Image2[6, 1] = Colour.BLACK;
            Image2[6, 2] = Colour.WHITE;
            Image2[6, 3] = Colour.WHITE;
            Image2[6, 4] = Colour.WHITE;
            Image2[6, 5] = Colour.WHITE;
            Image2[6, 6] = Colour.BLACK;
            Image2[6, 7] = Colour.WHITE;
            Image2[7, 0] = Colour.BLACK;
            Image2[7, 1] = Colour.BLACK;
            Image2[7, 2] = Colour.WHITE;
            Image2[7, 3] = Colour.WHITE;
            Image2[7, 4] = Colour.WHITE;
            Image2[7, 5] = Colour.WHITE;
            Image2[7, 6] = Colour.WHITE;
            Image2[7, 7] = Colour.BLACK;
            #endregion



            Console.WriteLine("Writing the value of two colour arrays\n\n");
            Console.WriteLine("array1:\n");
            for (int i = 0; i < Image2.GetLength(0); i++)
            {
                for (int j = 0; j < Image2.GetLength(0); j++)
                {
                    if (Image[i, j] == Colour.WHITE)
                        Console.Write("w ");
                    else
                        Console.Write("b ");
                }
                Console.Write("\n");
            }
            Console.WriteLine();
            Console.WriteLine("array2:\n");
            for (int i = 0; i < Image.GetLength(0); i++)
            {
                for (int j = 0; j < Image.GetLength(0); j++)
                {
                    if (Image2[i, j] == Colour.WHITE)
                        Console.Write("w ");
                    else
                        Console.Write("b ");
                }
                Console.Write("\n");
            }

            //Testing Constructor
            Console.WriteLine("Creating two trees with the former arrays");
            QuadTree quadTree = new QuadTree(Image);
            QuadTree quadTree1 = new QuadTree(Image2);

            Console.WriteLine("\n\nprinting out the values of both trees");
            //testing print
            quadTree.Print();
            Console.Write("\n");
            quadTree1.Print();
            Console.Write("\n");

            Console.WriteLine("Unionizing the two trees");
            //Testing union
            QuadTree quadTreeUnion = quadTree.Union(quadTree1);
            Console.WriteLine("printing out the result");
            quadTreeUnion.Print();

            //Testing switch
            Console.Write("\n");
            Console.Write("Testing switch on the bottom right black Node");
            quadTreeUnion.Switch(7,7);
            Console.Write("\n");
            Console.Write("Printing result");
            quadTreeUnion.Print();



            Console.ReadLine();






        }
    }
}
