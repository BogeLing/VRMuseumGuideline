using System;
using System.IO;
using UnityEngine;
using OfficeOpenXml;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ExcelManagerForTask1 : MonoBehaviour
{
    private string filePath;
    private string timename;
    public GameObject head;
    public GameObject table; // ���������ӵ�����
    public InputAction markAction; // ���ڼ�ʱ��¼�����붯��
    private int index = 2; // �ӵڶ��п�ʼ��¼���ݣ���һ�����ڱ���

    void Start()
    {
        timename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string prefix = "Task1Data_";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderName = "VRMuseumGuideline Data";
        string directoryPath = Path.Combine(desktopPath, folderName);

        // ȷ��Ŀ���ļ��д���
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        filePath = Path.Combine(directoryPath, prefix + timename + ".xlsx");

        CreateNewFile();

        // �������붯���������¼�
        markAction.Enable();
        markAction.performed += _ => RecordDataInstantly(); // ��ť����ʱ��¼����

        // ÿ���¼һ������
        InvokeRepeating(nameof(RecordData), 1f, 1f);
    }

    private void CreateNewFile()
    {
        FileInfo excelFile = new FileInfo(filePath);
        if (excelFile.Exists)
        {
            excelFile.Delete(); // ����ļ��Ѵ��ڣ�ɾ����
        }
        using (ExcelPackage package = new ExcelPackage(excelFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");
            // д�����
            string[] titles = { "Time", "HeadPosX", "HeadPosY", "HeadPosZ", "HeadRotX", "HeadRotY", "HeadRotZ", "TableHeight", "Marked" }; // ���������Ӹ߶�
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

            // ��������¼���Ӹ߶�
            float tableHeight = CalculateTableHeight();
            worksheet.Cells[index, 8].Value = tableHeight;

            if (recordMark)
            {
                string markedObject = FindActiveObjectName();
                worksheet.Cells[index, 9].Value = markedObject; // ��������Ϊ9
            }

            index++;
            package.Save();
        }
    }

    // �������������Ӹ߶ȵĺ���
    private float CalculateTableHeight()
    {
        float groundY = 0f; // ��������Y������0
        float tableCenterY = table.transform.position.y;
        float tableTotalHeight = table.transform.localScale.y;
        float tableTopY = tableCenterY + (tableTotalHeight / 2);
        return tableTopY - groundY;
    }

    private string FindActiveObjectName()
    {
        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 20; b++)
            {
                string objectName = $"{a}.{b}";
                GameObject obj = GameObject.Find(objectName);
                if (obj != null && obj.activeSelf)
                {
                    return objectName;
                }
            }
        }
        return "";
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