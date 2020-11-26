using System;

namespace laba3
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] darr = new double[5, 5]; //d - double, arr - array = darr
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    darr[i, j] = i;
                }
            }
            MyMatrix ex = new MyMatrix(darr);
            Console.WriteLine(ex);
        }
    }
    public class MyMatrix
    {
        private double[,] matrix;

        public int Height
        {
            get => this.matrix.GetLength(0);
        }

        public int Width
        {
            get => this.matrix.GetLength(1);
        }

        public int GetHeight()
        {
            return Height;
        }

        public int GetWidth()
        {
            return Width;
        }

        public MyMatrix(MyMatrix inputM)
        {
            this.matrix = inputM.matrix;
        }

        public MyMatrix(double[,] inputM)
        {
            this.matrix = inputM;
        }

        public MyMatrix(double[][] inputD)
        {
            try
            {
                foreach (double[] arr in inputD)
                {
                    if (inputD.Length != arr.Length)
                    {
                        throw new Exception("Количество строк и столбцов не совпадает");
                    }
                }

                int size = inputD.Length;
                this.matrix = new double[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        this.matrix[i, j] = inputD[i][j];
                    }
                }
            }
            catch
            {
                Console.WriteLine("Матрица не четырехугольник");
            }
        }

        public MyMatrix(String[] text)
        {
            int size = text[0].Split(' ').Length;
            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (size != text[i].Split(' ').Length)
                    {
                        throw new Exception("Матрица имеет разное количество элементов");
                    }
                }
                matrix = new double[text.Length, size];
                for (int i = 0; i < Height; i++)
                {
                    String[] numbers = text[i].Split(' ');
                    for (int j = 0; j < Width; j++)
                    {
                        this.matrix[i, j] = Convert.ToDouble(numbers[j]);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Матрица имеет разное количество элементов");
            }
        }

        public MyMatrix(String inputString) : this(inputString.Split('\t'))
        {
        }

        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { this.matrix[x, y] = value; }
        }

        public double GetElement(int i, int j)
        {
            return this.matrix[i, j];
        }

        public void SetElement(int i, int j, double num)
        {
            this.matrix[i, j] = num;
        }

        public static MyMatrix operator *(MyMatrix matrix1, MyMatrix matrix2) //de-reference operator *
        {
            double[,] result = new double[matrix1.Height, matrix2.Width];
            try
            {
                if (matrix1.Width != matrix2.Height)
                {
                    throw new Exception("Ширина первой матрицы не равна высоте второй");
                }

                for (int i = 0; i < matrix1.Height; i++)
                {
                    for (int j = 0; j < matrix2.Width; j++)
                    {
                        result[i, j] = 0;
                        for (int glass = 0; glass < matrix1.Width; glass++)
                        {
                            result[i, j] += matrix1[i, glass] * matrix2[glass, j];
                        }
                    }
                }

                return new MyMatrix(result);
            }
            catch
            {
                Console.WriteLine("Матрица не умножается");
            }

            return new MyMatrix(result);
        }

        public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
        {
            double[,] result = new double[matrix1.Height, matrix2.Width];
            try
            {
                if (matrix1.Height != matrix2.Height && matrix1.Width != matrix2.Width)
                    throw new Exception("Высота или ширина первой матрицы не равны высоте или ширине второй");

                for (int i = 0; i < matrix1.Height; i++)
                {
                    for (int j = 0; j < matrix2.Width; j++)
                    {
                        result[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }

                return new MyMatrix(result);
            }
            catch
            {
                Console.WriteLine("Матрица не может прибавить");
            }

            return new MyMatrix(result);
        }

        protected double[,] GetTransponedArray()
        {
            double[,] result = new double[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    result[i, j] = this.matrix[j, i];
                }
            }

            return result;
        }

        public MyMatrix GetTransponedCopy() => new MyMatrix(this.GetTransponedArray());

        public void TransopnedMe() => this.matrix = GetTransponedArray();

        override public String ToString()
        {
            String text = "";
            if (this.matrix == null) return "Матрица пуста";
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        text += this.matrix[i, j] + "\t";
                    }

                    text += "\n";
                }
            }
            return text;
        }

    }
}
