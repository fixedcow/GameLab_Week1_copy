using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStage : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables

    protected GameObject Platforms;

    #endregion

    #region PublicMethod

    public abstract void StartStage();

    #endregion

    #region PrivateMethod

    public void InitStage()
    {
        GameObject go = GameObject.Find("Platforms");

        Destroy(go);
    }

    #endregion


}
