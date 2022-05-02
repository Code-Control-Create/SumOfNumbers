using System;

namespace test_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");

            var pp = new Plus(10);
            int[] inputs = {1, 1};

            int v = pp.put(inputs);

            Console.WriteLine($"{v}");
        }
    }

    class Plus
    {
        int _base = 0;  // 进制数
        int weight_base = 0;  // 所有进制数的和

        public Plus(int _base_=10)
        {
            this._base = _base_;

            _base_ --;

            do{
                this.weight_base += _base_;
                _base_ --;
            }while(_base_ > 0);

        }

        /*
        基本思路：
                将一个N位数看成一个N维向量，则满足条件的数在向量空间中的分布具有相当明显的几何或代数特征，
            以此将对整个空间求解的问题转化为从N到N+1维向量的数学归纳问题
        */

        public int put(int[] input){
            int count = 0; // N维解向量的量,即N维向量空间中满足要求的向量（数）的个数，初始为0维
            int body = 0;  // N维的空间和, 即N维向量空间所有向量（数）的代数和，初始为0维
            int all = 0;   // N维解向量的和，即N维向量空间中满足要求的向量（数）的代数和，初始为0维

            int power = 1; // 下一维向量空间中最高维的权，初始为0维，

            foreach(int i in input){
                /*N到N+1时，N+1阶
                总和 =                满足前N维的和           +          仅满足N+1维的数的和       -       同时满足的部分
                        前N维的升阶和  +  第N+1维的权和                第N+1维的权和 + 前N维的空间和
                    N维和*进制数    N+1的权*进制和*N维解向量的量||N+1的坐标*N+1的权*N的空间数    前N维空间和||N+1的坐标*N+1的权*N维解空间的量   N维和
                */
                all = (all*this._base + power*this.weight_base*count) + (i*power*power + body) - (i*power*count + all);

                //    进制和*N+1维的权*N维的空间数      +  N维的空间和*进制数
                body = this.weight_base*power*power + body*this._base;

                //    N+1维的空间数 + N维解向量的量*(进制数 - 1) //重合一次
                count = power + count*(this._base - 1);

                //    N+2维的权
                power *= this._base;
            }

            return all;
        }
    }
}
