using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Jobs
{
    [BurstCompile]
    public struct MaskJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> Values;
        [WriteOnly] public NativeArray<int> Mask;
        
        public void Execute(int index)//Maskeleme işlemi için 2'nin üssü olan sayılar yerine 1 olmayanlar yerine 0 yazılması
        {
            int value = Values[index];

            if (IsPowerOfTwo(value))
            {
                Mask[index] = 1;
            }
            else
            {
                Mask[index] = 0;
            }
        }
        
        private  bool IsPowerOfTwo(int number)
        {
            /*
             *  İnternet üzerinden elde ettiğim bu fonksiyon kendisine parametre olarak verilen sayının 2'nin üssü olup olmadığını bulur.
             * Bu işlemi yaparken number'ın bir eksiği ve number'ın kendisinin binary karşılığını And'leyerek sonuç 0 ise bu sayı 2'nin üssüdür.
             * 1000 -> 8'in binary karşılığı , 0111 -> 7'nin binary karşılığı
             * Binary karşılıkları ve ifadesinde eğer 2'nin üssü bir sayı ise her zaman 0 çıkıyor.
             */
            return (number > 0) && ((number & (number - 1)) == 0);
        }
    }
}
