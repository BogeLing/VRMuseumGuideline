using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;


public class DynamicExcelUpdate : MonoBehaviour
{
    private float timer = 0f;
    private List<GameObject> objectsToExport = new List<GameObject>();
    private string filePath;

    private void Start()
    {
        objectsToExport = InitializeObjectsList();
        filePath = GenerateFilePath();
    }

    private List<GameObject> InitializeObjectsList()
    {
        return Enumerable.Range(1, 10).SelectMany(a => Enumerable.Range(1, 20).Select(b =>
        {
            string objectName = $"{a}.{b}";
            return GameObject.Find(objectName);
        })).Where(obj => obj != null).ToList();
    }

    private string GenerateFilePath()
    {
        string timename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string prefix = "Renderer_";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string directoryPath = Path.Combine(desktopPath, "VRMuseumGuideline Data");
        Directory.CreateDirectory(directoryPath); // 如果目录已存在，CreateDirectory 不会抛出异常
        return Path.Combine(directoryPath, prefix + timename + ".xlsx");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;
            UpdateExcelFile();
        }
    }

    private void UpdateExcelFile()
    {
        FileInfo fileInfo = new FileInfo(filePath);
        using (var package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault() ?? package.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells["A1"].Value = "物体名称";
            worksheet.Cells["B1"].Value = "Renderer X";
            worksheet.Cells["C1"].Value = "Renderer Y";
            worksheet.Cells["D1"].Value = "Renderer Z";
            worksheet.Cells["E1"].Value = "Shape Slider Value"; // 修改列标题以反映 Shape
            worksheet.Cells["F1"].Value = "Texture Slider Value"; // 新列标题

            int rowIndex = 2;
            foreach (var obj in objectsToExport)
            {
                worksheet.Cells[rowIndex, 1].Value = obj.name;
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    var boundsSize = renderer.bounds.size;
                    worksheet.Cells[rowIndex, 2].Value = Math.Round(boundsSize.x, 2);
                    worksheet.Cells[rowIndex, 3].Value = Math.Round(boundsSize.y, 2);
                    worksheet.Cells[rowIndex, 4].Value = Math.Round(boundsSize.z, 2);
                }

                // Assume Canvas is a sibling to obj, so we search the parent for a Canvas child
                var canvas = obj.transform.parent.parent?.Find("Canvas");
                if (canvas != null)
                {
                    // Find Shape and Texture Sliders within Canvas
                    var shapeSlider = canvas.Find("Shape")?.GetComponent<Slider>();
                    var textureSlider = canvas.Find("Texture")?.GetComponent<Slider>();

                    // Record their values or mark as N/A
                    worksheet.Cells[rowIndex, 5].Value = shapeSlider != null ? shapeSlider.value.ToString() : "N/A";
                    worksheet.Cells[rowIndex, 6].Value = textureSlider != null ? textureSlider.value.ToString() : "N/A";
                }
                else
                {
                    worksheet.Cells[rowIndex, 5].Value = "N/A";
                    worksheet.Cells[rowIndex, 6].Value = "N/A";
                }

                rowIndex++;
            }

            try
            {
                package.Save();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error saving Excel file: {ex.Message}");
            }
        }
    }



    private void OnDestroy()
    {
        // 在这种实现中不需要显式地释放资源，因为使用了using语句
    }
}