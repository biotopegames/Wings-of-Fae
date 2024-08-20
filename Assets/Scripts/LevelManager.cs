using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
public int pixiesRequiredToComplete = 5;
private bool hasCompletedLevel = false;
public int whichLevelCompleted = 1;
public Animator treeAnim;



public void CompleteLevel()
{
    if(!hasCompletedLevel)
    {
        GameManager.Instance.MarkLevelCompleted(1);
        treeAnim.Play("open");
    }
    //Open tree gate
}


}
