using System;

// 5.1 c# part, oh you probably know because it is a .cs file :)

int[] xs = { 3, 5, 12 };
int[] ys = { 2, 3, 4, 7 };

static int[] merge(int[] xs, int[] ys) {
    int[] ls = new int[xs.Length + ys.Length];
    int j = 0, k = 0;
    for (int i = 0; i < ls.Length; i++)
    {
        if (j >= xs.Length) ls[i] = ys[k];
        else if (k >= ys.Length) ls[i] = xs[j];
        else if (xs[j] < ys[k]) {
            ls[i] = xs[j];
            j++;
        }
        else {
            ls[i] = ys[k];
            k++;
        }
    }
    return ls;
}
Array.ForEach(merge(xs, ys), e=> Console.Write($"{e} "));