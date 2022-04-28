using UnityEngine;

[CreateAssetMenu(fileName = "GachaDatabase", menuName = "ScriptableObjects/GachaDatabase")]
public class GachaDatabase : ScriptableObject
{
    // A list of units that you can win in a gacha
    // with their respective weights
    // declare these in inspector!!

    public Unit[] units;
    public int[] weights;


    // Cumulative Weight, used for randomly selecting a unit

    private int[] cumulative_weight;


    // Update cumulative weight

    public void UpdateCumulativeWeight(){
        cumulative_weight = new int[weights.Length];
        cumulative_weight[0] = weights[0];

        for (int i = 1; i < weights.Length; ++i){
            cumulative_weight[i] = cumulative_weight[i - 1] + weights[i];
        }


    }

    // select a random unit from the Gacha
    public Unit PullUnit(){

        UpdateCumulativeWeight();
        // When you pray to the gacha gods... this is it
        int random = Random.Range(1,cumulative_weight[cumulative_weight.Length - 1] + 1);
        Debug.Log(random);

        // binary search the random number to see what unit you get
        int index = BinarySearch(cumulative_weight, random);

        return units[index];

    }

    // binary search for pull unit. 
    private static int BinarySearch(int[] arr, int val){
        if (val <= arr[0]){
            return 0;
        }
        if (val > arr[arr.Length - 1]){
            return arr.Length - 1;
        }
        return BinarySearch(arr, 0, arr.Length - 1, val);
    }

    private static int BinarySearch(int[] arr, int l, int r, int val){
        int mid = l + (r - l) / 2;

        if (val == arr[mid]){
            return mid;
        }

        if (r - l <= 1){
            return r;
        }

        if (val < arr[mid]){
            return BinarySearch(arr, l, mid, val);
        }else{
            return BinarySearch(arr, mid, r, val);
        }
    }

}
