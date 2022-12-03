using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AIPromtTextAnim : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;



    public string[] stringArray;

    int i = 0;



    // Start is called before the first frame update
    void Start()
    {
        EndCheck();
    }

    private void Update()
    {
       
    }
    public void EndCheck()
    {

        if (i <= stringArray.Length - 1)
        {

            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
            SaveSystem.LoadPlayer();
        }


    }


    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalvisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)

        {
            int visibleCount = counter % (totalvisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalvisibleCharacters)

            {
                i += 1;
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter += 1;

            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

}
