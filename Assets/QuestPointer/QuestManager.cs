using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select;
using System.Linq;
public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private List<Quest> quests = new List<Quest>();
    int questIndex;
    private const string QUESTINDEX = "QuestIndex", QUESTFINISH = "QuestFinish";
    int questFinish;
    public GameObject bottle;
    private void OnEnable()
    {
        //questFinish = PlayerPrefs.GetInt(QUESTFINISH);
        if (questFinish == 1)
        {
            Destroy(QuestSelectManager.Instance);
            Destroy(this);
        }
        else
            SelectManager.Instance.enabled = false;
        //questIndex = PlayerPrefs.GetInt(QUESTINDEX);
            
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt(QUESTINDEX,questIndex);
    }
    private void Start()
    {
        //StartCoroutine(quests[0].QuestAnimation());
        for (int i = 0; i < questIndex; i++)
        {
            RemoveQuest(quests[i]);
        }
        QuestPointer.Instance.SetTargetQuestPoint(quests[0].transform);
    }
    public Quest GetCurrentQuest()
    {
        if (quests.Count == 0)
            return null;
        return quests[0];
    }
    public void RemoveQuest(Quest quest)
    {
        quest.enabled = false;
        if (quests.Contains(quest))
        {
            
            quests.Remove(quest);
            Destroy(quest);
        }
        if (quests.Count > 0)
        {
            QuestPointer.Instance.SetTargetQuestPoint(quests[0].transform);
            //StartCoroutine(quests[0].QuestAnimation());
        }
        else
        {
            SelectManager.Instance.enabled = true;
            QuestSelectManager.Instance.enabled = false; ;
            //Destroy(this);
            QuestPointer.Instance.ToggleQuestPointer(false);
            PlayerPrefs.SetInt(QUESTFINISH, 1);
        }
        questIndex++;
    }
    public void AddQuest(Quest quest)
    {
        if (!quests.Contains(quest))
        {
            quests.Add(quest);
        }
    }
    public void AddAtQuest(Quest quest)
    {
        quests.Add(null);
        for (int i = quests.Count - 1; i < 0; i++)
        {
            quests[i + 1] = quests[i];
        }
        quests[0] = quest;
    }
    public bool CheckCurrentQuest(Quest quest)
    {
        return quests[0] == quest;
    }
}


