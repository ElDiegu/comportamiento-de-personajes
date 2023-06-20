using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondaryGameManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] List<GameObject> alive;
    [SerializeField] float timeToSurvive;
    [SerializeField] float actualTime;
    [SerializeField] GameObject defaultUI;
    [SerializeField] GameObject endUI;

    private void Awake()
    {
        StartCoroutine(CheckGameState());
    }

    private void FixedUpdate()
    {
        actualTime += 1.0f * Time.deltaTime;
        Debug.Log(actualTime >= timeToSurvive);
        if(actualTime >= timeToSurvive)
        {
            StopAllCoroutines();
            EndGame();
        }
    }

    IEnumerator CheckState()
    {
        var aliveChars = FindObjectsOfType<EntityInteraction>();
        foreach (EntityInteraction character in aliveChars) alive.Add(character.gameObject);
        yield return null;
    }

    IEnumerator CheckGameState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(CheckState());
            if (alive.Count <= 0) break;
            alive.Clear();
        }
        
    }

    void EndGame()
    {
        if (alive.Count < 0) endUI.GetComponentInChildren<TextMeshProUGUI>().text = $"Han muerto todos los personajes, el bug ha ganado";

        endUI.SetActive(true);
        defaultUI.SetActive(false);
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
