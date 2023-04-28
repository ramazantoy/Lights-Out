using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Jobs
{
    [BurstCompile]
    public struct FilterJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> Values;//Filtreleme işleminin hızlı olması için orjinal dizi elamanlarının okunması
        
        [ReadOnly] public NativeArray<int> Mask; //Maskelenmiş dizi
        
        [WriteOnly] public NativeArray<int> Result;//Filltreleme işlemi sonunda oluşacak dizi

        public void Execute(int index)
        {
            Result[index] = Values[index] * Mask[index];//Naskeleme işleminde maske dizisinin elemanları 1 veya 0 değeri aldığı için aynı indexler çarpılarak filtreleme yapılır.
        }
    }
}