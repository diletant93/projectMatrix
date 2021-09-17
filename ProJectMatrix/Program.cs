using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] array = new int[3][];
            array[0] = new int[11] { 3, 4, 5, 6, 7, 14, 18, 19, 20, 24, 25 };// A
            array[1] = new int[11] { 1, 2, 3, 4, 5, 6, 7, 14, 16, 21, 22 };// B
            array[2] = new int[13] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 16, 17, 23, 24 };// C
            int[] universal = new int[25];

            for (int i = 0; i < universal.Length; i++)
            {
                universal[i] = i + 1;
            }

            Console.WriteLine("B addition:\n");
            int[] Badd = Addition(array[1], universal);//B
            ShowArray(Badd);
            Console.WriteLine();

            Console.WriteLine("A accociation B(addition):\n");
            int[] A_acc_B_add = Accociation(Badd, array[0]);
            ShowArray(A_acc_B_add);

            Console.WriteLine("A addition:\n");
            int[] A_add = Addition(array[0], universal);
            ShowArray(A_add);

            Console.WriteLine("B addition twice:\n");
            int[] B_addTwice = Addition(Addition(array[1], universal), universal);
            ShowArray(B_addTwice);

            Console.WriteLine("A(addition) accociation B(double twice):\n");
            int[] A_add_acc_B_addTwice = Accociation(A_add, B_addTwice);
            ShowArray(A_add_acc_B_addTwice);


            Console.WriteLine("(A accociation B(addition)) difference (A(addition) accociation B(double twice)):\n");
            int[] A_acc_B_add_diff_A_add_acc_B_addTwice = Difference(A_acc_B_add, A_add_acc_B_addTwice);
            ShowArray(A_acc_B_add_diff_A_add_acc_B_addTwice);

            Console.WriteLine("C addition:\n");
            int[] C_add = Addition(array[2], universal);
            ShowArray(C_add);

            Console.WriteLine("((A accociation B(addition)) difference (A(addition) accociation B(double twice))) difference C(addition):\n ");
            int[] A_acc_B_add_diff_A_add_acc_B_addTwice_diff_C_add = Difference(A_acc_B_add_diff_A_add_acc_B_addTwice, C_add);
            ShowArray(A_acc_B_add_diff_A_add_acc_B_addTwice_diff_C_add);

        }
        static int[] Difference(int[] array1, int[] array2)//різниця
        {
            int[] result = array1[..];
            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array2.Length; j++)
                {
                    if (array1[i] == array2[j])
                    {
                        DelByValue(ref result, array2[j]);
                    }
                }
            }

            return result;
        }
        static int[] Decussation(int[] array1, int[] array2)//переріз
        {//1234
            //2345
            //
            int[] result = null;
            int amount = 0;
            for (int i = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array2.Length; j++)
                {
                    if (array1[i] == array2[j])
                    {
                        amount++;
                    }
                }
            }
            bool check = false;
            result = new int[amount];
            for (int i = 0, k = 0; i < array1.Length; i++)
            {
                for (int j = 0; j < array2.Length; j++)
                {
                    if (array1[i] == array2[j])
                    {
                        result[k] = array1[i];
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    k++;
                    check = false;
                }
            }
            return result;
        }
        static int[] Accociation(int[] array1, int[] array2)//обьєднання
        {
            int[] result = new int[(array1.Length + array2.Length)];
            array1.CopyTo(result, 0);
            array2.CopyTo(result, array1.Length);
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    else if (result[i] == result[j])
                    {
                        DelByValue(ref result, result[i]);
                        break;
                    }
                }
            }
            Array.Sort(result);
            return result;
        }
        static int[] Addition(int[] array, int[] universal)//доповнення до множини
        {
            int[] result = universal[..];//створюємо масив , в якого копіюємо універсум 
            for (int i = 0; i < array.Length; i++)//, який потім після операцій над ним стане доповненням множини
            {
                DelByValue(ref result, array[i]);
            }
            return result;//повертаємо уже готове доповнення до множини
        }
        static void DelByValue(ref int[] array, int value)
        {
            if (array == null || Array.FindIndex(array, 0, array.Length, x => x == value) == -1)//дивимось валідність даних що приходять у ф-цію
            {
                Console.WriteLine("Smth went wrong with DelByValue function, please check input values!\n");
            }
            else
            {
                int[] arr = new int[array.Length - 1];//створюємо новий масив , котрий буде резульатом даної ф-ції
                bool check = false;//створюємо чек для провірки чи знайшли ми вже потрібний елемент для видалення чи ні
                for (int i = 0; i < arr.Length; i++)
                {
                    if (array[i] == value && check == false)//якщо елем = значенню і ще до цього ми його не видаляли то виконується тіло if
                    {
                        arr[i] = array[i + 1];

                        check = true;
                        continue;
                    }
                    else if (check == true)//якщо чек = тру , тобто ми уже видалили елем , то присвоєння в масив буде елементами + 1 індексів
                    {
                        arr[i] = array[i + 1];
                        continue;
                    }
                    arr[i] = array[i];//поки не виконались два if ми просто копіюємо масив
                }
                array = arr;
            }
        }
        static bool isNull(int[] array)
        {
            bool ifNull = array == null ? true : false;
            return ifNull;
        }
        static void ShowArray(int[] array)//ф-ція просто виводить масив
        {
            if (isNull(array))
            {
                Console.WriteLine("Array = 0\n");
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"Elem {(i + 1)} = {array[i]}\t");
                    if (i % 4 == 0 && i != 0)
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
