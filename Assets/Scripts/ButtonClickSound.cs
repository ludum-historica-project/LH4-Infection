using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    public SoundValue clickSound;

    // Start is called before the first frame update
    void Start()
    {
        if (!clickSound) Destroy(gameObject);
        else GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Director.GetManager<SoundManager>().PlaySound(clickSound);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
