using Common.Structures;

float[,] f = {
    { 1, 2, 3 },
    { 0, 4, 0 }
};

float[,] s =
{
    { 0, 1, 3, 2 },
    { 2, 4, 0, 1 },
    { 4, 3, 1, 2 }
};

Matrix first = new Matrix(f);
Matrix second = new Matrix(s);

var a = first * second;
for (int i = 0; i < a.Size.X; i++)
{
    for (int j = 0; j < a.Size.Y; j++)
    {
        Console.Write(a[i,j] + " ");
    }

    Console.WriteLine();
}
