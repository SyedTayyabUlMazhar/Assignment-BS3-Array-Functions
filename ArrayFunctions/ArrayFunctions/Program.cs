using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            //MyArrayList<int> myArrayList1 = new MyArrayList<int>(5);
            //myArrayList1.Add(5);
            //myArrayList1.Add(23);
            //myArrayList1.Add(-9);
            //myArrayList1.Sort();
            //myArrayList1.Traverse();

            //Console.WriteLine(myArrayList1.BinarySearch(23));


            Menu.Select(0);
            Menu.Show();





        }
    }

    class MyArrayList<T> where T : IComparable<T>
    {
        public T[] array;
        public int Length;
        int Next = 0;
        int Last = -1;

        public static MyArrayList<int> AddArray()
        {
            MyArrayList<int> arrayList;

            Console.Write("Enter array size : ");
            int size = int.Parse(Console.ReadLine());

            arrayList = new MyArrayList<int>(size);

            Console.WriteLine("Enter elements, each on a new line. Type 'stop' and press enter to stop.");
            string input;
            while (!arrayList.IsFull())
            {
                input = Console.ReadLine();
                if (input.Equals("stop")) break;

                arrayList.Add(int.Parse(input));
            }

            return arrayList;
        }

        public MyArrayList(int length)
        {
            Length = length;
            array = new T[Length];
        }

        public void Add(T item)
        {
            if (Next != Length)
            {
                array[Next] = item;
                Next++;
                Last++;
            }
        }
        
        //public T Get(int index)
        //{
        //    if (index <= Last) return array[index];
        //    else return null;
        //}

        public bool IsFull()
        {
            return Next==Length;
        }

        public void Traverse()
        {
            for (int i = 0; i <= Last; i++)
                Console.Write(array[i] + " ");
            Console.WriteLine();
        }

        public void Insert(T item, int index)
        {
            //if (index < Length && !IsFull() && index <= Last)
            if (!IsFull() && index >= 0 && index <=Last)
            {
                int i;
                for (i = Next; i > index; i--)
                    array[i] = array[i - 1];
                array[i] = item;


                Last++;
                Next++;
            }

            
        }

        public void Delete(int index)
        {
            for (int i = index; i < Last; i++)
            {
                array[i] = array[i + 1];
            }
            Last--;
            Next--;
        }

        public void Delete(T item)
        {
            int index = Search(item);

            Delete(index);
        }

        public int Search(T item)
        {

            int index = -1;
            for (int i = 0; i <= Last; i++)
            {
                if (array[i].Equals(item))
                {
                    index = i; break;
                }
            }

            return index;
        }

        public int BinarySearch(T item)
        {
            
            int index = -1;

            int mid, start, end;
            start = 0;
            end = Last;

            while(start <= end)
            {
                mid = (start + end) / 2;
                if (array[mid].CompareTo(item) < 0)
                    start = mid + 1;
                else if (array[mid].CompareTo(item) > 0)
                    end = mid - 1;
                else
                {
                    index = mid; break;
                }
            }

            return index;
        }

        public void Sort()
        {
            for (int i = 1; i <= Last; i++)
            {
                T key = array[i];
                int j = i - 1;

                while (j != -1 && key.CompareTo(array[j]) < 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static MyArrayList<T> Merge(MyArrayList<T> arrayList1, MyArrayList<T> arrayList2)
        {
            MyArrayList<T> mergedList = new MyArrayList<T>(arrayList1.Length + arrayList2.Length);
            int j=0, k=0;
            
            
            while(j <= arrayList1.Last && k <=arrayList2.Last)
            {
                if (arrayList1.array[j].CompareTo(arrayList2.array[k]) <= 0)
                {
                    mergedList.Add(arrayList1.array[j]);
                    j++;
                }
                else
                {
                    mergedList.Add(arrayList2.array[k]);
                    k++;
                }
            }

            if(j > arrayList1.Last)
            {
                while(k <= arrayList2.Last)
                {
                    mergedList.Add(arrayList2.array[k]);
                    k++;
                }
            }
            else
            {
                while (j <= arrayList1.Last)
                {
                    mergedList.Add(arrayList1.array[j]);
                    j++;
                }
            }



            return mergedList;
        }


    }

    static class Menu
    {
        static MyArrayList<int> arrayList;
        public enum MenuOption { AddArray, Traverse, Insert, Delete, Sort, Search, BinarySearch, Merge }

        public static void Show()
        {
            Console.Clear();
                        
            Console.WriteLine("1- Traverse");
            Console.WriteLine("2- Insert");
            Console.WriteLine("3- Delete");
            Console.WriteLine("4- Sort");
            Console.WriteLine("5- Search");
            Console.WriteLine("6- Binary Search");
            Console.WriteLine("7- Merge");

            Menu.Select(int.Parse(Console.ReadLine()));
        }

        public static void MenuOrExit()
        {
            Console.WriteLine("Type 'menu' to return to main menu, or type 'exit' to exit.");
            string menuOrExit = "";
            menuOrExit = Console.ReadLine();
            if (menuOrExit.Equals("menu"))
            {
                Menu.Show();
            }
            else if (menuOrExit.Equals("exit"))
            {
                Environment.Exit(0);
            }
        }

        public static void Select(int option)
        {
            int index, item;
            switch((MenuOption)option)
            {
                case MenuOption.AddArray : arrayList = MyArrayList<int>.AddArray();
                    break;

                case MenuOption.Traverse : arrayList.Traverse();

                    Menu.MenuOrExit();
                    break;

                case MenuOption.Insert:
                    Console.Write("Enter index : ");
                    index = int.Parse(Console.ReadLine());
                    Console.Write("\nEnter element : ");
                    item = int.Parse(Console.ReadLine());
                    arrayList.Insert(item, index);

                    Menu.MenuOrExit();
                    break;

                case MenuOption.Delete:
                    Console.Write("Enter index : ");
                    index = int.Parse(Console.ReadLine());
                    arrayList.Delete(index);

                    Menu.MenuOrExit();
                    break;

                case MenuOption.Sort: arrayList.Sort();

                    Menu.MenuOrExit();
                    break;

                case MenuOption.Search:
                    Console.Write("Enter item : ");
                    item = int.Parse(Console.ReadLine());
                    arrayList.Search(item);

                    Menu.MenuOrExit();
                    break;

                case MenuOption.BinarySearch:
                    Console.Write("Enter item : ");
                    item = int.Parse(Console.ReadLine());
                    arrayList.BinarySearch(item);

                    Menu.MenuOrExit();
                    break;

                case MenuOption.Merge:
                    MyArrayList<int> arrayList1;
                    Console.WriteLine("Enter second array to merge with : ");
                    Console.Write("Enter array size : ");
                    int size = int.Parse(Console.ReadLine());

                    arrayList1 = new MyArrayList<int>(size);

                    Console.WriteLine("Enter elements, each on a new line.");
                    string input = Console.ReadLine();
                    while (!arrayList1.IsFull())
                    {
                        arrayList1.Add(int.Parse(input));
                        input = Console.ReadLine();
                    }
                    arrayList = MyArrayList<int>.Merge(arrayList, arrayList1);

                    Menu.MenuOrExit();
                    break;
            }
        }
    }
}
