using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Serialization;

/*
 *
 *
 * Given a NativeArray<int> of size N filled with positive integers, the class below filters the values which are powers of two.
 * The operation is asynchronous and when the operation is completed and the result is ready, the caller is notified through the callback given.
 *
 * The operation is divided into two steps:
 * 
 * The first step is to generate a mask array of size N. The value at an index is 1 if the corresponding value in the source array is a power of two, otherwise 0.
 * 
 * The second step is to filter the values in the source array using the mask generated in the first step.
 * The resulting data should contain the original value if it is a power of two, otherwise 0.
 *
 * The jobs can take as long is they need. The operation should not hang the main thread by waiting for jobs to complete.
 * Burst compilation should be used on the jobs.
 * The operations inside the jobs should be as optimized as possible. Relevant attributes should be used to maximize performance
 * The original array should not be modified.
 * When the result array is ready, invoke the given callback with the result.
 *
 * Note that there is a dependency between the outputs and inputs of the two jobs.
 *
 * Implement the class below and the described jobs, according to the constraints given above.
 * 
 * 
 */

namespace Jobs
{
    public class JobChain : MonoBehaviour
    {

        [SerializeField]
        private bool _useRandomValues; //Rastgele dizi oluşturulmak istenilirse
        
        [Header("Array Member Settings")] //Rastgele dizi için ayarlar
        
        [Range(0,99999)]
        [SerializeField]
        private int _minValue;
        
        [Range(0,99999)]
        [SerializeField]
        private int _maxValue;

        [Header("Array Length Settings")] 
        
        [SerializeField]
        [Range(1,99999)]
        private int _minLength;
        [Range(1,500)]
        [SerializeField]
        private int _maxLength;
        
        //Rastgele yerine elle dizi girilmek istenilirse
        [SerializeField] 
        private int[] _values;

        
        
        private delegate void OnFilterCompleteDelegate(NativeArray<int> nativeArray); // İşlem sonunda işlemin delegate yardımıyla çağırılması için delegate tanımlanması
        
        private OnFilterCompleteDelegate _onFilterCompleteDelegate;// Delagete değişkeni

        private void Start()
        {
          _onFilterCompleteDelegate +=OnFilterCompleted;//Filter completedfonsiyonun eklenmesi
          
          StartJobs(); 
        }

        private void StartJobs()
        {
            //jobArray'ın oluşturulması
            
            NativeArray<int> jobArray;
            if (!_useRandomValues)
            {
             jobArray = new NativeArray<int>(_values.Length, Allocator.TempJob); // Editörden girilen values dizisi uzunluğunda NativeArray tipinde jobArray'ın oluşturulması

                for (int i = 0; i < _values.Length; i++) // Oluşturulan jobArray'ın değişkenlerinin atanması
                {
                    jobArray[i] =_values[i];
                }

            }
            else
            {
                int arrayLength = UnityEngine.Random.Range(_minLength, _maxLength + 1); 
                
                jobArray = new NativeArray<int>(arrayLength, Allocator.TempJob); //Girilen değerler aralığında rastgele uzunlukta dizi oluşturulması
                
                for (int i = 0; i < arrayLength; i++) // Oluşturulan jobArray'ın değişkenlerinin atanması
                {
                    jobArray[i] = UnityEngine.Random.Range(_minLength, _maxLength); //girilen aralıkta rastgele elemanların girilmesi
                }
            }
           
         
            FilterPowerOfTwoValues(jobArray,_onFilterCompleteDelegate.Invoke); // jobArray'da iki ve ikinin üssüne eşit olan değerlerle yeni bir dizi oluşturulması

           jobArray.Dispose(); //jobArray dizisinin hafızadan silinmesi
        }

        private void OnFilterCompleted(NativeArray<int> result)
        {
            Debug.Log("Filtered Array");
            
            ShowArray(result);//result dizisinin içeriğinin gösterilmesi
      
            result.Dispose(); // result dizisinin hafızadan silinmesi
           
        }

        private void ShowArray(NativeArray<int> nativeArray) //Dizilerin editörde gösterilmesi
        {
            string temp = "";

            for (int i = 0; i < nativeArray.Length-1; i++)
            {
                temp +=nativeArray[i] + ",";
            }

            temp += nativeArray[^1];
        

            Debug.Log(temp);

        }
        
        private void FilterPowerOfTwoValues(NativeArray<int> nativeArray, Action<NativeArray<int>> onCompleted)
        {
            Debug.Log("Original Array");
            
            ShowArray(nativeArray);//Orjinal array'ın işlemden önce gösterilmesi
            
            NativeArray<int> mask = new NativeArray<int>(nativeArray.Length, Allocator.TempJob); //maskeleme için array oluşturulması

            MaskJob maskJob = new MaskJob(); //Maskleme işlemini gerçekleştirecek olan işin oluşturulması
            maskJob.Values = nativeArray;
            maskJob.Mask = mask;
          
            
            JobHandle maskJobHandle = maskJob.Schedule(nativeArray.Length, 64); //Maskeleme işleminin diğer işlerden bağımsız şekilde her framede max 64 olacak şekilde ayarlanması
            
            //Yukarıdaki satır native arrayın her bir index'i için sürekli çağırılır.
            
           maskJobHandle.Complete();//Maskeleme işlemi bittikten sonra aşağıya inilmesi
            
            Debug.Log("Mask Array");
            
            ShowArray(mask);
            
            NativeArray<int> result = new NativeArray<int>(nativeArray.Length, Allocator.TempJob);

            FilterJob filterJob = new FilterJob(); //Filtreleme işleminin oluşturulması
            
            filterJob.Values = nativeArray;
            filterJob.Mask = mask;
            filterJob.Result = result;
       
            
            JobHandle filterJobHandle = filterJob.Schedule(nativeArray.Length, 64, maskJobHandle); //filtreleme işleminin gerçekleşmesi yalnız bu işlem maskeleme işlemi bittikten sonra çalışıcak

            filterJobHandle.Complete();//Filtreleme işlemi bittikten sonra aşağıya inilmesi
            
            mask.Dispose(); //mask dizisinin hafızadan silinmesi
            
            onCompleted?.Invoke(result);//elde edilen  result dizisinin delegate  ile invoke edilmesi 
            
        }


    }
}


