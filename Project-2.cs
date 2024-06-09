using System;

namespace Project2
{
    public class Stack
    {
        public int Top;
        public int MaxSize;
        public string[] stack;

        public Stack(int maxsize)
        {
            MaxSize = maxsize;
            stack = new string[MaxSize];
            Top = -1;
        }

        public bool IsFull()
        {
            if (Top == MaxSize - 1)
                return true;
            return false;
        }

        public bool IsEmpty()
        {
            if (Top == -1)
                return true;
            return false;
        }

        public void Add(string x)
        {
            if (IsFull())
                Console.WriteLine("Stack full");
            else
                stack[++Top] = x;
        }

        public string Delete()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty.");
                return "0";
            }

            return stack[Top--];
        }

        public void Display()
        {
            for (Top = 0; Top != MaxSize; Top++)
            {
                Console.Write(stack[Top]);
            }
        }

        public string Reverse(string x)
        {
            string a = "";
            for (int i = 0; i < x.Length; i++)
            {
                char c = x[i];
                a = c + a;
            }

            return a;
        }
    }

    public class Program
    {
        public static int GetOption()
        {
            Console.WriteLine(
                "1. infix to postfix.\n2. infix to prefix.\n3. postfix to infix.\n4. prefix to infix.\n5. postfix to prefix.\n6. prefix to postfix.\n7. finish.");
            Console.Write("~ ");
            int menu = Convert.ToInt32(Console.ReadLine());
            return menu;
        }

        public static int Operator(char x) // in stack so "(" is the lowest.
        {
            if (x == '(')
                return 0;
            if (x == '+' || x == '-')
                return 1;
            if (x == '*' || x == '/')
                return 2;

            return 3;
        }

        static void Main(string[] args)
        {
            try
            {
                Stack s = new Stack(50);
                int k = GetOption();
                while (k != 7)
                {
                    switch (k)
                    {
                        case 1:
                            InfixPostfix(s);
                            break;
                        case 2:
                            InfixPrefix(s);
                            break;
                        case 3:
                            PostfixInfix(s);
                            break;
                        case 4:
                            PrefixInfix(s);
                            break;
                        case 5:
                            PostfixPrefix(s);
                            break;
                        case 6:
                            PrefixPostfix(s);
                            break;
                    }

                    Console.WriteLine();
                    k = GetOption();
                }

                Console.WriteLine("Done ...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void PrefixPostfix(Stack s)
        {
            string a;
            string b;
            Console.Write("prefix: ");
            string x = Console.ReadLine();
            Console.Write("postfix: ");
            for (int i = x.Length - 1; i >= 0; i--)
            {
                if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' ||
                    x[i] >= '1' && x[i] <= '9')
                    s.Add(Convert.ToString((x[i])));
                else
                {
                    a = s.Delete();
                    b = s.Delete();
                    string c = ($"({a}{x[i]}{b})");
                    s.Add(c);
                }
            }

            string w = s.Delete();
            for (int i = 0; i < w.Length; i++)
            {
                if ((w[i] >= 'a' && w[i] <= 'z') || w[i] >= 'A' && w[i] <= 'Z' ||
                    w[i] >= '1' && w[i] <= '9')
                    Console.Write(w[i]);
                else if (w[i] == ' ')
                {
                    i++;
                }
                else if (w[i] == '(')
                    s.Add(Convert.ToString((w[i])));
                else if (w[i] == ')')
                {
                    while ((a = s.Delete()) != "(")
                        Console.Write(a);
                }
                else
                {
                    if (s.Top != -1 && Operator(Convert.ToChar(s.stack[s.Top])) >= Operator(w[i]))
                        Console.Write(s.Delete());
                    s.Add(Convert.ToString((w[i])));
                }
            }

            while (s.Top != -1)
                Console.Write(s.Delete());
            Console.WriteLine();
        }

        private static void PostfixPrefix(Stack s)
        {
            string a;
            string b;
            Console.Write("Postfix: ");
            string x = Console.ReadLine();
            Console.Write("Prefix: ");
            for (int i = 0; i < x.Length; i++)
            {
                if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' ||
                    x[i] >= '1' && x[i] <= '9')
                    s.Add(Convert.ToString((x[i])));
                else
                {
                    a = s.Delete();
                    b = s.Delete();
                    string c = ($"({b}{x[i]}{a})");
                    s.Add(c);
                }
            }

            string w = s.Delete();
            string o = "";
            string y = s.Reverse(w);
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] == ')')
                {
                    y = y.Remove(i, 1);
                    y = y.Insert(i, "(");
                }
                else if (y[i] == '(')
                {
                    y = y.Remove(i, 1);
                    y = y.Insert(i, ")");
                }
            }

            for (int i = 0; i < y.Length; i++)
            {
                if ((y[i] >= 'a' && y[i] <= 'z') || y[i] >= 'A' && y[i] <= 'Z' ||
                    y[i] >= '1' && y[i] <= '9')
                    o += y[i];
                else if (y[i] == ' ')
                {
                    i++;
                    continue;
                }
                else if (y[i] == '(')
                    s.Add(Convert.ToString((y[i])));
                else if (y[i] == ')')
                {
                    while ((a = s.Delete()) != "(") // stack is LIFO
                        o += a;
                }
                else
                {
                    if (s.Top != -1 && Operator(Convert.ToChar(s.stack[s.Top])) >= Operator(y[i]))
                        o += s.Delete();
                    s.Add(Convert.ToString(y[i]));
                }
            }

            while (s.Top != -1)
                o += s.Delete();
            o = s.Reverse(o);
            Console.WriteLine(o);
        }

        private static void PrefixInfix(Stack s)
        {
            string a;
            string b;
            Console.Write("Prefix: ");
            string x = Console.ReadLine();
            Console.Write("Infix: ");
            for (int i = x.Length - 1; i >= 0; i--)
            {
                if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' ||
                    x[i] >= '1' && x[i] <= '9')
                    s.Add(Convert.ToString((x[i])));
                else
                {
                    a = s.Delete(); //first one.
                    b = s.Delete(); //seconde one.
                    string c = ($"({a}{x[i]}{b})");
                    s.Add(c);
                }
            }

            while (s.Top != -1)
                Console.WriteLine(s.Delete());
        }

        private static void PostfixInfix(Stack s)
        {
            string a;
            string b;
            Console.Write("PostFix: ");
            string x = Console.ReadLine();
            Console.Write("Infix: ");
            for (int i = 0; i < x.Length; i++)
            {
                if ((x[i] >= 'a' && x[i] <= 'z') || x[i] >= 'A' && x[i] <= 'Z' ||
                    x[i] >= '1' && x[i] <= '9')
                    s.Add(Convert.ToString((x[i])));
                else
                {
                    a = s.Delete(); //first one.
                    b = s.Delete(); //seconde one.
                    string c = ($"({b}{x[i]}{a})");
                    s.Add(c);
                }
            }

            while (s.Top != -1)
                Console.WriteLine(s.Delete());
        }

        private static void InfixPrefix(Stack s)
        {
            string o = "";
            string a;
            Console.Write("Infix: ");
            string x = Console.ReadLine();
            Console.Write("Prefix: ");
            string y = s.Reverse(x);
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] == ')')
                {
                    y = y.Remove(i, 1);
                    y = y.Insert(i, "(");
                }
                else if (y[i] == '(')
                {
                    y = y.Remove(i, 1);
                    y = y.Insert(i, ")");
                }
            }

            for (int i = 0; i < y.Length; i++)
            {
                if ((y[i] >= 'a' && y[i] <= 'z') || y[i] >= 'A' && y[i] <= 'Z' ||
                    y[i] >= '1' && y[i] <= '9')
                    o += y[i];
                else if (y[i] == ' ')
                {
                    i++;
                }
                else if (y[i] == '(')
                    s.Add(Convert.ToString((y[i])));
                else if (y[i] == ')')
                {
                    while ((a = s.Delete()) != "(") // stack is LIFO
                        o += a;
                }
                else
                {
                    if (s.Top != -1 && Operator(Convert.ToChar(s.stack[s.Top])) >= Operator(y[i]))
                        o += s.Delete();
                    s.Add(Convert.ToString(y[i]));
                }
            }

            while (s.Top != -1)
                o += s.Delete();
            o = s.Reverse(o);
            Console.WriteLine(o);
        }

        private static void InfixPostfix(Stack s)
        {
            string a;
            Console.Write("Infix: ");
            string infix = Console.ReadLine();
            Console.Write("Postfix: ");
            for (int i = 0; i < infix.Length; i++)
            {
                if ((infix[i] >= 'a' && infix[i] <= 'z') || infix[i] >= 'A' && infix[i] <= 'Z' ||
                    infix[i] >= '1' && infix[i] <= '9')
                    Console.Write(infix[i]);
                else if (infix[i] == ' ')
                {
                    i++;
                }
                else if (infix[i] == '(')
                    s.Add(Convert.ToString((infix[i])));
                else if (infix[i] == ')')
                {
                    while ((a = s.Delete()) != "(") // stack is LIFO
                        Console.Write(a);
                }
                else
                {
                    if (s.Top != -1 && Operator(Convert.ToChar(s.stack[s.Top])) >= Operator(infix[i]))
                        Console.Write(s.Delete());
                    s.Add(Convert.ToString(infix[i]));
                }
            }

            while (s.Top != -1)
                Console.Write(s.Delete());
            Console.WriteLine();
        }
    }
}