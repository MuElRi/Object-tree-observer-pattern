using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping_and_saving
{
	class Vector<T>: VectorObserved
	{
		private T[] arr;
		private int size;
		private int capacity;
		public Vector() 
		{
			arr = null;
		}
		public Vector(int _size)
		{
			int d = _size / 10 * 2 + 3;
			arr = new T[_size + d];
			size = _size;
			capacity = _size + d;
		}
		public void reserve(int _size)
		{
			int d = _size / 10 * 2 + 3;
			arr = new T[_size + d];
			size = 0;
			capacity = _size + d;
		}

		public void push_back(Vector<T> elem)
        {
			if (arr == null)
			{
				size = elem.Size();
				int d = size % 10 * 2 + 3;
				capacity = size + d;
				arr = new T[capacity];
				for (int i = 0; i < elem.Size(); i++)
					arr[i] = elem[i];
				size = elem.Size();
			}
			else
			{
				if (size + elem.Size() >= capacity)
				{
					int _size = size;
					size += elem.Size();
					int d = size % 10 * 2 + 3;
					capacity = size + d;
					T[] _arr = new T[capacity];
					for (int i = 0; i < _size; i++)
						_arr[i] = arr[i];
					for (int i = 0; i < elem.Size(); i++)
						_arr[_size + i] = elem[i];
					arr = _arr;
				}
				else
				{
					for (int i = 0; i < elem.Size(); i++)
						arr[size + i] = elem[i];
					size+=elem.Size();
				}

			}

		}
		public void push_back(T elem)
		{
			if (arr == null)
			{
				arr = new T[3];
				arr[0] = elem;
				size = 1;
				capacity = 3;
			}
			else
			{
				if (size == capacity)
				{
					size++;
					int d = size % 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 0; i < size - 1; i++)
						_arr[i] = arr[i];
					_arr[size - 1] = elem;
					arr = _arr;
					capacity = size + d;
				}
				else
				{
					arr[size] = elem;
					size++;
				}
			}
		}
		public void push_back(ref T elem)
		{
			if (arr == null)
			{
				arr = new T[3];
				arr[0] = elem;
				size = 1;
				capacity = 3;
			}
			else
			{
				if (size == capacity)
				{
					size++;
					int d = size % 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 0; i < size - 1; i++)
						_arr[i] = arr[i];
					_arr[size - 1] = elem;
					arr = _arr;
					capacity = size + d;
				}
				else
				{
					arr[size] = elem;
					size++;
				}
			}
		}
		public void pop_back()
		{
			if (arr != null && size > 0)
			{
				if (size < capacity / 3 && size > 20)
				{
					size--;
					int d = size % 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 0; i < size; i++)
						_arr[i] = arr[i];
					arr = _arr;
					capacity = size + d;
				}
				else size--;
			}
		}
		public void insert(ref T elem, int index)
		{
			if (arr == null)
			{
				arr = new T[3];
				arr[0] = elem;
				size = 1;
				capacity = 3;
			}
			else if (size == 0)
			{
				arr[0] = elem;
				size++;
			}
			else
			{
				if (index > size)
					index = size;
				if (index < 0)
					index = 0;
				if (size == capacity)
				{
					size++;
					int d = size / 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 0, k = 0; i < size - 1; i++, k++)
					{
						if (i == index)
						{
							_arr[k] = elem;
							k++;
						}
						_arr[k] = arr[i];
					}
					arr = _arr;
					capacity = size + d;
				}
				else
				{
					for (int i = size - 1; i >= index; i--)
						arr[i + 1] = arr[i];
					arr[index] = elem;
					size++;
				}
			}
		}
		public void push_front(ref T elem)
		{
			if (arr == null)
			{
				arr = new T[3];
				arr[0] = elem;
				size = 1;
				capacity = 3;
			}
			else
			{
				if (size == capacity)
				{
					size++;
					int d = size / 10 * 2 + 3;
					T[] _arr = new T[size + d];
					_arr[0] = elem;
					for (int i = 0; i < size - 1; i++)
						_arr[i + 1] = arr[i];
					arr = _arr;
					capacity = size + d;
				}
				else
				{
					for (int i = size - 1; i >= 0; i--)
						arr[i + 1] = arr[i];
					size++;
					arr[0] = elem;
				}
			}
		}
		public void pop_front()
		{
			if (arr != null && size > 0)
			{
				if (size < (capacity / 3) && size > 20)
				{
					size--;
					int d = size / 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 1; i < size + 1; i++)
						_arr[i - 1] = arr[i];
					arr = _arr;
					capacity = size + d;
				}
				else
				{
					for (int i = 1; i < size; i++)
						arr[i - 1] = arr[i];
					size--;
				}
			}
		}
		public void pop(int index)
		{
			if (arr != null && size > 0)
			{
				 if (size < (capacity / 3) && size > 20)
				{
					size--;
					int d = size / 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 0, k = 0; i < size + 1; i++)
						if (i != index)
						{
							_arr[k] = arr[i];
							k++;
						}
					arr = _arr;
					capacity = size + d;
				}
				else
				{
					for (int i = index + 1; i < size; i++)
						arr[i - 1] = arr[i];
					size--;
				}
			}
		}
		public T this[int index]
		{
			get { return arr[index]; }
			set { arr[index] = value; }
		}
		public T remove(int index)
		{
			if (arr != null && size > 0)
			{
				if (size < (capacity / 3) && size > 20)
				{
					size--;
					int d = size / 10 * 2 + 3;
					T[] _arr = new T[size + d];
					for (int i = 0, k = 0; i < size + 1; i++)
						if (i != index)
						{
							_arr[k] = arr[i];
							k++;
						}
					T r_elem = arr[index];
					arr = _arr;
					capacity = size + d;
					return r_elem;
				}
				else
				{
					T r_elem = arr[index];
					for (int i = index + 1; i < size; i++)
						arr[i - 1] = arr[i];
					size--;
					return r_elem;
				}
			}
			else
			{
				T r_elem = default(T);
				return r_elem;
			}
		}
		public void clear()
		{
			arr = null;
			size = 0;
			capacity = 0;
		}
		public int Size()
		{
			return size;
		}
		public int Capacity()
		{
			return capacity;
		}
		public bool empty()
		{
			if (arr == null || size == 0)
				return true;
			else return false;
		}
		public T front()
		{
			if (arr != null && size != 0)
				return arr[0];
			else
			{
				return default(T);
			}
		}
		public T back()
		{
			if (size > 0)
				return arr[size - 1];
			else
				return default(T);
		}
	}
}
