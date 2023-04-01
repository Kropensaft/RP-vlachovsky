using System.Collections;
using TMPro;
using UnityEngine;

public class AIPromtTextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private float timeBtwnChars;
    [SerializeField] private float timeBtwnWords;

    public string[] stringArray;

    private int i = 0;

    //SaveSystem data;

    // Start is called before the first frame update
    private void Start()
    {
        EndCheck();
        // data = GetComponent<SaveSystem>();
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
            //data.LoadPlayerData();
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