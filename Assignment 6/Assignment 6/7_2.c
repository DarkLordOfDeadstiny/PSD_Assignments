void main(int n) {
    int arr[4];
    arr[0] = 7;
    arr[1] = 13;
    arr[2] = 9;
    arr[3] = 8;

    int sum;
    arrsum(4, arr, &sum);
    print sum;
    println;

    int sum2;
    int arr2[20];
    squares(n, arr2);
    arrsum(n , arr2, &sum2);
    print sum2;
    println;

    int arr3[7];
    arr3[0] = 1;
    arr3[1] = 2;
    arr3[2] = 1;
    arr3[3] = 1;
    arr3[4] = 1;
    arr3[5] = 2;
    arr3[6] = 0;

    int freq[4];
    histogram(7, arr3, 4, freq);
    print freq[0];
    print freq[1];
    print freq[2];
    print freq[3];
    println;
}

void arrsum(int n, int arr[], int *sum) {
    int i;
    *sum = 0;
    i = 0;
    while (i < n) {
        *sum = *sum + arr[i];
        i = i + 1;
    }
}

void squares(int n, int arr[]) {
    int i;
    i = 0;
    while (i < n) {
        arr[i] = i * i;
        i = i + 1;
    }
}

void histogram(int n, int ns[], int max, int freq[]) {
    int i;
    i = 0;
    while (i < max) {
        freq[i] = 0;
        i = i + 1;
    }
    i = 0;
    while (i < n) {
        freq[ns[i]] = freq[ns[i]] + 1;
        i = i + 1;
    }
}
