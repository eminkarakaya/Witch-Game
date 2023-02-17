using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select;
using System.Linq;
public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private List<Quest> quests = new List<Quest>();
    [SerializeField] int questIndex;
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
        // PlayerPrefs.SetInt(QUESTINDEX,questIndex);
    }
    private void Start()
    {
        //StartCoroutine(quests[0].QuestAnimation());
        if(quests.Count == 0)
        {
            SelectManager.Instance.enabled = true;
            Quest [] quests = FindObjectsOfType<Quest>();
            foreach (var item in quests)
            {
                Destroy(item);
            }
            Destroy(QuestSelectManager.Instance);
            Destroy(this);
        }
        else
            QuestPointer.Instance.SetTargetQuestPoint(quests[0].transform);

        for (int i = 0; i < questIndex; i++)
        {
            RemoveQuest(quests[i]);
        }
        
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
            // PlayerPrefs.SetInt(QUESTFINISH, 1);
        }
        Debug.Log(questIndex + " " + quest , quest.gameObject);
        questIndex++;
    }
    public void AddQuest(Quest quest)
    {
        if (!quests.Contains(quest))
        {
            quests.Add(quest);
        }
    }
    public void AddAtQuest(Quest quest,int index)
    {
        quests.Insert(0,quest);
        Debug.Log(quests.Count);
    }
    public bool CheckCurrentQuest(Quest quest)
    {
        return quests[0] == quest;
    }
}


