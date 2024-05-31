using System;
using System.Linq;
using CSharpShellCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sort;

public static class Program
{
	static Stopwatch watch = new();
	public static void Main()
	{
		Random rand = new Random();
		int[] arr = new int[100].Select(_ => rand.Next(100)).ToArray();
		Console.WriteLine("未排序数组:");
		Print(arr);

		//冒泡排序
		BubbleSort(arr[..]);
		//选择排序
		SelectionSort(arr[..]);
		//插入排序
		InsertionSort(arr[..]);
		//快速排序
		QuickSort(arr[..], 0, arr.Length - 1);
		//归并排序
		MergeSort(arr[..], 0, arr.Length - 1);
	}

	public static void Print(int[] arr)
	{
		foreach (var item in arr) Console.Write(item + " ");
		Console.WriteLine("\n---------------------------------------------------");
	}

	public static void BubbleSort(int[] arr)
	{
		watch.Restart();
		for (int i = 0; i < arr.Length - 1; i++)
		{
			bool isSorted = true;
			for (int j = 0; j < arr.Length - i - 1; j++)
			{
				if (arr[j] > arr[j + 1])
				{
					(arr[j + 1], arr[j]) = (arr[j], arr[j + 1]);
					isSorted = false;
				}
			}
			if (isSorted) break;
		}
		watch.Stop();
		Console.WriteLine($"冒泡排序花费时间：{watch.Elapsed.TotalMilliseconds}ms");
		Print(arr);
	}

	public static void SelectionSort(int[] arr)
	{
		watch.Restart();
		for (int i = 0; i < arr.Length - 1; i++)
		{
			int minIndex = i;
			for (int j = i + 1; j < arr.Length; j++)
			{
				if (arr[minIndex] > arr[j])
				{
					minIndex = j;
				}
			}
			if (minIndex != i) (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
		}
		watch.Stop();
		Console.WriteLine($"选择排序花费时间：{watch.Elapsed.TotalMilliseconds}ms");
		Print(arr);
	}

	public static void InsertionSort(int[] arr)
	{
		watch.Restart();
		for (int i = 1; i < arr.Length; i++)
		{
			for (int j = 0; j < i; j++)
			{
				if (arr[i] < arr[j])
				{
					(arr[i], arr[j]) = (arr[j], arr[i]);
				}
			}
		}
		watch.Stop();
		Console.WriteLine($"插入排序花费时间：{watch.Elapsed.TotalMilliseconds}ms");
		Print(arr);
	}

	public static void QuickSort(int[] arr, int left, int right)
	{
		if (right - left == arr.Length - 1) watch.Restart();
		int l = left; int r = right; int flag = arr[left];
		while (l != r)
		{
			while (arr[r] >= flag && l != r) r--;
			while (arr[l] < flag && l != r) l++;
			if (l != r) (arr[l], arr[r]) = (arr[r], arr[l]);
		}
		if (right - left > 0)
		{
			QuickSort(arr, left, l);
			QuickSort(arr, r + 1, right);
		}
		if (right - left == arr.Length - 1)
		{
			watch.Stop();
			Console.WriteLine($"快速排序花费时间：{watch.Elapsed.TotalMilliseconds}ms");
			Print(arr);
		}
	}

	public static void MergeSort(int[] arr, int left, int right)
	{
		if (right - left == arr.Length - 1) watch.Restart();
		int[] temp = new int[right - left + 1];
		int l = left;
		int middle = (left + right) / 2;
		int r = middle + 1;
		if (right - left > 0)
		{
			MergeSort(arr, middle + 1, right);
			MergeSort(arr, l, middle);
		}
		for (int i = 0; i < temp.Length; i++)
		{
			if (l <= middle && r <= right) temp[i] = arr[l] < arr[r] ? arr[l++] : arr[r++];
			else temp[i] = l > middle ? arr[r++] : arr[l++];
		}
		for (int i = 0; i < temp.Length; i++) arr[left + i] = temp[i];
		if (right - left == arr.Length - 1)
		{
			watch.Stop();
			Console.WriteLine($"归并排序花费时间：{watch.Elapsed.TotalMilliseconds}ms");
			Print(arr);
		}
	}
}
