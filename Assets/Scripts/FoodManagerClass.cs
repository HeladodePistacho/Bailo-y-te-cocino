﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManagerClass : MonoBehaviour
{
    public Sprite[] ingredient_sprites;
    public RecipeClass[] recipes;

    // Start is called before the first frame update
    void Start()
    {
       // ingredient_sprites = new Sprite[(int)INGREDIENT_TYPE.NUM_OF_INGREDIENTS];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetSpriteByIngredient(INGREDIENT_TYPE type)
    {
        return ingredient_sprites[(int)type];
    }

    public RecipeClass GetRecipe()
    {
        return recipes[Random.Range(0, recipes.Length)];
    }
}
