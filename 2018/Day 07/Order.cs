using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode
{
    public class Heap
    {
        int size;
        List<string> minHeap = new List<string>(new string[26]);
        public bool IsEmpty()
        {
            if (size == 0)
            {
                return true;
            }
            return false;
        }

        private int GetParentIndex(int n)
        {
            return (n - 1) / 2;
        }

        private int GetLeftChildIndex(int n)
        {
            return (n * 2) + 1;
        }

        private int GetRightChildIndex(int n)
        {
            return (n * 2) + 2;
        }

        private string GetParentValue(int n)
        {
            int idx = GetParentIndex(n);
            if (idx < 0)
            {
                return null;
            }
            return minHeap[idx];
        }

        private string GetLeftChildValue(int n)
        {
            int idx = GetLeftChildIndex(n);
            if (idx >= size)
            {
                return null;
            }
            return minHeap[idx];
        }

        private string GetRightChildValue(int n)
        {
            int idx = GetRightChildIndex(n);
            if (idx >= size)
            {
                return null;
            }
            return minHeap[idx];
        }

        public string ExtractMin()
        {
            string minElement = minHeap[0];
            // Swap last element with Root node
            minHeap[0] = minHeap[size - 1];
            //minHeap[0] = minHeap[minHeap.Count - 1];
            size--;
            // Heapify Down
            HeapifyDown();
            //Console.WriteLine("After Extracting {0}, the ordering is:", minElement);
            for (int i = 0; i < size; i++)
            //for (int i = 0; i < minHeap.Count; i++)
            {
                //Console.Write(minHeap[i] + " ");
            }
            //Console.WriteLine();
            minHeap.RemoveAt(minHeap.Count - 1);
            return minElement;
        }

        public void InsertElement(string newElement)
        {
            minHeap[size] = newElement;
            //minHeap.Add(newElement);
            size++;
            // Heapify Up
            HeapifyUp();
            //Console.WriteLine("After Inserting {0}, the ordering is:", newElement);
            for (int i = 0; i < size; i++)
            //for (int i = 0; i < minHeap.Count; i++)
            {
                //Console.Write(minHeap[i] + " ");
            }
            //Console.WriteLine();
        }

        private void HeapifyUp()
        {
            int idx = size - 1;
            string child = minHeap[idx];
            string parent = GetParentValue(idx);
            while (idx != 0 && String.Compare(child, parent) < 0)
            {
                int parentIdx = GetParentIndex(idx);
                Swap(idx, parentIdx);
                idx = parentIdx;
            }
        }

        private void HeapifyDown()
        {
            int rootIdx = 0;
            while (true)
            {
                string leftChild = GetLeftChildValue(rootIdx);
                string rightChild = GetRightChildValue(rootIdx);
                if (leftChild == null)
                {
                    break;
                }

                string minChild;
                int childIdx;
                if (rightChild == null)
                {
                    childIdx = GetLeftChildIndex(rootIdx);
                }
                else
                {
                    if (String.Compare(leftChild, rightChild) <= 0)
                    {
                        childIdx = GetLeftChildIndex(rootIdx);
                    }
                    else
                    {
                        childIdx = GetRightChildIndex(rootIdx);
                    }
                }
                if (String.Compare(minHeap[rootIdx], minHeap[childIdx]) < 0)
                {
                    break;
                }
                Swap(rootIdx, childIdx);
                rootIdx = childIdx;
            }
        }

        private void Swap(int root, int child)
        {
            string temp = minHeap[root];
            minHeap[root] = minHeap[child];
            minHeap[child] = temp;
        }
    }

    public class Order
    {

        public class Node
        {
            public int inDegree = 0;
            public List<string> outNodes = new List<string>();
        }

        public Node GetOrCreateNode(Dictionary<string, Node> adjList, string letter)
        {
            Node node = null;
            if (adjList.ContainsKey(letter))
            {
                node = adjList[letter];
            }
            else
            {
                node = new Node();
                adjList.Add(letter, node);
            }
            return node;
        }
        static void Main(string[] args)
        {
            string line;
            Dictionary<string, Node> adjList = new Dictionary<string, Node>();
            Dictionary<string, Node> adjListPartB = new Dictionary<string, Node>();

            List<string> topologicalOrder = new List<string>();
            while ((line = Console.ReadLine()) != null)
            {
                // input is: "Step C must be finished before step A can begin"

                //build the adjList
                // relation is C -> A
                Node prevNode = new Order().GetOrCreateNode(adjList, line[5].ToString());
                Node nextNode = new Order().GetOrCreateNode(adjList, line[36].ToString());

                Node prevNode1 = new Order().GetOrCreateNode(adjListPartB, line[5].ToString());
                Node nextNode1 = new Order().GetOrCreateNode(adjListPartB, line[36].ToString());

                prevNode.outNodes.Add(line[36].ToString());
                nextNode.inDegree += 1;

                prevNode1.outNodes.Add(line[36].ToString());
                nextNode1.inDegree += 1;
            }

            Console.WriteLine("Prinitng the contents");
            foreach (KeyValuePair<string, Node> kvp in adjList)
            {
                Console.WriteLine("Key: {0}. InNodes = {1}, OutNodes count = {2}", kvp.Key, kvp.Value.inDegree, kvp.Value.outNodes.Count);
                foreach (string s in kvp.Value.outNodes)
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
            }

            //adjListPartB = adjList;
            Console.WriteLine("Prinitng the contents of PartB");
            foreach (KeyValuePair<string, Node> kvp in adjListPartB)
            {
                Console.WriteLine("Key: {0}. InNodes = {1}, OutNodes count = {2}", kvp.Key, kvp.Value.inDegree, kvp.Value.outNodes.Count);
                foreach (string s in kvp.Value.outNodes)
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
            }

            // Add the Nodes which does not have InDegree to the minHeap
            Heap minHeap = new Heap();
            Heap minHeapPartB = new Heap();
            foreach (KeyValuePair<string, Node> kvp in adjList)
            {
                Node node = kvp.Value;
                if (node.inDegree == 0)
                {
                    minHeap.InsertElement(kvp.Key);
                    minHeapPartB.InsertElement(kvp.Key);
                }
            }

            while (!minHeap.IsEmpty())
            {
                string letter = minHeap.ExtractMin();
                topologicalOrder.Add(letter);
                Node node = adjList[letter];

                Console.WriteLine("Extracted {0}. The adj letters are:", letter);
                foreach (string s in node.outNodes)
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
                foreach (string s in node.outNodes)
                {
                    Node childNode = adjList[s];
                    childNode.inDegree -= 1;
                    if (childNode.inDegree == 0)
                    {
                        minHeap.InsertElement(s);
                    }
                }
            }

            for (int i = 0; i < topologicalOrder.Count; i++)
            {
                Console.Write(topologicalOrder[i]);
            }
            Console.WriteLine();

            // Part B
            List<int> resourcesTime = new List<int>(2);
            List<string> resourcesString = new List<string>(2);
            List<string> dest = new List<string>();
            Queue<string> queueOfStrings = new Queue<string>();
            int time = 0;
            for (int i = 0; i < 2; i++)
            {
                resourcesTime.Add(0);
                resourcesString.Add(null);
            }
            Console.WriteLine("Is minHeapPartB empty? {0}", minHeapPartB.IsEmpty());
            Console.WriteLine("Time\t 1 \t 2 \t 3 \t 4 \t 5");
            while (dest.Count != topologicalOrder.Count && time < 20)
            {
                Console.Write(time + "\t");
                //Console.WriteLine("resourcestring count = {0}", resourcesString.Count);
                for (int i = 0; i < resourcesString.Count; i++)
                {

                    if (resourcesString[i] == null)
                    {
                        //Console.WriteLine("resourcestring {0} is null", i);
                        if (!minHeapPartB.IsEmpty())
                        {
                            //Console.WriteLine("minHeapPartB is not empty");
                            string letter = minHeapPartB.ExtractMin();
                            dest.Add(letter);
                            queueOfStrings.Enqueue(letter);
                            resourcesString[i] = letter;
                            resourcesTime[i] = letter[0] - 'A';
                            //Console.WriteLine("resourcesString[{0}] = {1}, resourcesTime = {2}", i, resourcesString[i], resourcesTime[i]);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("resourcestring {0} is not null. Time is {1}", i, resourcesTime[i]);
                        resourcesTime[i]--;
                        if (resourcesTime[i] <= 0)
                        {
                            if (!minHeapPartB.IsEmpty())
                            {
                                // Console.WriteLine("minHeapPartB is not empty");
                                string letter = minHeapPartB.ExtractMin();
                                dest.Add(letter);
                                queueOfStrings.Enqueue(letter);
                                resourcesString[i] = letter;
                                resourcesTime[i] = letter[0] - 'A';
                            }
                            resourcesString[i] = null;
                        }
                    }
                    Console.Write(resourcesString[i] + "\t");

                    if (queueOfStrings.Count() != 0)
                    {
                        while (queueOfStrings.Count() != 0)
                        {
                            string queueFront = queueOfStrings.Dequeue();
                            Node node = adjListPartB[queueFront];
                            //Console.WriteLine("Extracted {0}. The adj letters are:", queueFront);
                            foreach (string s in node.outNodes)
                            {
                                //Console.Write(s + " ");
                            }
                            foreach (string s in node.outNodes)
                            {
                                Node childNode = adjListPartB[s];
                                childNode.inDegree -= 1;
                                //Console.WriteLine("string {0} indegree {1}", s, childNode.inDegree);
                                if (childNode.inDegree == 0)
                                {
                                    minHeapPartB.InsertElement(s);
                                }
                            }
                        }
                    }

                }
                time++;
                Console.WriteLine();
            }
        }
    }
}