using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateTitle : MonoBehaviour
{

    public static LateTitle lateTitle;
    public GameObject titleCard;
    bool shown = false;

    // Start is called before the first frame update
    void Start()
    {
        lateTitle = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTitle() 
    {
        if(!shown)
        {
            shown = true;
            titleCard.SetActive(true);
        }
    }
}
