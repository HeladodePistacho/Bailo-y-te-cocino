using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    GameObject recipe_to_do = null;
    GameObject recipe_to_drop = null;
    public GameObject recipe_pos = null;
    public GameObject recipe_pos_drop = null;

    INGREDIENT_TYPE[] ingredients;
    INGREDIENT_TYPE ingredient_index = (INGREDIENT_TYPE)0;

    public SpriteRenderer current_ingredient = null;
    public SpriteRenderer next_ingredient = null;
    public SpriteRenderer prev_ingredient = null;


    IEnumerator switch_coroutine;

    // Start is called before the first frame update
    void Start()
    {
       //Init la ruleta 
       ingredients = new INGREDIENT_TYPE[(int)INGREDIENT_TYPE.NUM_OF_INGREDIENTS];
       for(int i = 0; i < (int)INGREDIENT_TYPE.NUM_OF_INGREDIENTS; ++i)
        {
            ingredients[i] = (INGREDIENT_TYPE)i;
        }

        current_ingredient.sprite = FoodManagerClass.Instance.GetSpriteByIngredient(ingredients[(int)ingredient_index]);
        next_ingredient.sprite = FoodManagerClass.Instance.GetSpriteByIngredient(ingredients[(int)(ingredient_index + 1)]);
        prev_ingredient.sprite = FoodManagerClass.Instance.GetSpriteByIngredient(ingredients[(int)(INGREDIENT_TYPE.NUM_OF_INGREDIENTS - 1)]);

        SelectRecipe();

        //DEBUG
        StartCoroutine(Switch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Switch()
    {
        while (true)
        {
            SwitchIngredients();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SwitchIngredients()
    {
        //Set sprites
        prev_ingredient.sprite = current_ingredient.sprite;
        current_ingredient.sprite = next_ingredient.sprite;
        ++ingredient_index;

        if (ingredient_index == INGREDIENT_TYPE.NUM_OF_INGREDIENTS)
            ingredient_index = (INGREDIENT_TYPE)0;

        if ((ingredient_index + 1) == INGREDIENT_TYPE.NUM_OF_INGREDIENTS)
        {
            next_ingredient.sprite = FoodManagerClass.Instance.GetSpriteByIngredient(ingredients[0]);
        }
        else next_ingredient.sprite = FoodManagerClass.Instance.GetSpriteByIngredient(ingredients[(int)ingredient_index + 1]);
    }

    //Select Recipe
    void SelectRecipe()
    {
        recipe_to_do = Instantiate<GameObject>(FoodManagerClass.Instance.GetRecipe(), recipe_pos.transform.position, new Quaternion(0,0,0,1));
        recipe_to_do.GetComponent<RecipeClass>().ExpandChilds();

        recipe_to_drop = Instantiate<GameObject>(FoodManagerClass.Instance.GetRecipe(), recipe_pos_drop.transform.position, new Quaternion(0, 0, 0, 1));
        recipe_to_drop.SetActive(false);
        recipe_to_drop.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void CheckIngredient()
    {
        RecipeClass tmp_recipe = recipe_to_do.GetComponent<RecipeClass>();
        if (tmp_recipe.CheckIngredient(ingredient_index))
        {
            if (tmp_recipe.ChildDown())
                StartCoroutine(DropRecipe());
        }
        else { Debug.Log("[BAAAD]"); } //ERROR CODE
    }

    IEnumerator DropRecipe()
    {
        //Wait until recipe is deleted
        while(recipe_to_do != null)
        {
            yield return null;
        }

        recipe_to_drop.SetActive(true);

    }
}
