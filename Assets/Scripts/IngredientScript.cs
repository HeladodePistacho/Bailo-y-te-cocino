using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum INGREDIENT_TYPE
{
    B_ROSA = 0,
    B_VERDE,
    B_MORADO,

    R_ARAÑA,
    R_ROJO,
    R_VERDE,

    T_OJOS,
    T_INTESTINOS,

    NUM_OF_INGREDIENTS
}

public class IngredientScript : MonoBehaviour
{
    public INGREDIENT_TYPE type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = FoodManagerClass.Instance.GetSpriteByIngredient(type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
