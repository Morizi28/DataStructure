using System;

namespace Project1
{
    class Node
    {
        public int Data;
        public Node Link;

        public Node(int data = -1, Node link = null)
        {
            Data = data;
            Link = link;
        }
    }

    class LinkList
    {
        public Node First;

        public LinkList()
        {
            First = new Node();
            First.Link = First;
        }

        public void Insert(int x)
        {
            Node node = new Node(x);
            Node p = First;

            while (p.Link != First) // Peyda kardan akharin node
                p = p.Link;

            p.Link = node;
            node.Link = First; // Halqavi kardan
        }

        public bool IsEmpty()
        {
            if (First.Link == First)
                return true;
            else
                return false;
        }

        public void Print()
        {
            if (IsEmpty())
                Console.WriteLine("List Empty");
            else
            {
                Node p = First.Link;
                while (p != First)
                {
                    Console.Write(p.Data + " ");
                    p = p.Link;
                }

                Console.WriteLine(); // khat jadid
            }
        }

        public void Merge(LinkList a, LinkList b)
        {
            Node p = a.First.Link;
            Node q = b.First.Link;
            Node last = First;

            while (p != a.First && q != b.First)
            {
                if (p.Data <= q.Data)
                {
                    last.Link = p;
                    last = p;
                    p = p.Link;
                }
                else
                {
                    last.Link = q;
                    last = q;
                    q = q.Link;
                }
            }

            if (p != a.First)
            {
                last.Link = p;
                while (p.Link != a.First)
                    p = p.Link;
                p.Link = First;
            }
            else if (q != b.First)
            {
                last.Link = q;
                while (q.Link != b.First)
                    q = q.Link;
                q.Link = First;
            }
        }

        public void DeletePrime()
        {
            Node p = First.Link;
            Node q = First;
            while (p != First)
            {
                if (IsPrime(p.Data))
                    q.Link = p.Link;
                else
                    q = p;

                p = p.Link;
            }
        }

        bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number % 2 == 0) return true;

            int limit = number / 2;
            for (int i = 2; i <= limit; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }

    public class Stack
    {
        private LinkList list;

        public Stack()
        {
            list = new LinkList();
        }

        public void Push(int x)
        {
            list.Insert(x);
        }

        public int Pop()
        {
            if (list.IsEmpty())
            {
                Console.WriteLine("Stack empty");
                return -1;
            }
            else
            {
                Node p = list.First.Link;
                Node q = list.First;

                while (p.Link != list.First)
                {
                    q = p;
                    p = p.Link;
                }

                q.Link = p.Link;
                return p.Data;
            }
        }
    }

    public class Queue
    {
        private LinkList list;

        public Queue()
        {
            list = new LinkList();
        }

        public void Add(int x)
        {
            list.Insert(x);
        }

        public int Delete()
        {
            if (list.IsEmpty())
            {
                Console.WriteLine("Queue empty");
                return -1;
            }
            else
            {
                Node first = list.First;
                Node p = first.Link;
                first.Link = p.Link;
                return p.Data;
            }
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var a = new LinkList();
            var b = new LinkList();
            for (int i = 0; i < 20; i += 2)
            {
                a.Insert(i);
                b.Insert(i + 1);
            }

            b.Insert(21);
            b.Insert(22);
            b.Insert(23);

            a.Print();
            b.Print();
            var c = new LinkList();
            c.Merge(a, b);
            c.Print();

            Console.ReadKey();
        }
    }
}