using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Teams")]
    [SerializeField] List<List<GameObject>> teams = new List<List<GameObject>>(4);
    [SerializeField] int aliveTeams;
    [SerializeField] static int winner;
    [SerializeField] GameObject defaultUI;
    [SerializeField] GameObject endUI;

    private void Awake()
    {
        InitializeTeams();
        StartCoroutine(CheckGameState());
    }

    IEnumerator CheckTeamsState()
    {
        var characters = FindObjectsOfType<EntityInteraction>();
        foreach(EntityInteraction character in characters) teams[character.team - 1].Add(character.gameObject);
        yield return null;
    }

    IEnumerator CheckGameState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(CheckTeamsState());
            aliveTeams = 0;
            foreach (List<GameObject> team in teams) if (team.Count > 0) aliveTeams++;
            if (aliveTeams <= 1) break;
            for (int i = 0; i < teams.Count; i++) teams[i].Clear();
        }
        EndGame();
    }

    public void EndGame()
    {
        if(aliveTeams == 0)
        {
            endUI.GetComponentInChildren<TextMeshProUGUI>().text = $"Han muerto todos los equipos";
        } 
        else
        {
            for(int i = 0; i < 4; i++) if (teams[i].Count > 0) { winner = i; break; }
            endUI.GetComponentInChildren<TextMeshProUGUI>().text = $"Ha ganado el equipo {winner}";
        }
        defaultUI.SetActive(false);
        endUI.SetActive(true);
    }
    public void InitializeTeams()
    {
        for (int i = 0; i < 4; i++) teams.Add(new List<GameObject>());
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
