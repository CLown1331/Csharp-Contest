﻿namespace Library.Bitmap
{
    using System;
    using System.Collections;
    using System.Linq;

    public class Bitmap
    {
        private readonly BitArray bitArray;
        private readonly int[] dimensions;

        public Bitmap(params int[] dimensions)
        {
            this.dimensions = dimensions;
            this.bitArray = new BitArray(this.dimensions.Aggregate((s, n) => s * n));
        }

        public void Clear()
        {
            this.bitArray.SetAll(false);
        }

        public void SetValue(bool value, params int[] indexes)
        {
            this.ValidateIndexes(indexes);
            this.bitArray[this.GetIndex(indexes)] = value;
        }

        public bool GetValue(int[] indexes)
        {
            this.ValidateIndexes(indexes);
            return this.bitArray[this.GetIndex(indexes)];
        }

        public bool this[params int[] indexes]
        {
            get => this.GetValue(indexes);
            set => this.SetValue(value, indexes);
        }

        private void ValidateIndexes(int[] indexes)
        {
            if (indexes.Length != this.dimensions.Length)
            {
                throw new Exception("Number of indexes != _dimensions");
            }

            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] < 0 || indexes[i] >= this.dimensions[i])
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        private int GetIndex(int[] indexes)
        {
            int ret = 0;
            int power = 1;
            for (int i = 1; i <= indexes.Length; i++)
            {
                ret += power * indexes[^i];
                power *= this.dimensions[^i];
            }

            return ret;
        }
    }
}
