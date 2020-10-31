﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum INGREDIENT_TYPE
{
    PAN_ARRIBA = 0,
    QUESO,
    CARNE,
    PAN_ABAJO,

    NUM_OF_INGREDIENTS
}

public class RecipeClass : MonoBehaviour
{
    public float offset = 2.0f;
    public float speed = 5.0f;
    public float time_to_expand = 2.0f;

    GameObject[] childs;

    //DEBUG
    bool tmp = false;
    int child_index = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        childs = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            childs[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tmp)
            ExpandChilds();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("MoveChildDown", child_index);
            ++child_index;
        }
    }

    void ExpandChilds()
    {
        for (int i = 1; i < childs.Length; ++i)
        {
            StartCoroutine("MoveChildsUp", i);
        }

        tmp = true;
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

        Debug.Log("init: " + init_pos_y + " final: " + final_pos);

        while (trans.position.y > final_pos)
        {
            float new_pos = Mathf.SmoothStep(init_pos_y, final_pos, current_time / time_to_expand);
            current_time += Time.deltaTime;

            trans.position = new Vector3(trans.position.x, new_pos);
            yield return null;
        }

        trans.position = new Vector3(trans.position.x, final_pos);
    }
}
