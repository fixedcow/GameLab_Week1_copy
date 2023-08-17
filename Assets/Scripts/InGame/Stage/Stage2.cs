using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : BaseStage
{
    
    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    public override void StartStage()
    {
        base.InitStage();
        Platforms = new GameObject { name = "Platforms" };

        MakePlatform();
    }

    void MakePlatform()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Stage2");
        GameObject go = Instantiate(prefab);

        //for (int i = 0; i < go.transform.childCount; i++)
        //{
        //    transform.GetChild(i).gameObject.SetActive(true);
        //}

        go.transform.SetParent(Platforms.transform);
    }

   

    #endregion


}
