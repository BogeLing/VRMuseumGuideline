using System;
using System.IO;
using UnityEngine;
using OfficeOpenXml;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ExcelManagerForTask2 : MonoBehaviour
{
    private string filePath;
    private string timename;
    public GameObject head, leftHand, rightHand;
    public InputAction markAction; // 用于即时记录的输入动作
    private int index = 2; // 从第二行开始记录数据，第一行用于标题

    void Start()
    {
        timename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string prefix = "Task2Data_";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderName = "VRMuseumGuideline Data";
        string directoryPath = Path.Combine(desktopPath, folderName);

        // 确保目标文件夹存在
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        filePath = Path.Combine(directoryPath, prefix + timename + ".xlsx");

        CreateNewFile();

        // 启用输入动作并订阅事件
        markAction.Enable();
        markAction.performed += _ => RecordDataInstantly(); // 按钮按下时记录数据

        // 每秒记录一次数据
        InvokeRepeating(nameof(RecordData), 1f, 1f);
    }

    private void CreateNewFile()
    {
        FileInfo excelFile = new FileInfo(filePath);
        if (excelFile.Exists)
        {
            excelFile.Delete(); // 如果文件已存在，删除它
        }
        using (ExcelPackage package = new ExcelPackage(excelFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");
            // 写入标题
            string[] titles = { "Time", "HeadPosX", "HeadPosY", "HeadPosZ", "HeadRotX", "HeadRotY", "HeadRotZ",
                                "LeftPosX", "LeftPosY", "LeftPosZ", "LeftRotX", "LeftRotY", "LeftRotZ",
                                "RightPosX", "RightPosY", "RightPosZ", "RightRotX", "RightRotY", "RightRotZ",
                                "Marked", "MarkPosX", "MarkPosY", "MarkPosZ", "MarkRotX", "MarkRotY", "MarkRotZ" };
            for (int i = 0; i < titles.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = titles[i];
            }
            package.Save();
        }
    }

    private void RecordData()
    {
        RecordDataInternal(false);
    }

    private void RecordDataInstantly()
    {
        RecordDataInternal(true);
    }

    private void RecordDataInternal(bool recordMark)
    {
        FileInfo excelFile = new FileInfo(filePath);
        if (!excelFile.Exists)
        {
            CreateNewFile();
        }

        using (ExcelPackage package = new ExcelPackage(excelFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Data"];
            worksheet.Cells[index, 1].Value = DateTime.Now.ToString("HH:mm:ss");

            RecordTransform(worksheet, index, 2, head.transform);
            RecordTransform(worksheet, index, 8, leftHand.transform);
            RecordTransform(worksheet, index, 14, rightHand.transform);

            if (recordMark)
            {
                GameObject markedObj = FindGrabbedObject();
                if (markedObj != null)
                {
                    worksheet.Cells[index, 20].Value = markedObj.name;
                    RecordTransform(worksheet, index, 21, markedObj.transform);
                }
            }

            index++;
            package.Save();
        }
    }

    private GameObject FindGrabbedObject()
    {
        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 20; b++)
            {
                string objectName = $"{a}.{b}";
                GameObject obj = GameObject.Find(objectName);
                if (obj != null && obj.GetComponent<XRGrabInteractable>()?.isSelected == true)
                {
                    return obj;
                }
            }
        }
        return null;
    }

    private void RecordTransform(ExcelWorksheet worksheet, int rowIndex, int colIndex, Transform t)
    {
        worksheet.Cells[rowIndex, colIndex].Value = t.position.x;
        worksheet.Cells[rowIndex, colIndex + 1].Value = t.position.y;
        worksheet.Cells[rowIndex, colIndex + 2].Value = t.position.z;
        worksheet.Cells[rowIndex, colIndex + 3].Value = t.eulerAngles.x;
        worksheet.Cells[rowIndex, colIndex + 4].Value = t.eulerAngles.y;
        worksheet.Cells[rowIndex, colIndex + 5].Value = t.eulerAngles.z;
    }

    void OnDestroy()
    {
        markAction.performed -= _ => RecordDataInstantly();
        markAction.Disable();
    }
}
