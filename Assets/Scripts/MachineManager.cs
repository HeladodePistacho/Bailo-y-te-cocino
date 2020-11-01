using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineManager : MonoBehaviour
{
    GameObject recipe_to_do = null;
    GameObject recipe_to_drop = null;
    public GameObject recipe_pos = null;
    public GameObject recipe_pos_drop = null;

    INGREDIENT_TYPE[] ingredients;
    INGREDIENT_TYPE ingredient_index = (INGREDIENT_TYPE)0;

    public SpriteRenderer current_ingredient = null;
    public GameObject ruleta = null;

    public GameObject flecha = null;

    public float amount_up = 1.0f;
    public float morirse_speed = 5.0f;
    public GameObject mask = null;

    IEnumerator switch_coroutine;


    public GameObject[] lights;
    bool prev_strike = false;
    int strike = 0;
    int fase = 1;

    int num_cakes = 20;

    // Start is called before the first frame update
    void Start()
    {
       //Init la ruleta 
       ingredients = new INGREDIENT_TYPE[(int)INGREDIENT_TYPE.NUM_OF_INGREDIENTS];
       for(int i = 0; i < (int)INGREDIENT_TYPE.NUM_OF_INGREDIENTS; ++i)
        {
            ingredients[i] = (INGREDIENT_TYPE)i;
        }

        SelectRecipe();

        //DEBUG
        StartCoroutine(Switch());
        Debug.Log(mask.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        mask.transform.Translate(0.0f, -morirse_speed * Time.deltaTime, 0.0f);
        if(mask.transform.position.y <= -4.2f)
        {
            //Dead
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator Switch()
    {
        while (true)
        {
            SwitchIngredients();
            yield return new WaitForSeconds(0.4285625f);
        }
    }

    void SwitchIngredients()
    {
        //Set sprites
        ruleta.transform.Rotate(0.0f, 0.0f, -45.0f);
        
        ++ingredient_index;

        if (ingredient_index == INGREDIENT_TYPE.NUM_OF_INGREDIENTS)
            ingredient_index = (INGREDIENT_TYPE)0;

    }

    //Select Recipe
    void SelectRecipe()
    {
        recipe_to_do = Instantiate<GameObject>(FoodManagerClass.Instance.GetRecipe(), recipe_pos.transform.position, new Quaternion(0,0,0,1));
        recipe_to_do.GetComponent<RecipeClass>().ExpandChilds();
        flecha.transform.position = new Vector3(flecha.transform.position.x, recipe_to_do.GetComponent<RecipeClass>().GetCurrentPartPosY());

        recipe_to_drop = Instantiate<GameObject>(recipe_to_do, recipe_pos_drop.transform.position, new Quaternion(0, 0, 0, 1));
        recipe_to_drop.SetActive(false);
        recipe_to_drop.GetComponent<Rigidbody2D>().simulated = true;
    }

    public void CheckIngredient()
    {
        RecipeClass tmp_recipe = recipe_to_do.GetComponent<RecipeClass>();
        if (tmp_recipe.CheckIngredient(ingredient_index))
        {
            if (tmp_recipe.ChildDown())
            {
                //Recipe Finished
                StartCoroutine(DropRecipe());
                SoundManager.Instance.PlayFX(SoundManager.FX.RECIPE_COMPLETE);
                num_cakes--;

                if(num_cakes == 0)
                    SceneManager.LoadScene("MainMenu");
            }
            else
            {
                flecha.transform.position = new Vector3(flecha.transform.position.x, tmp_recipe.GetCurrentPartPosY());
                SoundManager.Instance.PlayFX(SoundManager.FX.PICK_INGREDIENT);
            }

            mask.transform.Translate(0.0f, amount_up, 0.0f);


            if (strike >= 3)
            {
                //Reset
                ApagarLuces();
                strike = 0;
            }
            lights[strike].SetActive(true);
            ++strike; 
            
            if(strike == 3)
            {
                ++fase;
                fase = Mathf.Clamp(fase, 0, 3);
                SoundManager.Instance.SetMusicStage(fase);
            }
        }
        else {

            mask.transform.Translate(0.0f, -amount_up/2, 0.0f);
            fase = 1;
            SoundManager.Instance.SetMusicStage(fase);
            ApagarLuces();

            SoundManager.Instance.PlayFX(SoundManager.FX.FAIL_INGREDIENT);
        } //ERROR CODE
    }

    IEnumerator DropRecipe()
    {
        //Wait until recipe is deleted
        while(recipe_to_do != null)
        {
            yield return null;
        }

        recipe_to_drop.SetActive(true);

        SelectRecipe();
    }

    void ApagarLuces()
    {
        foreach(GameObject light in lights)
        {
            light.SetActive(false);
        }
    }
}
