using System;
using System.Collections.Generic;
using UnityEngine;


// DO NOT USE THIS RAW. OR IT WILL NOT DISPLAY IN INSPECTOR. 
public class InvList<T> : IEnumerator<T> where T : Data{
    private int position = -1;
    public int length {get; private set;}

    protected T[] arr;

    public InvList(int len){
        length = len;
    }

    // i hope the lack of deep / shallow copy distinction does not cause bugs here
    public static InvList<T> LoadFrom(T[] new_array, int len){
        InvList<T> new_list = new InvList<T>(len);
        new_list.arr = new_array;
        return new_list;
    }


    //note. I HAVE NO IDEA if this works, or if it will throw a nullreference once this InvList is destroyed.
    public T[] SaveTo(){
        return arr;
    }

    public bool Add(T obj, int index){
        if (index < length && arr[index] == null){
            arr[index] = obj;
            return true;
        }else{
            return false; // failed to add unit
        }    
    }

    public bool Add(T obj){
        for (int i = 0; i < length; ++i){
            if (arr[i] == null){
                arr[i] = obj;
                return true;
            }
        }
        return false;
    }

    public void ClearAll(){
        for (int i = 0; i < length; ++i){
            arr[i] = null;
        }
    }

    public void Swap(int index_a, int index_b){
        (arr[index_a], arr[index_b]) = (arr[index_b], arr[index_a]);
    }

    public bool Remove(int index){
        if (index < length){
            arr[index] = default(T);
            return true;
        }else{
            return false;
        }
    }

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