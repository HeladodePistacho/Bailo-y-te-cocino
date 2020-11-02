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
    public GameObject mask_pasteles = null;

    IEnumerator switch_coroutine;


    public GameObject[] lights;
    bool prev_strike = false;
    int strike = 0;
    int fase = 1;

    int num_cakes = 20;


    //Ruleta

    public GameObject ruleta_fake;
    private IEnumerator cor;

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
        cor = LaRuletaGira();
        StartCoroutine(Switch());
        
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
            Quaternion init_q = ruleta.transform.rotation;
            Quaternion final_q = ruleta_fake.transform.rotation;

            Debug.Log(final_q);

            float current_time = 0.0f;

            while (Quaternion.Angle(ruleta.transform.rotation, final_q) > 5.0f)
            {
                float anle = Quaternion.Angle(ruleta.transform.rotation, final_q);
                // Debug.Log(anle);

                Quaternion new_rot = Quaternion.Slerp(init_q, final_q, current_time / 0.2f);
                current_time += Time.deltaTime;

                ruleta.transform.rotation = new_rot;

                yield return null;
            }

            ruleta.transform.rotation = final_q;
            Debug.Log("Giro");

            ++ingredient_index;

            if (ingredient_index == INGREDIENT_TYPE.NUM_OF_INGREDIENTS)
                ingredient_index = (INGREDIENT_TYPE)0;

            yield return new WaitForSeconds(SoundManager.Instance.beat.clip.length - 0.2f);
        }
    }

    void SwitchIngredients()
    {
        //Set sprites
        // ruleta.transform.Rotate(0.0f, 0.0f, -45.0f);

        //StopCoroutine(cor);
        StartCoroutine(cor);
        
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
                mask_pasteles.transform.Translate(-0.205f, 0.0f, 0.0f);

                if (num_cakes == 0)
                    SceneManager.LoadScene("MainMenu");
            }
            else
            {
                flecha.transform.position = new Vector3(flecha.transform.position.x, tmp_recipe.GetCurrentPartPosY());
                SoundManager.Instance.PlayFX(SoundManager.FX.PICK_INGREDIENT);
            }

            if((mask.transform.localPosition.y + amount_up) < 2.8f)
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

    IEnumerator LaRuletaGira()
    {
        Quaternion init_q = ruleta.transform.rotation;
        Quaternion final_q = ruleta_fake.transform.rotation;

        Debug.Log(final_q);

        float current_time = 0.0f;

        while (Quaternion.Angle(ruleta.transform.rotation, final_q) > 5.0f)
        {
            float anle = Quaternion.Angle(ruleta.transform.rotation, final_q);
           // Debug.Log(anle);

            Quaternion new_rot = Quaternion.Slerp(init_q, final_q, current_time / 0.2f);
            current_time += Time.deltaTime;

            ruleta.transform.rotation = new_rot;
        
            yield return null;
        }

        ruleta.transform.rotation = final_q;
        Debug.Log("Giro");
    }
}
