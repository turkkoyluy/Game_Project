using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonManager : MonoBehaviour
{
    public List<Person> people;                 // Inspector'dan kişi ekleyebilirsin
    public GameObject personItemPrefab;         // Tek bir kişi UI prefabı
    public Transform contentPanel;              // ScrollView'daki içerik paneli

    void Start()
    {
        foreach (var person in people)
        {
            GameObject obj = Instantiate(personItemPrefab, contentPanel);
            obj.transform.Find("NameText").GetComponent<Text>().text = person.firstName + " " + person.lastName;
            obj.transform.Find("DepartmentText").GetComponent<Text>().text = person.department;
            obj.transform.Find("ProfileImage").GetComponent<Image>().sprite = person.profilePic;

            obj.transform.Find("FireButton").GetComponent<Button>().onClick.AddListener(() => Fire(person));
            obj.transform.Find("RewardButton").GetComponent<Button>().onClick.AddListener(() => Reward(person));
        }
    }

    void Fire(Person person)
    {
        Debug.Log($"{person.firstName} kovuldu!");
    }

    void Reward(Person person)
    {
        Debug.Log($"{person.firstName} ödüllendirildi!");
    }
}
