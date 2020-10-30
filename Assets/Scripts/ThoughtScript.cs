using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtScript : MonoBehaviour
{
    RecipeClass choosed_recipe;
    

    // Start is called before the first frame update
    void Start()
    {
        choosed_recipe = FoodManagerClass.Instance.GetRecipe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
