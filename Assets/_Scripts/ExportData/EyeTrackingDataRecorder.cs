using System;
using System.IO;
using UnityEngine;
using OfficeOpenXml;

public class EyeTrackingDataRecorder : MonoBehaviour
{
    private string filePath;
    private string timename;
    public GameObject head; // �������ߵ�Դ����
    public LayerMask layerMask; // �������߼���LayerMask
    public float rayLength = 100.0f; // ���ߵĳ���
    private int index = 2; // �ӵڶ��п�ʼ��¼����

    void Start()
    {
        timename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string prefix = "eyeTracking_"; // ����ǰ׺ΪeyeTracking_
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderName = "VRMuseumGuideline Data";
        string directoryPath = Path.Combine(desktopPath, folderName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        filePath = Path.Combine(directoryPath, prefix + timename + ".xlsx");

        CreateNewFile();

        // �޸�Ϊÿ0.2���¼һ������
        InvokeRepeating(nameof(RecordData), 0.2f, 0.2f);
    }

    private void CreateNewFile()
    {
        FileInfo excelFile = new FileInfo(filePath);
        if (excelFile.Exists)
        {
            excelFile.Delete();  // ����ļ��Ѵ��ڣ�ɾ����
        }
        using (ExcelPackage package = new ExcelPackage(excelFile))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");
            // ���±����Է�ӳ�µ�������
            string[] titles = { "Time", "CollisionPointX", "CollisionPointY", "CollisionPointZ", "CollidedObjectName" };
            for (int i = 0; i < titles.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = titles[i];
            }
            package.Save();
        }
    }

    private void RecordData()
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

            RaycastHit hit;
            Vector3 forward = head.transform.TransformDirection(Vector3.forward) * rayLength;

            if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, rayLength, layerMask))
            {
                // ��¼��ײ������ͱ���ײ���������
                Vector3 collisionPoint = hit.point;
                worksheet.Cells[index, 2].Value = collisionPoint.x;
                worksheet.Cells[index, 3].Value = collisionPoint.y;
                worksheet.Cells[index, 4].Value = collisionPoint.z;
                worksheet.Cells[index, 5].Value = hit.collider.gameObject.name;
            }
            else
            {
                // ���û����ײ����¼"NA"
                worksheet.Cells[index, 2].Value = "NA";
                worksheet.Cells[index, 3].Value = "NA";
                worksheet.Cells[index, 4].Value = "NA";
                worksheet.Cells[index, 5].Value = "NA";
            }

            index++;
            package.Save();
        }
    }
}
