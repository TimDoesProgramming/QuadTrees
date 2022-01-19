using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree
{
    public enum Colour { BLACK, WHITE, GRAY }

    
    class QuadTree
    {
        Node root;
        int maxDepth;

        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        //  Constructor
        //  builds a quad tree with queues, has assumption the size of the image will be atleast of depth 2
        //  Time Complexity O(N)
        public QuadTree(Colour[,] Image)
        {

            root = new Node(Colour.GRAY);  //root of tree
            int sideLength = Image.GetLength(0); // if the image was a square this is the length of a side
            maxDepth = (int)Math.Log((double)sideLength, 2); // the max depth of the tree with the root as depth 0
            Queue<Node> qN = new Queue<Node>(); //qN for queue Nodes   
            Queue<Colour> qC = new Queue<Colour>(); //qN for queue colours
            Node current;

            qN.Enqueue(root);

            //creates the tree up to maxdepth - 1
            do
            {
                current = qN.Dequeue();

                current.NW = new Node(Colour.GRAY);
                current.NE = new Node(Colour.GRAY);
                current.SW = new Node(Colour.GRAY);
                current.SE = new Node(Colour.GRAY);
                qN.Enqueue(current.NW);
                qN.Enqueue(current.NE);
                qN.Enqueue(current.SW);
                qN.Enqueue(current.SE);

            } while (qN.Count != (sideLength * sideLength) / 4);

            //makes the leaf nodes all white
            while (qN.Count != 0)
            {
                current = qN.Dequeue();

                current.NE = new Node(Colour.WHITE);
                current.NW = new Node(Colour.WHITE);
                current.SE = new Node(Colour.WHITE);
                current.SW = new Node(Colour.WHITE);
            }
            
            // enqueues the colours from the  Image array based on the zone they belong to

            //NW zone
            for (int row = 0; row < sideLength/2; row += 2)
                for (int column = 0; column < sideLength/2; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    qC.Enqueue(Image[row, column]);
                    qC.Enqueue(Image[row, column + 1]);
                    qC.Enqueue(Image[row + 1, column]);
                    qC.Enqueue(Image[row + 1, column + 1]);
                }
            //NE zone
            for (int row = 0; row < sideLength / 2; row += 2)
                for (int column = sideLength/2; column < sideLength; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    qC.Enqueue(Image[row, column]);
                    qC.Enqueue(Image[row, column + 1]);
                    qC.Enqueue(Image[row + 1, column]);
                    qC.Enqueue(Image[row + 1, column + 1]);
                }
            //SW zone
            for (int row = sideLength / 2; row < sideLength; row += 2)
                for (int column = 0; column < sideLength / 2; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    qC.Enqueue(Image[row, column]);
                    qC.Enqueue(Image[row, column + 1]);
                    qC.Enqueue(Image[row + 1, column]);
                    qC.Enqueue(Image[row + 1, column + 1]);
                }
            //SE zone
            for (int row = sideLength / 2; row < sideLength; row += 2)
                for (int column = sideLength / 2; column < sideLength; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    qC.Enqueue(Image[row, column]);
                    qC.Enqueue(Image[row, column + 1]);
                    qC.Enqueue(Image[row + 1, column]);
                    qC.Enqueue(Image[row + 1, column + 1]);
                }

            CheckColour(root, ref qC);
            this.Compress();
        }


        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        //  CheckColour
        //  sets the leaf Nodes to the correct colour 
        //  Time Complexity O(N)
        private void CheckColour(Node root, ref Queue<Colour> qC)
        {
            if (root.NW.NW != null)
            {
                CheckColour(root.NW, ref qC);
                CheckColour(root.NE, ref qC);
                CheckColour(root.SW, ref qC);
                CheckColour(root.SE, ref qC);
            }
            else
            {
                root.NW.C = qC.Dequeue();
                root.NE.C = qC.Dequeue();
                root.SW.C = qC.Dequeue();
                root.SE.C = qC.Dequeue();
            }
           
        }

        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        //  Compress
        //  if there are redunancies in the tree(i.e. a gray node with 4 whites or 4 blacks) it decreases the size 
        //  Time Complexity O(N)

        public void Compress()
        {
            this.root = Compress(this.root);
        }
        private Node Compress(Node root)
        {
            if(root.NW.C == Colour.GRAY) {

                Compress(root.NW);

            }
            if(root.NE.C == Colour.GRAY) {

                Compress(root.NE);

            }
            if(root.SW.C == Colour.GRAY) {

                Compress(root.SW);

            }if(root.SE.C == Colour.GRAY) { 

                    Compress(root.SE);

            }
            if (root.NW.C == root.NE.C && root.SW.C == root.SE.C && root.NW.C == root.SW.C)
            {         
                root.C = root.NW.C;
                root.NW = null;
                root.NE = null;
                root.SW = null;
                root.SE = null;
            }
            return root;

        }
        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        //  Print
        //  prints the tree by decrompressing and printing 
        //  Time Complexity O(N)
        public void Print()
        {
            Colour[,] Colours = this.Decompress();

            for (int i = 0; i < Colours.GetLength(0); i++)
            {
                for (int j = 0; j < Colours.GetLength(0); j++)
                {
                    if (Colours[i, j] == Colour.WHITE)
                        Console.Write("w ");
                    else
                        Console.Write("b ");
                }
                Console.Write("\n");
            }
           
        }
        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

            //  Decompress
            //  Creates a 2d array of colours based on the tree
            // time complexity O(N)
        public Colour[,] Decompress()
        {
            int sideLength = (int)Math.Pow(2, (double)maxDepth);

            Colour[,] colours = new Colour[sideLength, sideLength];

            
            QuadTree temp = this;
            
            Node current;

            Queue<Node> qN = new Queue<Node>();

            qN.Enqueue(temp.root.NW);
            qN.Enqueue(temp.root.NE);
            qN.Enqueue(temp.root.SW);
            qN.Enqueue(temp.root.SE);

            do
            {
                current = qN.Dequeue();

                if (current.NW == null)
                {

                    current.NW = new Node(current.C);
                    current.NE = new Node(current.C);
                    current.SW = new Node(current.C);
                    current.SE = new Node(current.C);
                }
                qN.Enqueue(current.NW);
                qN.Enqueue(current.NE);
                qN.Enqueue(current.SW);
                qN.Enqueue(current.SE);

              

            } while (qN.Count != sideLength * sideLength);


            for (int row = 0; row < sideLength / 2; row += 2)
                for (int column = 0; column < sideLength / 2; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    colours[row, column] = qN.Dequeue().C;
                    colours[row, column + 1] = qN.Dequeue().C;
                    colours[row + 1, column] = qN.Dequeue().C;
                    colours[row + 1, column + 1] = qN.Dequeue().C;
                }
            //NE Zone
            for (int row = 0; row < sideLength / 2; row += 2)
                for (int column = sideLength / 2; column < sideLength; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    colours[row, column] = qN.Dequeue().C;
                    colours[row, column + 1] = qN.Dequeue().C;
                    colours[row + 1, column] = qN.Dequeue().C;
                    colours[row + 1, column + 1] = qN.Dequeue().C;
                }

            //SW zone
            for (int row = sideLength / 2; row < sideLength; row += 2)
                for (int column = 0; column < sideLength / 2; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    colours[row, column] = qN.Dequeue().C;
                    colours[row, column + 1] = qN.Dequeue().C;
                    colours[row + 1, column] = qN.Dequeue().C;
                    colours[row + 1, column + 1] = qN.Dequeue().C;
                }
            //SE zone
            for (int row = sideLength / 2; row < sideLength; row += 2)
                for (int column = sideLength / 2; column < sideLength; column += 2)
                {
                    // enqueued in this order NW -> NE -> SW -> SE
                    colours[row, column] = qN.Dequeue().C;
                    colours[row, column + 1] = qN.Dequeue().C;
                    colours[row + 1, column] = qN.Dequeue().C;
                    colours[row + 1, column + 1] = qN.Dequeue().C;
                }

            return colours;
        }

        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

        //  Union 
        //  creates a new quadtree compares the two trees to make a new tree with values as BLACK > GRAY > WHITE
        //  Time Complexity O(N)
        //
        public QuadTree Union(QuadTree q)
        {
            //makes sure the trees are of the same size
            if (q.maxDepth != this.maxDepth)
            {
                return (q.maxDepth > this.maxDepth) ? q : this;
            }
            

            Node v1, v2, current;
            QuadTree newTree = this;
            v1 = newTree.root;
            v2 = q.root;

            // queues made for comparing
            Queue<Node> qV1 = new Queue<Node>();
            Queue<Node> qV2 = new Queue<Node>();
            Queue<Node> union = new Queue<Node>();

            //enqueueing the first 4 nodes of each tree
            qV1.Enqueue(v1.NW);
            qV1.Enqueue(v1.NE);
            qV1.Enqueue(v1.SW);
            qV1.Enqueue(v1.SE);

            qV2.Enqueue(v2.NW);
            qV2.Enqueue(v2.NE);
            qV2.Enqueue(v2.SW);
            qV2.Enqueue(v2.SE);

            //en
            union.Enqueue(newTree.root);

            // checks the tree breadth first and zone by zone
            do
            {
                current = union.Dequeue();

                //NW
                v1 = qV1.Dequeue();
                v2 = qV2.Dequeue();
                if (v1.C == Colour.BLACK || v2.C == Colour.BLACK)
                {
                    current.NW = new Node(Colour.BLACK);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.WHITE)
                {
                    current.NW = new Node(Colour.WHITE);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.GRAY)
                {
                    current.NW = v2;
                }
                else if (v1.C == Colour.GRAY && v2.C == Colour.WHITE)
                {
                    current.NW = v1;
                }
                else
                {
                    current.NW = new Node(Colour.GRAY);

                    union.Enqueue(current.NW);

                    qV1.Enqueue(v1.NW);
                    qV1.Enqueue(v1.NE);
                    qV1.Enqueue(v1.SW);
                    qV1.Enqueue(v1.SE);

                    qV2.Enqueue(v2.NW);
                    qV2.Enqueue(v2.NE);
                    qV2.Enqueue(v2.SW);
                    qV2.Enqueue(v2.SE);
                }


                // NE
                v1 = qV1.Dequeue();
                v2 = qV2.Dequeue();
                if (v1.C == Colour.BLACK || v2.C == Colour.BLACK)
                {
                    current.NE = new Node(Colour.BLACK);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.WHITE)
                {
                    current.NE = new Node(Colour.WHITE);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.GRAY)
                {
                    current.NE = v2;
                }
                else if (v1.C == Colour.GRAY && v2.C == Colour.WHITE)
                {
                    current.NE = v1;
                }
                else
                {
                    current.NE = new Node(Colour.GRAY);

                    union.Enqueue(current.NE);

                    qV1.Enqueue(v1.NW);
                    qV1.Enqueue(v1.NE);
                    qV1.Enqueue(v1.SW);
                    qV1.Enqueue(v1.SE);

                    qV2.Enqueue(v2.NW);
                    qV2.Enqueue(v2.NE);
                    qV2.Enqueue(v2.SW);
                    qV2.Enqueue(v2.SE);
                }

                //SW
                v1 = qV1.Dequeue();
                v2 = qV2.Dequeue();
                if (v1.C == Colour.BLACK || v2.C == Colour.BLACK)
                {
                    current.SW = new Node(Colour.BLACK);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.WHITE)
                {
                    current.SW = new Node(Colour.WHITE);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.GRAY)
                {
                    current.SW = v2;
                }
                else if (v1.C == Colour.GRAY && v2.C == Colour.WHITE)
                {
                    current.SW = v1;
                }
                else
                {
                    current.SW = new Node(Colour.GRAY);
                    union.Enqueue(current.SW);

                    qV1.Enqueue(v1.NW);
                    qV1.Enqueue(v1.NE);
                    qV1.Enqueue(v1.SW);
                    qV1.Enqueue(v1.SE);

                    qV2.Enqueue(v2.NW);
                    qV2.Enqueue(v2.NE);
                    qV2.Enqueue(v2.SW);
                    qV2.Enqueue(v2.SE);
                }

                //SE
                v1 = qV1.Dequeue();
                v2 = qV2.Dequeue();
                if (v1.C == Colour.BLACK || v2.C == Colour.BLACK)
                {
                    current.SE = new Node(Colour.BLACK);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.WHITE)
                {
                    current.SE = new Node(Colour.WHITE);
                }
                else if (v1.C == Colour.WHITE && v2.C == Colour.GRAY)
                {
                    current.SE = v2;
                }
                else if (v1.C == Colour.GRAY && v2.C == Colour.WHITE)
                {
                    current.SE = v1;
                }
                else
                { 
                    current.SE = new Node(Colour.GRAY);

                    union.Enqueue(current.SE);

                    qV1.Enqueue(v1.NW);
                    qV1.Enqueue(v1.NE);
                    qV1.Enqueue(v1.SW);
                    qV1.Enqueue(v1.SE);

                    qV2.Enqueue(v2.NW);
                    qV2.Enqueue(v2.NE);
                    qV2.Enqueue(v2.SW);
                    qV2.Enqueue(v2.SE);
                }

            } while (qV1.Count > 0);

            return newTree;
        }
        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------
        //  Switch
        //  Swaps the colour of a node calls the private switch
        //  Time complexity log(N)
        public void Switch(int i, int j)
        {

            Region regOfSwitch;
            int maxLeafNodes = (int)Math.Pow(4, (double)maxDepth);
            int specificNode;
            int sideLength = (int)Math.Pow(2, (double)maxDepth);

            if (i % 2 == 0)
            {
                if (j % 2 == 0)
                {
                    regOfSwitch = Region.NW;
                }
                if (j % 2 == 1)
                {
                    regOfSwitch = Region.NE;
                }
            }
            else if (j % 2 == 0)
            {
                regOfSwitch = Region.SW;
            }
            else
                regOfSwitch = Region.SE;

            specificNode = j + (sideLength*i);

            if(i >= sideLength || j >= sideLength)
            {
                Console.WriteLine("the indices were out of bound");
            }
            else
            {
                Switch(root, specificNode, 1);
            }

            
        }
        //  Switch
        //  follows the path down based on the particular node
        //  Time complexity log(N)
        private bool Switch(Node root, int specificNode, int currentDepth)
        {
            //gets the quadrant size of the quadrants below the root node
            int maxQuadSize = (int)Math.Pow(4, (double)(maxDepth - currentDepth));

            if (maxDepth - currentDepth != 0)
            {
                if (specificNode < maxQuadSize)
                {
                    if(root.NW.C != Colour.GRAY)
                    {
                        root.NW.NW = new Node(root.NW.C);
                        root.NW.NE = new Node(root.NW.C);
                        root.NW.SW = new Node(root.NW.C);
                        root.NW.SE = new Node(root.NW.C);
                    }
                    return Switch(root.NW, specificNode, currentDepth + 1);
                }
                else if (specificNode < maxQuadSize * 2)
                {
                    if (root.NE.C != Colour.GRAY)
                    {
                        root.NE.NW = new Node(root.NE.C);
                        root.NE.NE = new Node(root.NE.C);
                        root.NE.SW = new Node(root.NE.C);
                        root.NE.SE = new Node(root.NE.C);
                    }
                    specificNode -= maxQuadSize;
                    return Switch(root.NE, specificNode, currentDepth + 1);
                }
                else if (specificNode < maxQuadSize * 3)
                {
                    if (root.SW.C != Colour.GRAY)
                    {
                        root.SW.NW = new Node(root.SW.C);
                        root.SW.NE = new Node(root.SW.C);
                        root.SW.SW = new Node(root.SW.C);
                        root.SW.SE = new Node(root.SW.C);
                    }
                    specificNode -= maxQuadSize * 2;
                    return Switch(root.SW, specificNode, currentDepth + 1);
                }
                else if (specificNode < maxQuadSize * 4)
                {
                    if (root.SE.C != Colour.GRAY)
                    {
                        root.SE.NW = new Node(root.SE.C);
                        root.SE.NE = new Node(root.SE.C);
                        root.SE.SW = new Node(root.SE.C);
                        root.SE.SE = new Node(root.SE.C);
                    }
                    specificNode -= maxQuadSize * 3;
                    return Switch(root.SE, specificNode, currentDepth + 1);
                }
            }
            else
            {
                //NW
                if (specificNode == 0)
                {
                    if (root.NW.C == Colour.BLACK)
                    {
                        root.NW.C = Colour.WHITE;
                    }
                    else
                        root.NW.C = Colour.BLACK;
                    return true;
                }
                //NE
                if (specificNode == 1)
                {
                    if (root.NE.C == Colour.BLACK)
                    {
                        root.NE.C = Colour.WHITE;
                    }
                    else
                        root.NE.C = Colour.BLACK;
                    return true;
                }
                //SW
                if (specificNode == 2)
                {
                    if (root.SW.C == Colour.BLACK)
                    {
                        root.SW.C = Colour.WHITE;
                    }
                    else
                        root.SW.C = Colour.BLACK;
                    return true;
                }
                //SE
                if (specificNode == 3)
                {
                    
                    if (root.SE.C == Colour.BLACK)
                    {
                        root.SE.C = Colour.WHITE;
                    }
                    else
                        root.SE.C = Colour.BLACK;
                    return true;
                }
            }
            return false;
        }
        //----------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------

    }


}

