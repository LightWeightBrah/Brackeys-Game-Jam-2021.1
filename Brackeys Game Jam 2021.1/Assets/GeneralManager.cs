using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour
{
    public Image currentPlayerIcon;

    public void SwitchPlayerIcon(Sprite newPlayerIcon)
    {
        currentPlayerIcon.sprite = newPlayerIcon;
    }
}
