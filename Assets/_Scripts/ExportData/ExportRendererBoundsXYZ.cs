using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;

public class ExportRendererBoundsXYZ : MonoBehaviour
{
    private float timer = 0f;
    private string timename;
    private string filePath;
    private FileInfo newFile;
    private ExcelPackage package;
    private List<GameObject> objectsToExport = new List<GameObject>();

    void Start()
    {
        // ��ʼ�������б�
        InitializeObjectsList();

        timename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string prefix = "Renderer_";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderPath = Path.Combine(desktopPath, "VRMuseumGuideline Data");

        // ȷ���ļ��д��ڣ���������ڣ��򴴽�
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        filePath = Path.Combine(folderPath, prefix + timename + ".xlsx");

        // �����µ�Excel�ļ�
        CreateNewExcelFile();
    }

    void InitializeObjectsList()
    {
        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 20; b++)
            {
                string objectName = $"{a}.{b}";
                GameObject foundObject = GameObject.Find(objectName);
                if (foundObject != null)
                {
                    objectsToExport.Add(foundObject);
                }
            }
        }

        // ��ӡ�����ҵ�����������
        foreach (var obj in objectsToExport)
        {
            Debug.Log(obj.name);
        }
    }

    void CreateNewExcelFile()
    {
        newFile = new FileInfo(filePath);
        package = new ExcelPackage(newFile);
        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

        // ���ñ���
        worksheet.Cells[1, 1].Value = "��������";
        worksheet.Cells[1, 2].Value = "Renderer X";
        worksheet.Cells[1, 3].Value = "Renderer Y";
        worksheet.Cells[1, 4].Value = "Renderer Z";

        // ���õ�Ԫ���ʽΪ��λС��
        worksheet.Cells[2, 2, 2, 4].Style.Numberformat.Format = "0.00";

        package.Save();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1) // ÿ�����һ��
        {
            timer = 0;
            UpdateObjectsRendererBounds();
        }
    }

    void UpdateObjectsRendererBounds()
    {
        using (package = new ExcelPackage(newFile))
        {
            var worksheet = package.Workbook.Worksheets[1];
            int rowIndex = 2; // ���� rowIndex Ϊ 2

            foreach (var obj in objectsToExport)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Vector3 boundsSize = renderer.bounds.size;
                    worksheet.Cells[rowIndex, 1].Value = obj.name;
                    worksheet.Cells[rowIndex, 2].Value = Math.Round(boundsSize.x, 2);
                    worksheet.Cells[rowIndex, 3].Value = Math.Round(boundsSize.y, 2);
                    worksheet.Cells[rowIndex, 4].Value = Math.Round(boundsSize.z, 2);
                    rowIndex++;
                }
            }

            package.Save(); // ����ÿ�εĸ���
        }
    }

    private void OnDestroy()
    {
        if (package != null)
        {
            package.Dispose();
        }
    }
}
