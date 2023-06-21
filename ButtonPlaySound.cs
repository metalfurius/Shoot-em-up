using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaySound : MonoBehaviour
{
    public void play1(){
    FindObjectOfType<AudioManager>().Play("Click");
    }
}
