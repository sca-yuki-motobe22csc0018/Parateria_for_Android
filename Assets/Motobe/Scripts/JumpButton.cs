using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class JumpButton : MonoBehaviour
{
    private Button button;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        
    }
}
