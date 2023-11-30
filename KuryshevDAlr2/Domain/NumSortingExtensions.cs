namespace KuryshevDAlr2.Domain
{
    public static class NumSortingExtensions
    {
        #region Сортировка выбором
        
        /// <summary>
        /// Сортировка выбором
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void SelectionSort(this int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[min])
                        min = j;
                }
                
                int temp = array[min];
                array[min] = array[i];
                array[i] = temp;
            }
        }

        #endregion

        #region Сортировка вставками

        /// <summary>
        /// Сортировка вставками
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void SwapSort(this int[] array)
        {
            int x;
            int j;
            for (int i = 1; i < array.Length; i++)
            {
                x = array[i];
                j = i;
                while (j > 0 && array[j - 1] > x)
                {
                    array.Swap(j, j - 1);
                    j -= 1;
                }
                array[j] = x;
            }
        }

        #endregion

        #region Сортировка пузырьком

        /// <summary>
        /// Сортировка пузырьком
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void BubbleSort(this int[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                for (var j = 0; j < array.Length - i; j++)
                {
                    if (array[j] > array[j + 1])
                        array.Swap(j, j + 1);
                }
            }
        }

        #endregion

        #region Сортировка слиянием

        /// <summary>
        /// Сортировка слиянием
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void MergeSort(this int[] array)
        {
            array.MergeSort(0, array.Length - 1);
        }

        /// <summary>
        /// Сортировка слиянием в диапазоне
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <param name="lowIndex">Индекс начального элемента для сортировки в массиве</param>
        /// <param name="highIndex">Индекс последнего элемента для сортировки в массиве</param>
        static void MergeSort(this int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                array.MergeSort(lowIndex, middleIndex);
                array.MergeSort(middleIndex + 1, highIndex);
                array.Merge(lowIndex, middleIndex, highIndex);
            }
        }

        #endregion

        #region Быстрая сортировка

        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void QuickSort(this int[] array)
        {
            array.QuickSort(0, array.Length - 1);
        }

        /// <summary>
        /// Получение индекса опорного элемента
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <param name="minIndex">Индекс начального элемента для сортировки в массиве</param>
        /// <param name="maxIndex">Индекс последнего элемента для сортировки в массиве</param>
        static int Partition(this int[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    array.Swap(pivot, i);
                }
            }

            pivot++;
            array.Swap(pivot, maxIndex);
            return pivot;
        }

        /// <summary>
        /// Быстрая сортировка в диапазоне
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <param name="minIndex">Индекс начального элемента для сортировки в массиве</param>
        /// <param name="maxIndex">Индекс последнего элемента для сортировки в массиве</param>
        static void QuickSort(this int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
                return;

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);
        }

        #endregion

        #region Сортировка Шелла

        /// <summary>
        /// Сортировка Шелла
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void ShellSort(this int[] array)
        {
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        array.Swap(j, j - d);
                        j = j - d;
                    }
                }

                d = d / 2;
            }
        }

        #endregion

        #region Пирамидальная сортировка

        /// <summary>
        /// Пирамидальная сортировка
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void HeapSort(this int[] array)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                array.Heapify(n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                array.Heapify(i, 0);
            }
        }

        /// <summary>
        /// Преобразования в двоичную кучу поддерева
        /// </summary>
        /// <param name="array">Куча</param>
        /// <param name="n">Размер кучи</param>
        /// <param name="i">Корневым узел</param>
        static void Heapify(this int[] array, int n, int i)
        {
            int largest = i;

            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && array[l] > array[largest])
                largest = l;

            if (r < n && array[r] > array[largest])
                largest = r;

            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                array.Heapify(n, largest);
            }
        }

        #endregion

        #region TimSort

        /// <summary>
        /// TimSort
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void TimSort(this int[] array)
        {
            array.TimSort(array.Length);
        }

        /// <summary>
        /// TimSort с ограничением
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <param name="size">Размер массива</param>
        static void TimSort(this int[] array, int size)
        {
            int middle = 0;
            int last = 0;
            int run = 32;

            for (int j = 0; j < 10; j++)
            {
                var clonedArray = (int[])array.Clone();
                try
                {
                    for (int i = 0; i < size; i += run)
                        clonedArray.InsertionSort(i, CompareMinElement((i + 31), (size)));

                    for (int i = run; i < size; i = 2 * i)
                    {
                        for (int start = 0; start < size; start += 2 * i)
                        {
                            middle = start + i - 1;
                            last = CompareMinElement((start + 2 * i - 1), (size));
                            clonedArray.MergeElements(start, last, middle);
                        }
                    }
                }
                catch
                {
                    run *= 2;
                }
                array = clonedArray;
            }
        }

        /// <summary>
        /// Сортировка вставками с ограничением
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        /// <param name="start">Нижний предел</param>
        /// <param name="last">Верхний предел</param>
        static void InsertionSort(this int[] array, int start, int last)
        {
            int i = 0;
            int j = 0;
            for (i = start; i < last - 1; ++i)
            {
                j = i + 1;
                while (j > start && array[j - 1] > array[j])
                {
                    array.Swap(j, j - 1);
                    j--;
                }
            }
        }

        /// <summary>
        /// Сравнение элементов, на минимальность
        /// </summary>
        /// <param name="first">Первый элемент</param>
        /// <param name="second">Второй элемент</param>
        static int CompareMinElement(int first, int second)
        {
            if (first < second)
                return first;

            return second;
        }

        static void MergeElements(this int[] array, int front, int tail, int middle)
        {
            int s1 = (middle - front) + 1;

            int s2 = tail - middle;
            int[] first_subarray = new int[s1];
            int[] second_subarray = new int[s2];

            int i = 0;
            int j = 0;
            int counter = 0;

            for (i = 0; i < s1; i++)
            {
                first_subarray[i] = array[front + i];
            }
            for (i = 0; i < s2; i++)
            {
                second_subarray[i] = array[middle + i + 1];
            }
            i = 0;

            while (counter < s1 + s2)
            {
                if (i < s1 && j < s2)
                {
                    if (first_subarray[i] <= second_subarray[j])
                    {
                        array[front + counter] = first_subarray[i];
                        i++;
                    }
                    else
                    {
                        array[front + counter] = second_subarray[j];
                        j++;
                    }
                }
                else if (i < s1)
                {
                    array[front + counter] = first_subarray[i];
                    i++;
                }
                else
                {
                    array[front + counter] = second_subarray[j];
                    j++;
                }
                counter++;
            }
        }

        #endregion

        #region IntroSort

        /// <summary>
        /// IntroSort
        /// </summary>
        /// <param name="array">Массив для сортировки</param>
        public static void IntroSort(this int[] array)
        {
            int partitionSize = array.Partition(0, array.Length - 1);

            if (partitionSize < 16)
            {
                array.SwapSort();
            }
            else if (partitionSize > (2 * Math.Log(array.Length)))
            {
                array.HeapSort();
            }
            else
            {
                array.QuickSort(0, array.Length - 1);
            }
        }

        #endregion

        /// <summary>
        /// Перестановка элементов по индексу
        /// </summary>
        /// <param name="array">Массив в котором перестанавливаем элементы</param>
        /// <param name="i">Индекс целевого элемента</param>
        /// <param name="j">Индекс сменяемого элемента</param>
        static void Swap(this int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }


        /// <summary>
        /// Слияние подмассивов в массиве
        /// </summary>
        /// <param name="array">Основной массив</param>
        /// <param name="lowIndex">Индекс первого подмассив</param>
        /// <param name="middleIndex">Индекс середины основного массива, делящий на подмассивы</param>
        /// <param name="highIndex">Индекс конца основного массива и второго подмассива</param>
        static void Merge(this int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            var left = lowIndex;
            var right = middleIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }
    }
}
