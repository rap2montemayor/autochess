using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DO NOT USE THIS RAW. OR IT WILL NOT DISPLAY IN INSPECTOR.
/// </summary>
/// <typeparam name="T">Unit or Item</typeparam>
public class InvList<T> : IEnumerator<T> where T : Data{
    private int position = -1;
    public int length {get; private set;}

    protected T[] arr;

    /// <summary>
    /// Constructor for InvList<T>. 
    /// </summary>
    /// <param name="len">Number of elements</param>
    public InvList(int len){
        length = len;
    }

    /// <summary>
    /// Loads data from new_array.
    /// 
    /// i hope the lack of deep / shallow copy distinction does not cause bugs
    /// here.
    /// </summary>
    /// <param name="new_array">Array to load from</param>
    /// <param name="len">Length of array</param>
    /// <returns>A copy of the array</returns>
    public static InvList<T> LoadFrom(T[] new_array, int len){
        InvList<T> new_list = new InvList<T>(len);
        new_list.arr = new_array;
        return new_list;
    }

    /// <summary>
    /// note. I HAVE NO IDEA if this works, or if it will throw a nullreference
    /// once this InvList is destroyed.
    /// </summary>
    /// <returns>Returns the internal array</returns>
    public T[] SaveTo(){
        return arr;
    }

    /// <summary>
    /// Add an object to the list, with index specified. Will fail if the index
    /// is already occupied or if it's greater than the length of the list.
    /// </summary>
    /// <param name="obj">Object to be added</param>
    /// <param name="index">Index to place the new object in</param>
    /// <returns>True if the operation succeeded. False otherwise.</returns>
    public bool Add(T obj, int index){
        if (index < length && arr[index] == null){
            arr[index] = obj;
            return true;
        }else{
            return false; // failed to add unit
        }    
    }

    /// <summary>
    /// Add an object to the list. The object will be assigned to the first
    /// non-null index. Fails if all indices are occupied.
    /// </summary>
    /// <param name="obj">Object to be added</param>
    /// <returns>True if the operation succeeded. False otherwise.</returns>
    public bool Add(T obj){
        for (int i = 0; i < length; ++i){
            if (arr[i] == null){
                arr[i] = obj;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Swaps the elements in the specified indices
    /// </summary>
    /// <param name="index_a"></param>
    /// <param name="index_b"></param>
    public void Swap(int index_a, int index_b){
        (arr[index_a], arr[index_b]) = (arr[index_b], arr[index_a]);
    }

    /// <summary>
    /// Remove the element at the specified index. Fails if index is invalid.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>True if the operation succeeded. False otherwise.</returns>
    public bool Remove(int index){
        if (index < length){
            arr[index] = default(T);
            return true;
        }else{
            return false;
        }
    }

    /// <summary>
    /// Returns the element at the specified index. Basically an accessor for
    /// the internal list.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T At(int index){
        return arr[index];
    }
/*
    public InvList<Data> ToData(){
        
    }
*/
    // IENUMERATOR FUNCTIONS

    public IEnumerator<T> GetEnumerator(){
        return (IEnumerator<T>)this;
    }

    public bool MoveNext(){
        position++;
        return position < length;
    }

    public void Reset(){
        position = -1;
    }

    public void Dispose(){}

    T IEnumerator<T>.Current{
        get { return (T)arr[position]; }
    }

    public object Current{
        get { return (object)arr[position]; }
    }
}