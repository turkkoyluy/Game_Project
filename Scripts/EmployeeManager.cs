using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro kullanıyorsanız bu satırı ekleyin
using System.IO; // Dosya işlemleri için
using System.Collections.Generic; // List için

public class EmployeeManager : MonoBehaviour
{
    // UI Elemanları
    public TMP_InputField nameInputField; // TextMeshPro InputField
    public TMP_InputField positionInputField; // TextMeshPro InputField
    public Button addButton;
    public Button clearButton; // Clear butonu için yeni değişken ekledik
    public TextMeshProUGUI employeeListText; // TextMeshPro Text

    // JSON dosyasının yolu
    private string jsonFilePath;
    private EmployeeList currentEmployeeList;

    void Awake()
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "employees.json");

        // Butonlara tıklama olayı dinleyicilerini ekle
        addButton.onClick.AddListener(OnAddButtonClicked);
        clearButton.onClick.AddListener(OnClearButtonClicked); // Clear butonu için dinleyici ekledik

        // Uygulama başladığında mevcut çalışanları yükle
        LoadEmployeesFromJson();
        DisplayEmployees();
    }

    public void OnAddButtonClicked()
    {
        string employeeName = nameInputField.text.Trim();
        string position = positionInputField.text.Trim();

        if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(position))
        {
            Debug.LogWarning("Employee Name and Position cannot be empty.");
            return;
        }

        Employee newEmployee = new Employee(employeeName, position);
        currentEmployeeList.employees.Add(newEmployee);

        SaveEmployeesToJson();
        DisplayEmployees();

        nameInputField.text = "";
        positionInputField.text = "";
    }

    // Yeni metot: Clear butonu tıklandığında çalışacak
    public void OnClearButtonClicked()
    {
        Debug.Log("OnClearButtonClicked metodu çağrıldı!"); // Metodun başında

        currentEmployeeList.employees.Clear();

        // Sadece birini seçmelisin!
        if (File.Exists(jsonFilePath))
        {
            // Dosyayı tamamen silmek istersen:
            File.Delete(jsonFilePath);
            Debug.Log("JSON file deleted: " + jsonFilePath);
        }
        // Veya sadece içeriğini boş EmployeeList olarak kaydetmek istersen:
        // Bu satırı File.Delete() ile aynı anda kullanma
        SaveEmployeesToJson();


        DisplayEmployees();
        Debug.Log("Employee list cleared."); // Metodun sonunda
    }


    void SaveEmployeesToJson()
    {
        string json = JsonUtility.ToJson(currentEmployeeList, true);
        File.WriteAllText(jsonFilePath, json);
        Debug.Log("Employees saved to: " + jsonFilePath);
    }

    void LoadEmployeesFromJson()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            currentEmployeeList = JsonUtility.FromJson<EmployeeList>(json);
            Debug.Log("Employees loaded from: " + jsonFilePath);
        }
        else
        {
            currentEmployeeList = new EmployeeList();
            Debug.Log("No existing employees. Creating a new list.");
        }
    }

    void DisplayEmployees()
    {
        employeeListText.text = "";

        if (currentEmployeeList.employees.Count == 0)
        {
            employeeListText.text = "No employees added yet.";
            return;
        }

        foreach (Employee emp in currentEmployeeList.employees)
        {
            employeeListText.text += $"Name: {emp.employeeName} | Position: {emp.position}\n";
        }
    }
}