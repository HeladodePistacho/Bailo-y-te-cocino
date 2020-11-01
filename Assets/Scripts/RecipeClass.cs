using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeClass : MonoBehaviour
{
    public float offset = 2.0f;
    public float speed = 5.0f;
    public float time_to_expand = 2.0f;

    GameObject[] childs;
    int current_ingredient = 0;

    //DEBUG
    bool tmp = false;
    int child_index = 0;

    private void Awake()
    {
        childs = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            childs[i] = transform.GetChild(i).gameObject;
        }
    }

    public void InitRecipe()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //if (Input.GetKeyDown(KeyCode.Space))
       //{
       //    StartCoroutine("MoveChildDown", child_index);
       //    ++child_index;
       //}
    }

    public void ExpandChilds()
    {
        for (int i = 1; i < childs.Length; ++i)
        {
            StartCoroutine(MoveChildsUp(i));
        }

        tmp = true;
    }

    public bool ChildDown()
    {
        StartCoroutine(MoveChildDown(child_index));
        ++child_index;

        if (child_index == childs.Length)
            return true;
        else return false;
    }

    IEnumerator MoveChildsUp(int index_child)
    {
        Transform trans = childs[index_child].transform;

        float init_pos_y = trans.position.y;
        float final_pos = (init_pos_y + (offset * index_child));
        float current_time = 0.0f;

        while (trans.position.y < final_pos)
        {
            float new_pos = Mathf.SmoothStep(init_pos_y, final_pos, current_time / time_to_expand);
            current_time += Time.deltaTime;

            trans.position = new Vector3(trans.position.x, new_pos);
            yield return null;
        }

        trans.position = new Vector3(trans.position.x, final_pos);
    }

    IEnumerator MoveChildDown(int index_child)
    {
        Transform trans = childs[index_child].transform;

        float init_pos_y = trans.position.y;
        float final_pos = (init_pos_y - (offset * index_child));
        float current_time = 0.0f;

        while (trans.position.y > final_pos)
        {
            float new_pos = Mathf.SmoothStep(init_pos_y, final_pos, current_time / time_to_expand);
            current_time += Time.deltaTime;

            trans.position = new Vector3(trans.position.x, new_pos);
            yield return null;
        }

        trans.position = new Vector3(trans.position.x, final_pos);

        for(int i = 0; i <= index_child; ++i)
            childs[i].GetComponent<BounceAnimation>().StartSquashFunc();

        //Check if last child
        if (index_child == childs.Length - 1)
            StartCoroutine(Blink());

    }

    public bool CheckIngredient(INGREDIENT_TYPE type)
    {
        if (current_ingredient >= childs.Length)
            return false;

        if (childs[current_ingredient].GetComponent<IngredientScript>().type == type)
        {
            ++current_ingredient;
            return true;
        }
        else return false;
    }

    IEnumerator Blink()
    {
        for(int i = 0; i < 5; ++i)
        {
            foreach (GameObject child in childs)
                child.GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(0.1f);

            foreach (GameObject child in childs)
                child.GetComponent<SpriteRenderer>().enabled = true;

            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
