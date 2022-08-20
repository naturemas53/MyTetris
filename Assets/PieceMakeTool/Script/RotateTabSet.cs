using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CommonDefines;

public class RotateTabSet : MonoBehaviour
{
    Toggle[] tabs;

    // Start is called before the first frame update
    void Start()
    {
        tabs = GetComponentsInChildren<Toggle>();
        tabs[0].isOn = true;

        List<RotateTab> tabScripts = new List<RotateTab>();
        tabScripts.Clear();
        for( int i = 0; i < tabs.Length; ++i )
        {
            tabs[i].GetComponentInChildren<Text>().text = (90 * i).ToString();
            var tabScript = tabs[i].gameObject.AddComponent<RotateTab>();
            tabScripts.Add( tabScript );
        }

        StartCoroutine( CoLateInitTabScripts( tabScripts ) );
    }

    IEnumerator CoLateInitTabScripts( List< RotateTab > tabScripts )
    {
        yield return new WaitForEndOfFrame();

        for( int i = 0; i < tabScripts.Count; ++i )
        {
            tabScripts[i].Initialize(this, (EPieceRotate)i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectedRotateTab( EPieceRotate selectRotate )
    {
        Debug.Log( selectRotate.ToString() );
    }
}
